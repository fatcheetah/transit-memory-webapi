using System.Buffers;
using System.Text;
using transit_parser.ExtensionMethods;
using transit_parser.Records;

namespace transit_parser.Services;

public class TransitService
{
    private Dictionary<string, List<TripRecord>> _tripContext = new();
    private Dictionary<string, List<StopTimeRecord>> _stopTimesContext = new();

    public TransitService()
    {
        var baseDirectory = "/MBTA_GTFS/";
        var tripsFilePath = $"{baseDirectory}trips.txt";
        var stopTimesFilePath = $"{baseDirectory}stop_times.txt";

        ParseFile<TripRecord>(tripsFilePath);
        ParseFile<StopTimeRecord>(stopTimesFilePath);
    }

    public string AcquireRoute(string route)
    {
        var routeInformation = _tripContext[route]
            .Select(t => new
            {
                Route = t,
                StopInfo = _stopTimesContext[t.TripId]
                    .Select(s => new
                    {
                        s.ArrivalTime,
                        s.DepartureTime
                    })
            }).ToJson();

        return routeInformation;
    }

    private void ParseFile<T>(string filePath)
    {
        using var fileStream = new FileInfo(filePath).Open(FileMode.Open, FileAccess.Read);
        using var streamReader = new StreamReader(fileStream);
        streamReader.ReadLine(); // skip headers

        var builder = new StringBuilder();

        while (fileStream.CanRead)
        {
            builder.Clear();

            while (streamReader.Peek() >= 0)
            {
                var readChar = (char)streamReader.Read();

                if (readChar == '\r') continue;
                if (readChar == '\n') break;

                builder.Append(readChar);
            }

            var charBuffer = ArrayPool<char>.Shared.Rent(builder.Length);
            var bufferPool = ArrayPool<char>.Shared;
            var charBuffer = bufferPool.Rent(builder.Length);

            for (int i = 0; i < builder.Length; i++)
            {
                charBuffer[i] = builder[i];
            }

            if (typeof(T) == typeof(TripRecord))
            {
                BuildTripContext(charBuffer);
            }

            if (typeof(T) == typeof(StopTimeRecord))
            {
                BuildStopTimeContext(charBuffer);
            }
            bufferPool.Return(charBuffer);

            if (streamReader.Peek() == -1)
            {
                fileStream.Close();
            }
        }
    }

    private void BuildTripContext(char[] bufferLine)
    {
        var model = new TripRecord(bufferLine);

        if (!_tripContext.ContainsKey(model.RouteId))
        {
            _tripContext.Add(model.RouteId, new List<TripRecord> { model });
        }
        else
        {
            _tripContext[model.RouteId].Add(model);
        }
    }

    private void BuildStopTimeContext(char[] bufferLine)
    {
        var model = new StopTimeRecord(bufferLine);

        if (!_stopTimesContext.ContainsKey(model.TripId))
        {
            _stopTimesContext.Add(model.TripId, new List<StopTimeRecord> { model });
        }
        else
        {
            _stopTimesContext[model.TripId].Add(model);
        }
    }
}