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
COPY ["Microservices/Product/Product.API/Product.API.csproj", "Microservices/Product/Product.API/"]
COPY ["Microservices/Product/Product.Application/Product.Application.csproj", "Microservices/Product/Product.Application/"]
COPY ["Microservices/Product/Product.Infrastructure/Product.Infrastructure.csproj", "Microservices/Product/Product.Infrastructure/"]
COPY ["Microservices/Product/Product.Core/Product.Core.csproj", "Microservices/Product/Product.Core/"]
RUN dotnet restore "./Microservices/Product/Product.API/Product.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Product/Product.API"
RUN dotnet build "./Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Product.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.API.dll"]