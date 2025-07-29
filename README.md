# Echo Foundry Agent

This sample is an updated version of the EchoBot template that uses Microsoft 365 Agents SDK and Semantic Kernel to integrate an Azure AI Foundry Agent into Microsoft 365 Copilot.

It demonstrates how to:

- Use Semantic Kernel to connect to the Azure AI Foundry project and retrieve the Agent definition from Azure AI Agent Service.
- Use Semantic Kernel to orchestrate the conversation between the user and the Agent.
- Use Microsoft 365 Agents SDK to return streamed responses to Microsoft 365 Copilot.
- Use Semantic Kernel to handle threaded conversations in Azure AI Agent Service.
- Use Microsoft 365 Agents SDK to store and retrieve values from conversation state.
- Use Blob Storage to persist conversation state across sessions.
- Use Azurite to simulate Azure Blob Storage for local development.

This sample is intended to be used as a starting point for building your own Agents that can be integrated into Microsoft 365 Copilot.

## Prerequisites

- Visual Studio 2022 17.14.9 (or higher) with Microsoft Teams development tools workload installed

## Minimal steps to awesome

1. Clone this repository.
1. Go to [Azure AI Foundry](https://ai.azure.com) in a browser, create a project and agent.
1. Open the solution in Visual Studio 2022.
1. In M365Agent project, open the `env` folder, rename `.env.local.user.sample` to `.env.local.user`, and `.env.local.sample` to `.env.local`.
1. In `.env.local` file, replace `<AzureAIProjectAgentId>` with the ID of the agent and `<AzureAIProjectEndpoint>` for the agent and project you created in Azure AI Foundry.
1. Create a public Dev Tunnel.
1. Select `Microsoft 365 Copilot (browser)` as the startup project and start a debug session (F5). This will open Microsoft 365 Copilot chat in your browser.
1. Select the agent from the Agents menu and send a prompt.
1. Update the agent in Azure AI Foundry to add instructions, and test the changes in Microsoft 365 Copilot.
