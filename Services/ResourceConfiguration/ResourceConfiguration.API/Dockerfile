#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/ResourceConfiguration/ResourceConfiguration.API/ResourceConfiguration.API.csproj", "Services/ResourceConfiguration/ResourceConfiguration.API/"]
COPY ["Services/ResourceConfiguration/ResourceConfigurator.Shared/ResourceConfigurator.Shared.csproj", "Services/ResourceConfiguration/ResourceConfigurator.Shared/"]
COPY ["Services/ResourceConfiguration/ResourceConfigurator.DataAccess/ResourceConfigurator.DataAccess.csproj", "Services/ResourceConfiguration/ResourceConfigurator.DataAccess/"]
COPY ["Services/ResourceConfiguration/ResourceConfiguration.BackgroundJob/ResourceConfiguration.BackgroundJob.csproj", "Services/ResourceConfiguration/ResourceConfiguration.BackgroundJob/"]
COPY ["Services/ResourceConfiguration/ResourceConfigurator.NetworkClient/ResourceConfigurator.NetworkClient.csproj", "Services/ResourceConfiguration/ResourceConfigurator.NetworkClient/"]
RUN dotnet restore "Services/ResourceConfiguration/ResourceConfiguration.API/ResourceConfiguration.API.csproj"
COPY . .
WORKDIR "/src/Services/ResourceConfiguration/ResourceConfiguration.API"
RUN dotnet build "ResourceConfiguration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ResourceConfiguration.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResourceConfiguration.API.dll"]