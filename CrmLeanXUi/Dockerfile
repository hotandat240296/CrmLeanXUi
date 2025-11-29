# Build stage: use official .NET 8 SDK to restore and publish the Blazor WebAssembly app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["CrmLeanXUi/CrmLeanXUi.csproj", "CrmLeanXUi/"]
RUN dotnet restore "CrmLeanXUi/CrmLeanXUi.csproj"

# Copy the rest of the application code
COPY . .

# Publish the Blazor WebAssembly app (produces static files in /app/publish/wwwroot)
WORKDIR "/src/CrmLeanXUi"
RUN dotnet publish "CrmLeanXUi.csproj" -c Release -o /app/publish

# Runtime stage: serve the static files with Caddy (no nginx)
FROM caddy:2-alpine AS runtime

# Clean default content and copy published Blazor static files
RUN rm -rf /srv/*
COPY --from=build /app/publish/wwwroot/ /srv/

# Create a minimal Caddyfile to serve static files on port 80
RUN printf '%s\n' ':80' 'root * /srv' 'file_server' > /etc/caddy/Caddyfile \
    && apk add --no-cache curl

# Expose HTTP port
EXPOSE 80

# Simple healthcheck using curl
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s \
  CMD curl -f http://localhost/ || exit 1

# Run Caddy in foreground using the configured Caddyfile
CMD ["caddy", "run", "--config", "/etc/caddy/Caddyfile", "--adapter", "caddyfile"]
