namespace transit_parser.DTO;

public struct ScheduleRouteDTO
{
    public string Route { get; set; }
    public string Service { get; set; }
    public ScheduleRouteStopTime[] StopInfo { get; set; }
}

public struct ScheduleRouteStopTime
{
    public string StopId { get; set; }
    public string ArrivalTime { get; set; }
    public string DepartureTime { get; set; }
}