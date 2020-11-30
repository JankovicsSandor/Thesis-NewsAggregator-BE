#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Services/ResourceConfiguration/ResourceConfiguration.BackgroundJob/ResourceConfiguration.BackgroundJob.csproj", "Services/ResourceConfiguration/ResourceConfiguration.BackgroundJob/"]
RUN dotnet restore "Services/ResourceConfiguration/ResourceConfiguration.BackgroundJob/ResourceConfiguration.BackgroundJob.csproj"
COPY . .
WORKDIR "/src/Services/ResourceConfiguration/ResourceConfiguration.BackgroundJob"
RUN dotnet build "ResourceConfiguration.BackgroundJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ResourceConfiguration.BackgroundJob.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResourceConfiguration.BackgroundJob.dll"]