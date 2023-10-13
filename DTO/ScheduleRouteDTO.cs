namespace transit_parser.DTO;

public struct ScheduleRouteDTO
{
    public string Route { get; set; }
    public string Service { get; set; }
    public IEnumerable<ScheduleRootStopTime> StopInfo { get; set; }
}

public struct ScheduleRootStopTime
{
    public string StopId { get; set; }
    public string ArrivalTime { get; set; }
    public string DepartureTime { get; set; }
}