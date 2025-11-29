# PSEUDOCODE / PLAN (detailed)
# 1. Use .NET 8 SDK image as build stage to compile and publish the Blazor WebAssembly project.
#    - Set BUILD_CONFIGURATION arg (default Release).
#    - Copy the project file first to leverage Docker layer caching for restore.
#    - Run `dotnet restore` on the project.
#    - Copy the remaining source and run `dotnet publish` with UseAppHost=false to produce static files under /app/publish/wwwroot.
# 2. Use a small nginx base image in the final stage to serve static files:
#    - Create an nginx configuration that listens on ports 8080 and 8081 (both exposed).
#    - Copy the published `wwwroot` content into nginx's html folder.
#    - Start nginx in the foreground.
# 3. Expose ports and provide a HEALTHCHECK for basic readiness (optional, simple curl).
#
# Notes:
# - This Dockerfile assumes the project is a standalone Blazor WebAssembly project (Sdk="Microsoft.NET.Sdk.BlazorWebAssembly")
#   and that the publish output contains the compiled client artifacts under `wwwroot`.
# - If this project is ASP.NET Core hosted (server + client), use an ASP.NET runtime image and copy the server publish instead.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the csproj first for better cache use
COPY ["CrmLeanXUi.csproj", "./"]
# Restore packages
RUN dotnet restore "./CrmLeanXUi.csproj"

# Copy everything and publish
COPY . .
RUN dotnet publish "./CrmLeanXUi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: serve static files with nginx
FROM nginx:stable-alpine AS final
# Expose same ports as original Dockerfile intent
EXPOSE 8080
EXPOSE 8081

# Replace default nginx.conf to listen on 8080 and 8081 and serve from /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
RUN cat > /etc/nginx/conf.d/default.conf <<'NGINX_CONF'
server {
    listen 8080;
    listen 8081;

    server_name _;

    root /usr/share/nginx/html;
    index index.html;

    # Serve static files and fallback to index.html for client-side routing
    location / {
        try_files $uri $uri/ /index.html;
    }

    # Optional: small cache for static assets
    location ~* \.(?:css|js|json|html|xml|jpg|jpeg|gif|png|svg|ico|woff2?|ttf|eot)$ {
        expires 30d;
        add_header Cache-Control "public";
    }
}
NGINX_CONF

# Copy published Blazor WebAssembly static files into nginx html folder
# The Blazor publish puts runtime files under /app/publish/wwwroot
COPY --from=build /app/publish/wwwroot/ /usr/share/nginx/html/

# Simple healthcheck: verify nginx serves index.html on 8080
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 CMD wget -qO- --tries=1 --timeout=2 http://127.0.0.1:8080/index.html >/dev/null || exit 1

# Run nginx in foreground
CMD ["nginx", "-g", "daemon off;"]