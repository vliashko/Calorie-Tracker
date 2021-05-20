#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["IngredientMicroService/IngredientMicroService.csproj", "IngredientMicroService/"]
RUN dotnet restore "IngredientMicroService/IngredientMicroService.csproj"
COPY . .
WORKDIR "/src/IngredientMicroService"
RUN dotnet build "IngredientMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IngredientMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IngredientMicroService.dll"]