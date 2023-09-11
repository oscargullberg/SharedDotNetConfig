using Microsoft.AspNetCore.Mvc;

namespace GullbergSolutions.SharedAspNetConfig.Demo.Controllers;

[ApiController]
[Route("/configs")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;

    public ConfigController(ILogger<ConfigController> logger, IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public Dictionary<string, string?> Get()
    {
        return _config.AsEnumerable().ToDictionary(x => x.Key, x => x.Value);
    }
}