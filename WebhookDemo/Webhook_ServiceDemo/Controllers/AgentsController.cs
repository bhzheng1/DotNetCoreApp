using Microsoft.AspNetCore.Mvc;
using Webhook_ServiceDemo.Services;

namespace Webhook_ServiceDemo.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AgentsController : ControllerBase
{
    public AgentsController(AgentService agentService)
    {
        _agentService = agentService;
    }
    private readonly AgentService _agentService;
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var agent = _agentService.Agents.FirstOrDefault(agent => agent.Id == id);
        if (agent is null) return NotFound();
        return Ok(agent);
    }
}