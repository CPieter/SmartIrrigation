using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace SmartIrrigation.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        System.Diagnostics.Debug.WriteLine("yea");
        _logger = logger;
    }

    [HttpGet(Name = "Test")]
    public IEnumerable<String> Get()
    {
        IEnumerable<int> range = Enumerable.Range(1, 5);
        List<String> messages = new List<string>();
        foreach (var index in range)
        {
            messages.Add("message number " + index);
        }
        return messages;
    }
}