# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ThundersTecnologia.Api/ThundersTecnologia.Api.csproj", "src/ThundersTecnologia.Api/"]
COPY ["src/ThundersTecnologia.Business/ThundersTecnologia.Business.csproj", "src/ThundersTecnologia.Business/"]
COPY ["src/ThundersTecnologia.Data/ThundersTecnologia.Data.csproj", "src/ThundersTecnologia.Data/"]
RUN dotnet restore "./src/ThundersTecnologia.Api/ThundersTecnologia.Api.csproj"
COPY . .
WORKDIR "/src/src/ThundersTecnologia.Api"
RUN dotnet build "./ThundersTecnologia.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ThundersTecnologia.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThundersTecnologia.Api.dll"]