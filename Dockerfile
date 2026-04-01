# Multi-stage Dockerfile for .NET 8 ASP.NET Core MVC (production-ready)

# 1) Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Ecommerce.csproj .
# copy any projects if needed
COPY ./*.csproj ./

RUN dotnet restore --disable-parallel

# copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# 2) Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_PRINT_TELEMETRY_MESSAGE=false

# Expose port (container-level), actual port will come from Railway's $PORT
EXPOSE 80

# copy published output
COPY --from=build /app/publish ./

# Use non-root user (optional but recommended)
# Create appuser with minimal privileges
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "MetaVerse.dll"]
