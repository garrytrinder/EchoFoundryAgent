using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.Builder;
using Microsoft.Agents.Builder.App;
using Microsoft.Agents.Builder.State;
using Microsoft.Agents.Core.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.AzureAI;
using Microsoft.SemanticKernel.ChatCompletion;

namespace EchoAgent.Agent;

public class EchoAgent : AgentApplication
{
    private readonly AIProjectClient _projectClient;
    private readonly string _agentId;

    public EchoAgent(AgentApplicationOptions options, IConfiguration configuration): base(options)
    {
        var agentConfig = configuration.GetSection("AzureAIAgentConfiguration").Get<AzureAIAgentConfiguration>();
        _projectClient = AzureAIAgent.CreateAzureAIClient(agentConfig.ConnectionString, new AzureCliCredential());
        _agentId = agentConfig.AgentId;
    }

    [Route(RouteType = RouteType.Conversation, EventName = ConversationUpdateEvents.MembersAdded)]
    protected async Task WelcomeMessageAsync(ITurnContext turnContext, ITurnState turnState, CancellationToken cancellationToken)
    {
        foreach (ChannelAccount member in turnContext.Activity.MembersAdded)
        {
            if (member.Id != turnContext.Activity.Recipient.Id)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Hello and Welcome!"), cancellationToken);
            }
        }
    }

    [Route(RouteType = RouteType.Activity, Type = ActivityTypes.Message, Rank = RouteRank.Last)]
    protected async Task OnMessageAsync(ITurnContext turnContext, ITurnState turnState, CancellationToken cancellationToken)
    {
        // get the Azure AI Agent
        var agentClient = _projectClient.GetAgentsClient();
        var agentModel = await agentClient.GetAgentAsync(_agentId, cancellationToken);
        var agent = new AzureAIAgent(agentModel, agentClient);

        try
        {
            // send the initial message
            await turnContext.StreamingResponse.QueueInformativeUpdateAsync("Working on it...", cancellationToken);

            // increment the message count in state
            int count = turnState.Conversation.IncrementMessageCount();
            turnContext.StreamingResponse.QueueTextChunk($"[{count}] ");

            // create the chat message
            var message = new ChatMessageContent(AuthorRole.User, turnContext.Activity.Text);

            // stream the chat response
            await foreach (StreamingChatMessageContent response in agent.InvokeStreamingAsync(message, cancellationToken: cancellationToken))
            {
                var content = string.IsNullOrEmpty(response.Content) ? "" : response.Content.ToString();
                turnContext.StreamingResponse.QueueTextChunk(content);
            }
        }
        finally
        {
            await turnContext.StreamingResponse.EndStreamAsync(cancellationToken);
        }
    }
}
