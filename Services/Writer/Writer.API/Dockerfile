#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Services/Writer/Writer.API/Writer.API.csproj", "Services/Writer/Writer.API/"]
RUN dotnet restore "Services/Writer/Writer.API/Writer.API.csproj"
COPY . .
WORKDIR "/src/Services/Writer/Writer.API"
RUN dotnet build "Writer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Writer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Writer.API.dll"]