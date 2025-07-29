@maxLength(20)
@minLength(4)
@description('Used to generate names for all resources in this file')
param resourceBaseName string

@maxLength(42)
param botDisplayName string

param botEntraAppClientId string
param botEntraAppTenantId string
param messageEndpointDomain string

resource botService 'Microsoft.BotService/botServices@2021-03-01' = {
  kind: 'azurebot'
  location: 'global'
  name: resourceBaseName
  properties: {
    displayName: botDisplayName
    endpoint: 'https://${messageEndpointDomain}/api/messages'
    msaAppId: botEntraAppClientId
    msaAppTenantId: botEntraAppTenantId
    msaAppType:'SingleTenant'
  }
  sku: {
    name: 'F0'
  }
}

resource botServiceMsTeamsChannel 'Microsoft.BotService/botServices/channels@2021-03-01' = {
  parent: botService
  location: 'global'
  name: 'MsTeamsChannel'
  properties: {
    channelName: 'MsTeamsChannel'
  }
}
