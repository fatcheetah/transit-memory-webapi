using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
    // [PerformanceTestFilter]
    [Route("Schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Schedule(string route)
    {
        try
        {
            var routeInformation = _transitService.AcquireRoute(route);
            return Ok(routeInformation);
        }
        catch (Exception)
        {
            return NoContent();
        }
    }
}

public class PerformanceTestFilter : ActionFilterAttribute
{
    private Stopwatch _watch = new Stopwatch();
    private int _requestsProcessed = 0;

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        _watch.Reset();
        _watch.Start();
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        _requestsProcessed++;
        _watch.Stop();
        var executionTime = _watch.ElapsedTicks * 1000000000 / Stopwatch.Frequency;
        Console.WriteLine($"response returned in: {executionTime:##.###} ns");
        Console.WriteLine($"requests processed {_requestsProcessed} ");
    }
}
