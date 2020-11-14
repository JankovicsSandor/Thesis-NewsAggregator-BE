#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Services/News/News.API/News.API.csproj", "Services/News/News.API/"]
COPY ["Services/Shared/NewsAggregator.Shared/NewsAggregator.Shared.csproj", "Services/Shared/NewsAggregator.Shared/"]
COPY ["Services/News/News.Shared/News.Shared.csproj", "Services/News/News.Shared/"]
COPY ["BuilldingBlocks/EventBus/EventBusRabbitMq/EventBusRabbitMQ.csproj", "BuilldingBlocks/EventBus/EventBusRabbitMq/"]
COPY ["Services/News/News.BussinessLogic/News.BussinessLogic.csproj", "Services/News/News.BussinessLogic/"]
COPY ["Services/News/News.DataAccess/News.DataAccess.csproj", "Services/News/News.DataAccess/"]
RUN dotnet restore "Services/News/News.API/News.API.csproj"
COPY . .
WORKDIR "/src/Services/News/News.API"
RUN dotnet build "News.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "News.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "News.API.dll"]