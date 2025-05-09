# EchoFoundryAgent

This sample is an updated version of the EchoBot template that uses the Azure AI Foundry SDK, Semantic Kernel and Microsoft 365 Agents SDK to create an agent that can respond to user queries in Microsoft Teams or Microsoft 365 Copilot as a custom engine agent.

It demonstrates how to:

- Use Azure AI Foundry SDK to connect to an Agent in an Azure AI Foundry Project 
- Use Semantic Kernel to handle the conversation and call the Azure AI Foundry Agent
- Use Microsoft 365 Agents SDK to return streamed responses including citations to the user in Teams, and Copilot
- Use Microsoft 365 Agents SDK to add labels, AI generated and Sensitivity labels to messages returned in Teams

## Prerequisites

- Visual Studio 2022 17.14.0 Preview with Microsoft Teams development tools workload installed
- Microsoft 365 Copilot licence (to use in Microsoft 365 Copilot Chat)

## Minimal steps to awesome

1. Create an Azure AI Foundry project and Agent with instructions
1. Add knowledge to the Agent using the Files datasource, upload documents from the `Assets` folder in the project 
1. In TeamsApp project, open the `env` folder, rename `.env.local.user.sample` to `.env.local.user`, and `.env.local.sample` to `.env.local`.
1. In `.env.local.user` file, replace `<AzureAIFoundryProjectConnectionString>` with the connection string of your Azure AI Foundry project.
1. In `.env.local` file, replace `<AzureAIFoundryAgentId>` with the ID of the agent you created in the Azure AI Foundry project.
1. In Visual Studio, create a public Dev Tunnel.
1. Select `Microsoft Teams (browser)` as the startup project and start a debug session (F5). This will open Microsoft Teams in your browser and the app installation dialog.
