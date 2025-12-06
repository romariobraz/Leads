# EF Core Migrations


Use the CLI to add and apply migrations. Examples:


```bash
# from solution root (where .sln is):
cd backend/LeadQualifier.Infrastructure
# add migration
dotnet ef migrations add Init_Leads -p ../LeadQualifier.Infrastructure.csproj -s ../LeadQualifier.Api/LeadQualifier.Api.csproj -o Migrations
# apply migrations
dotnet ef database update -p ../LeadQualifier.Infrastructure.csproj -s ../LeadQualifier.Api/LeadQualifier.Api.csproj
```


If you use Visual Studio, ensure the startup project is LeadQualifier.Api and the target project for migrations is LeadQualifier.Infrastructure.