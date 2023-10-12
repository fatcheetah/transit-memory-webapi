using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using transit_parser.Services;

namespace transit_parser.Controllers;

[ApiController]
[Route("[controller]")]
public class TransitController : ControllerBase
{
    private readonly ILogger<TransitController> _logger;
    private readonly TransitService _transitService;

    public TransitController(ILogger<TransitController> logger, TransitService transitService)
    {
        _logger = logger;
        _transitService = transitService;
    }

    [HttpGet]
    [Route("Schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Schedule(string route)
    {
        // AppDomain.MonitoringIsEnabled = true;
        try
        {
            var routeInformation = _transitService.AcquireRoute(route);
            // Console.WriteLine($"Took: {AppDomain.CurrentDomain.MonitoringTotalProcessorTime.TotalMilliseconds:#,###} ms");
            // Console.WriteLine($"Allocated: {AppDomain.CurrentDomain.MonitoringTotalAllocatedMemorySize / 1024:#,#} kb");
            // Console.WriteLine($"Peak Working Set: {Process.GetCurrentProcess().PeakWorkingSet64 / 1024:#,#} kb");
            return Ok(routeInformation);
        }
        catch (Exception)
        {
            return NoContent();
        }
    }
}