# myurlshortenernet

myurlshortenernet

## Infrastracture as Code

### Instal CLI Azure

https://learn.microsoft.com/pl-pl/cli/azure/install-azure-cli?view=azure-cli-latest

### Log into Azure

```bash
az login
```

### Create Resource Group

```bash
az group create --name myurlshortener-dev --location westeurope
```

### test configuraction

```bash
az deployment group what-if --resource-group myurlshortener-dev --template-file infrastructure/main.bicep
```

### deployment

```bash
az deployment group create --resource-group myurlshortener-dev --template-file infrastructure/main.bicep
```

### Create user for GH Action

in xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx you should copy your subscription id from azure portal

```bash
az ad sp create-for-rbac --name "GitHub-Action-SP" \
    --role contributor \
    --scopes /subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx \
    --sdk-auth
```

### Read publishing-profiles

we use:

```bash
az webapp deployment list-publishing-profiles \
--name api-(write name) \
--resource-group my_resource_group \
--xml
```

and copy all string into git secrets

### Read roleDefinitionId for vault bicep

we use:

```bash
az role definition list --name "Contributor" --query "[].id" -o tsv
```

the id is somethink like this: b24988ac-6180-42a0-ab88-20f7382dd24c
