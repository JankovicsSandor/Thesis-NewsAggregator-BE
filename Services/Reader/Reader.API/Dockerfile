#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["Services/Reader/Reader.API/Reader.API.csproj", "Services/Reader/Reader.API/"]
RUN dotnet restore "Services/Reader/Reader.API/Reader.API.csproj"
COPY . .
WORKDIR "/src/Services/Reader/Reader.API"
RUN dotnet build "Reader.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reader.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reader.API.dll"]