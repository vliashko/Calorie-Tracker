#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CaloriesTracker.Api/CaloriesTracker.Api.csproj", "CaloriesTracker.Api/"]
COPY ["CaloriesTracker.Services/CaloriesTracker.Services.csproj", "CaloriesTracker.Services/"]
COPY ["CaloriesTracker.LoggerService/CaloriesTracker.LoggerService.csproj", "CaloriesTracker.LoggerService/"]
COPY ["CaloriesTracker.Contracts/CaloriesTracker.Contracts.csproj", "CaloriesTracker.Contracts/"]
COPY ["CaloriesTracker.Entities/CaloriesTracker.Entities.csproj", "CaloriesTracker.Entities/"]
COPY ["CaloriesTracker.Repositories/CaloriesTracker.Repositories.csproj", "CaloriesTracker.Repositories/"]
RUN dotnet restore "CaloriesTracker.Api/CaloriesTracker.Api.csproj"
COPY . .
WORKDIR "/src/CaloriesTracker.Api"
RUN dotnet build "CaloriesTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CaloriesTracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CaloriesTracker.Api.dll"]