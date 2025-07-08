param name string
param location string
param kind string
param databaseName string
param laocationName string
param keyVaultName string

param containers array = [
  {
    name: 'items'
    partitionKey: '/PartitionKey'
  }
]

resource cosmosDbAccount 'Microsoft.DocumentDB/databaseAccounts@2025-04-15' = {
  name: name
  location: location
  kind: kind
  properties: {
    databaseAccountOfferType: 'Standard'
    locations: [
      { locationName: laocationName, failoverPriority: 0, isZoneRedundant: false }
    ]
  }
}

resource cosmosDBDatabase 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2025-04-15' = {
  parent: cosmosDbAccount
  name: databaseName
  properties: {
    resource: {
      id: databaseName
    }
  }
}

resource cosmosDbContainers 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases/containers@2025-04-15' = [
  for container in containers: {
    parent: cosmosDBDatabase
    name: container.name
    properties: {
      resource: {
        id: container.name
        partitionKey: { paths: [container.partitionKey], kind: 'Hash' }
        indexingPolicy: {
          automatic: true
          indexingMode: 'consistent'
          includedPaths: [{ path: '/*' }]
          excludedPaths: [{ path: '/"_etag"/?' }]
        }
        defaultTtl: -1
      }
    }
  }
]

resource keyVault 'Microsoft.KeyVault/vaults@2024-11-01' existing = {
  name: keyVaultName
}

resource cosmosDBConnectionString 'Microsoft.KeyVault/vaults/secrets@2024-11-01' = {
  parent: keyVault
  name: 'CosmosDB--ConnectionString'
  properties: {
    value: cosmosDbAccount.listConnectionStrings().connectionStrings[0].connectionString
  }
}

output cosmosDBId string = cosmosDbAccount.id
