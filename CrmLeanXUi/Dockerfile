# Stage 1: Build the Blazor WebAssembly application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Publish the Blazor WebAssembly application
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Serve the published static files using Nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Remove default Nginx configuration and add a custom one
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy the published Blazor WebAssembly files from the build stage
COPY --from=build /app/publish/wwwroot .

# Expose port 80 (Nginx default)
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]