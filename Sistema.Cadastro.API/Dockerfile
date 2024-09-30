# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sistema.Cadastro.API/Sistema.Cadastro.API.csproj", "Sistema.Cadastro.API/"]
COPY ["Sistema.Cadastro.CrossCutting.IoC/Sistema.Cadastro.CrossCutting.IoC.csproj", "Sistema.Cadastro.CrossCutting.IoC/"]
COPY ["Sistema.Cadastro.Application/Sistema.Cadastro.Application.csproj", "Sistema.Cadastro.Application/"]
COPY ["Sistema.Cadastro.CrossCutting/Sistema.Cadastro.CrossCutting.csproj", "Sistema.Cadastro.CrossCutting/"]
COPY ["Sistema.Cadastro.Domain/Sistema.Cadastro.Domain.csproj", "Sistema.Cadastro.Domain/"]
COPY ["Sistema.Cadastro.Infrastructure/Sistema.Cadastro.Infrastructure.csproj", "Sistema.Cadastro.Infrastructure/"]
COPY ["Sistema.Cadastro.Infrastructure.ExternalServices/Sistema.Cadastro.Infrastructure.ExternalServices.csproj", "Sistema.Cadastro.Infrastructure.ExternalServices/"]
RUN dotnet restore "./Sistema.Cadastro.API/Sistema.Cadastro.API.csproj"
COPY . .
WORKDIR "/src/Sistema.Cadastro.API"
RUN dotnet build "./Sistema.Cadastro.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sistema.Cadastro.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sistema.Cadastro.API.dll"]