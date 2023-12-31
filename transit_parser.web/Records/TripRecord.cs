using transit_parser.ExtensionMethods;

namespace transit_parser.Records;

public record TripRecord
{
    public TripRecord(char[] bufferLine)
    {
        RouteId = bufferLine.ParseSection();
        Service = bufferLine.ParseSection(2);
        TripId = bufferLine.ParseSection(3);
    }
    
    public string RouteId { get; private set; }
    public string Service { get; private set; }
    public string TripId { get; private set; }
    
    // public string trip_headsign { get; set; }
    // public string trip_shortname { get; set; }
    // public string direction_id { get; set; }
    // public string block_id { get; set; }
    // public string shape_id { get; set; }
    // public string wheelchair_accessible { get; set; }
    // public string trip_route_type { get; set; }
    // public string route_pattern_id { get; set; }
    // public bool bikes_allowed { get; set; }
}
