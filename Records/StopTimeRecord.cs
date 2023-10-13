using transit_parser.ExtensionMethods;

namespace transit_parser.Records;

public record StopTimeRecord
{
    public StopTimeRecord(char[] bufferLine)
    {
        TripId = bufferLine.ParseSection();
        ArrivalTime = bufferLine.ParseSection(2);
        DepartureTime = bufferLine.ParseSection(3);
        StopId = bufferLine.ParseSection(4);
    }

    public char[] TripId { get; private set; }
    public char[] ArrivalTime { get; private set; }
    public char[] DepartureTime { get; private set; }
    public char[] StopId { get; private set; }

    // public string stop_sequence { get; set; }
    // public string stop_headsign { get; set; }
    // public string pickup_type { get; set; }
    // public string drop_off_type { get; set; }
    // public string timepoint { get; set; }
    // public string checkpoint_id { get; set; }
    // public bool continuous_pickup { get; set; }
    // public bool continuous_drop_off { get; set; }
}