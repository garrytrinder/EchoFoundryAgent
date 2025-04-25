# EchoFoundryAgent

This sample is an updated version of the EchoBot template that uses the Azure AI Foundry SDK and Microsoft 365 Agents SDK to create an agent that can respond to user queries in Microsoft Teams or Microsoft 365 Copilot Chat.

It demonstrates how to:

- Use Azure AI Foundry SDK to connect to an Agent in an Azure AI Foundry Project 
- Use Semantic Kernel to handle the conversation and call the Azure AI Foundry Agent
- Use Microsoft 365 Agents SDK to return streamed responses to the user in Teams, or Copilot

## Prerequisites

- Visual Studio 2022 17.14.0 Preview 5 with Microsoft Teams development tools workload installed
- Microsoft 365 Copilot licence (to use in Microsoft 365 Copilot Chat)

## Minimal steps to awesome

1. Create an Azure AI Foundry project and agent with instructions
1. In TeamsApp project, open `env/.env.local.user` file and add `SECRET_AZURE_AI_PROJECT_CONNECTION_STRING=<AzureAIFoundryProjectConnectionString>` to the file. You can find the connection string in the Azure AI Foundry project page.
1. In TeamsApp project, open `env/.env.local` file and add `AZURE_AI_AGENT_ID=<AzureAIFoundryAgentID>` to the file. You can find the agent ID in the Azure AI Foundry project Agents page.
1. In Visual Studio, create a public Dev Tunnel.
1. Select `Microsoft Teams (browser)` as the startup project and start a debug session (F5). This will open Microsoft Teams in your browser and the app installation dialog.
