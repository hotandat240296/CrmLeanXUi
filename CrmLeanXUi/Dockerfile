# Stage 1: Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file
COPY CrmLeanXUi/CrmLeanXUi.csproj ./CrmLeanXUi/

# Restore packages
RUN dotnet restore ./CrmLeanXUi/CrmLeanXUi.csproj

# Copy toàn bộ source code
COPY CrmLeanXUi/CrmLeanXUi.csproj ./CrmLeanXUi/ # optional, đã copy trên
COPY CrmLeanXUi/. ./CrmLeanXUi/

# Build
RUN dotnet build ./CrmLeanXUi/CrmLeanXUi.csproj -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./CrmLeanXUi/CrmLeanXUi.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrmLeanXUi.dll"]
