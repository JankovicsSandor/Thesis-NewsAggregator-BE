#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["NewsReader.API/NewsReader.API.csproj", "NewsReader.API/"]
RUN dotnet restore "NewsReader.API/NewsReader.API.csproj"
COPY . .
WORKDIR "/src/NewsReader.API"
RUN dotnet build "NewsReader.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsReader.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsReader.API.dll"]