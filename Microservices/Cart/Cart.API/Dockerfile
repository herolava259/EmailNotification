# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservices/Cart/Cart.API/Cart.API.csproj", "Microservices/Cart/Cart.API/"]
COPY ["Microservices/Cart/Cart.Application/Cart.Application.csproj", "Microservices/Cart/Cart.Application/"]
COPY ["Microservices/Cart/Cart.Infrastructure/Cart.Infrastructure.csproj", "Microservices/Cart/Cart.Infrastructure/"]
COPY ["Microservices/Cart/Cart.Core/Cart.Core.csproj", "Microservices/Cart/Cart.Core/"]
RUN dotnet restore "./Microservices/Cart/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Cart/Cart.API"
RUN dotnet build "./Cart.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cart.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]