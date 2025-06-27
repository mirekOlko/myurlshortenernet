param location string = resourceGroup().location
var uniqueId = uniqueString(resourceGroup().id)
var keyVaultName string = 'kv-${uniqueId}'
param cos string = 'mynameisadam'

@description('Sekret, który ma zostać zapisany')
@secure()
param secretValue string

@description('Użytkownik lub aplikacja, która ma dostać uprawnienia (objectId)')
param principalObjectId string

@description('Nazwa sekreta')
param secretName string = 'my-secret-test'

resource keyVault 'Microsoft.KeyVault/vaults@2023-02-01' = {
  name: keyVaultName
  location: location
  properties: {
    tenantId: subscription().tenantId
    enableRbacAuthorization: true
    sku: {
      name: 'standard'
      family: 'A'
    }
    accessPolicies: [] // RBAC only
  }
}

resource secret 'Microsoft.KeyVault/vaults/secrets@2023-02-01' = {
  name: '${keyVault.name}/${secretName}'
  properties: {
    value: secretValue
  }
  dependsOn: [keyVault]
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(keyVault.id, principalObjectId, 'KeyVaultSecretsOfficer')
  scope: keyVault
  properties: {
    principalId: principalObjectId
    roleDefinitionId: subscriptionResourceId(
      'Microsoft.Authorization/roleDefinitions',
      'b86a8fe4-44ce-4948-aee5-eccb2c155cd7'
    ) // Key Vault Secrets Officer
    principalType: 'User' // lub 'ServicePrincipal'
  }
}
