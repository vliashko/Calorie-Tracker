#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 3000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["UserMicroService/UserMicroService.csproj", "UserMicroService/"]
RUN dotnet restore "UserMicroService/UserMicroService.csproj"
COPY . .
WORKDIR "/src/UserMicroService"
RUN dotnet build "UserMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserMicroService.dll"]