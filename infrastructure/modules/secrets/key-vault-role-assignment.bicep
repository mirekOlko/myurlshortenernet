param keyVaultName string
param principalIds array
param principalType string = 'ServicePrincipal'
//id is heare https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles
param roleDefinition string = 'KeyVaultSecretsUser'
param roleDefinitionId string = '4633458b-17de-408a-b874-0445c86b69e6'

resource keyVault 'Microsoft.KeyVault/vaults@2024-11-01' existing = {
  name: keyVaultName
}

resource keyVaultRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = [
  for principalId in principalIds: {
    name: guid(keyVault.id, principalId, roleDefinition)
    scope: keyVault
    properties: {
      roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleDefinitionId)
      principalId: principalId
      principalType: principalType
    }
  }
]
