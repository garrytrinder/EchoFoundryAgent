using Microsoft.Agents.Builder;
using Microsoft.Agents.Hosting.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoAgent.Controllers;

// ASP.Net Controller that receives incoming HTTP requests from the Azure Bot Service or other configured event activity protocol sources.
// When called, the request has already been authorized and credentials and tokens validated.
[Authorize]
[ApiController]
[Route("api/messages")]
public class AgentController(IAgentHttpAdapter adapter, IAgent agent) : ControllerBase
{
    [HttpPost]
    public Task PostAsync(CancellationToken cancellationToken)
        => adapter.ProcessAsync(Request, Response, agent, cancellationToken);
}