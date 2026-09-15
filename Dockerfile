FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Build.props", "."]
COPY ["global.json", "."]
COPY ["FieldOps.Bootstrapper/FieldOps.Bootstrapper.csproj", "FieldOps.Bootstrapper/"]
COPY ["FieldOps.Shared.Abstractions/FieldOps.Shared.Abstractions.csproj", "FieldOps.Shared.Abstractions/"]
COPY ["FieldOps.Shared.Infrastructure/FieldOps.Shared.Infrastructure.csproj", "FieldOps.Shared.Infrastructure/"]
COPY ["FieldOps.Modules.Accounts.Api/FieldOps.Modules.Accounts.Api.csproj", "FieldOps.Modules.Accounts.Api/"]
COPY ["FieldOps.Modules.Accounts.Contracts/FieldOps.Modules.Accounts.Contracts.csproj", "FieldOps.Modules.Accounts.Contracts/"]
COPY ["FieldOps.Modules.Accounts.Core/FieldOps.Modules.Accounts.Core.csproj", "FieldOps.Modules.Accounts.Core/"]
COPY ["FieldOps.Modules.Assets.Api/FieldOps.Modules.Assets.Api.csproj", "FieldOps.Modules.Assets.Api/"]
COPY ["FieldOps.Modules.Assets.Contracts/FieldOps.Modules.Assets.Contracts.csproj", "FieldOps.Modules.Assets.Contracts/"]
COPY ["FieldOps.Modules.Assets.Core/FieldOps.Modules.Assets.Core.csproj", "FieldOps.Modules.Assets.Core/"]
COPY ["FieldOps.Modules.Files.Api/FieldOps.Modules.Files.Api.csproj", "FieldOps.Modules.Files.Api/"]
COPY ["FieldOps.Modules.Files.Contracts/FieldOps.Modules.Files.Contracts.csproj", "FieldOps.Modules.Files.Contracts/"]
COPY ["FieldOps.Modules.Files.Core/FieldOps.Modules.Files.Core.csproj", "FieldOps.Modules.Files.Core/"]
COPY ["FieldOps.Modules.Jobs.Api/FieldOps.Modules.Jobs.Api.csproj", "FieldOps.Modules.Jobs.Api/"]
COPY ["FieldOps.Modules.Jobs.Application/FieldOps.Modules.Jobs.Application.csproj", "FieldOps.Modules.Jobs.Application/"]
COPY ["FieldOps.Modules.Jobs.Contracts/FieldOps.Modules.Jobs.Contracts.csproj", "FieldOps.Modules.Jobs.Contracts/"]
COPY ["FieldOps.Modules.Jobs.Domain/FieldOps.Modules.Jobs.Domain.csproj", "FieldOps.Modules.Jobs.Domain/"]
COPY ["FieldOps.Modules.Jobs.Infrastructure/FieldOps.Modules.Jobs.Infrastructure.csproj", "FieldOps.Modules.Jobs.Infrastructure/"]
COPY ["FieldOps.Modules.Operators.Api/FieldOps.Modules.Operators.Api.csproj", "FieldOps.Modules.Operators.Api/"]
COPY ["FieldOps.Modules.Operators.Contracts/FieldOps.Modules.Operators.Contracts.csproj", "FieldOps.Modules.Operators.Contracts/"]
COPY ["FieldOps.Modules.Operators.Core/FieldOps.Modules.Operators.Core.csproj", "FieldOps.Modules.Operators.Core/"]
COPY ["FieldOps.Modules.Reports.Api/FieldOps.Modules.Reports.Api.csproj", "FieldOps.Modules.Reports.Api/"]
COPY ["FieldOps.Modules.Reports.Application/FieldOps.Modules.Reports.Application.csproj", "FieldOps.Modules.Reports.Application/"]
COPY ["FieldOps.Modules.Reports.Contracts/FieldOps.Modules.Reports.Contracts.csproj", "FieldOps.Modules.Reports.Contracts/"]
COPY ["FieldOps.Modules.Reports.Domain/FieldOps.Modules.Reports.Domain.csproj", "FieldOps.Modules.Reports.Domain/"]
COPY ["FieldOps.Modules.Reports.Infrastructure/FieldOps.Modules.Reports.Infrastructure.csproj", "FieldOps.Modules.Reports.Infrastructure/"]
COPY ["FieldOps.Modules.Technicians.Api/FieldOps.Modules.Technicians.Api.csproj", "FieldOps.Modules.Technicians.Api/"]
COPY ["FieldOps.Modules.Technicians.Contracts/FieldOps.Modules.Technicians.Contracts.csproj", "FieldOps.Modules.Technicians.Contracts/"]
COPY ["FieldOps.Modules.Technicians.Core/FieldOps.Modules.Technicians.Core.csproj", "FieldOps.Modules.Technicians.Core/"]
RUN dotnet restore "./FieldOps.Bootstrapper/FieldOps.Bootstrapper.csproj"

COPY . .
WORKDIR "/src/FieldOps.Bootstrapper"
RUN dotnet build "./FieldOps.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FieldOps.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
RUN apt-get update && apt-get install -y --no-install-recommends libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FieldOps.Bootstrapper.dll"]
