FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MarketAPI/Market.API.csproj", "Market.API/"]
COPY ["Market.Application/Market.Application.csproj", "Market.Application/"]
COPY ["Market.Domain/Market.Domain.csproj", "Market.Domain/"]
COPY ["Market.Infrastructure/Market.Infrastructure.csproj", "Market.Infrastructure/"]
RUN dotnet restore "MarketAPI/Market.API.csproj"
COPY . .
WORKDIR "D:\Projects\MarketApi"
RUN dotnet build "Market.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Market.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Market.API.dll"]
