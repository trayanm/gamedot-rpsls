#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GameDot.Api/GameDot.Api.csproj", "GameDot.Api/"]
COPY ["GameDot.Application/GameDot.Application.csproj", "GameDot.Application/"]
COPY ["GameDot.Core/GameDot.Core.csproj", "GameDot.Core/"]
COPY ["GameDot.Infrastructure/GameDot.Infrastructure.csproj", "GameDot.Infrastructure/"]
RUN dotnet restore "GameDot.Api/GameDot.Api.csproj"
COPY . .
WORKDIR "/src/GameDot.Api"
RUN dotnet build "GameDot.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameDot.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameDot.Api.dll"]