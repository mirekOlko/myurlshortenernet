param location string = resourceGroup().location
param vaultName string

resource keyVault 'Microsoft.KeyVault/vaults@2024-11-01' = {
  name: vaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    enableRbacAuthorization: true
    tenantId: subscription().tenantId
    accessPolicies: []
  }
}

output id string = keyVault.id
output name string = keyVault.name
