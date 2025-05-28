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
