namespace EchoAgent.Models
{
    public class AzureAIAgentConfiguration
    {
        public string AgentId { get; set; }
        public string ProjectEndpoint { get; set; }
        public string SubscriptionId { get; set; }
        public string ResourceGroupName { get; set; }
        public string ProjectName { get; set; }
    }
}
