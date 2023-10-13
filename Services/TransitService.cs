using System.Buffers;
using System.Text;
using transit_parser.DTO;
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

    public ScheduleRouteDTO[] AcquireRoute(string route)
    {
        var routeInformation = _tripContext[route]
            .Select(t => new ScheduleRouteDTO
            {
                Route = t.RouteId,
                Service = t.Service,
                StopInfo = _stopTimesContext[t.TripId]
                    .Select(s => new ScheduleRootStopTime
                    {
                        StopId = s.StopId,
                        ArrivalTime = s.ArrivalTime,
                        DepartureTime = s.DepartureTime,
                    })
            }).ToArray();

        return routeInformation;
    }

    private void ParseFile<T>(string filePath)
    {
        using var fileStream = new FileInfo(filePath).Open(FileMode.Open, FileAccess.Read);
        using var streamReader = new StreamReader(fileStream);
        streamReader.ReadLine(); // skip headers

        var builder = new StringBuilder();

        var endOfFile = false;
        while (fileStream.CanRead)
        {
            builder.Clear();

            while (endOfFile == false)
            {
                var readByte = streamReader.Read();
                var readChar = (char)readByte;

                if (readByte == -1)
                {
                    endOfFile = true;
                    streamReader.Close();
                    fileStream.Close();
                    break;
                }

                if (readChar == '\r') continue;
                if (readChar == '\n') break;

                builder.Append(readChar);
            }

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
        }
    }

    private void BuildTripContext(char[] bufferLine)
    {
        var model = new TripRecord(bufferLine);
        var key = model.RouteId;

        if (!_tripContext.ContainsKey(key))
        {
            _tripContext.Add(key, new List<TripRecord> { model });
        }
        else
        {
            _tripContext[key].Add(model);
        }
    }

    private void BuildStopTimeContext(char[] bufferLine)
    {
        var model = new StopTimeRecord(bufferLine);
        var key = model.TripId;

        if (!_stopTimesContext.ContainsKey(key))
        {
            _stopTimesContext.Add(key, new List<StopTimeRecord> { model });
        }
        else
        {
            _stopTimesContext[key].Add(model);
        }
    }
}