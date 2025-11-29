############################################################
# Stage 1: Build Blazor WebAssembly
############################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copy project file(s) để restore NuGet
COPY CrmLeanXUi/CrmLeanXUi.csproj ./CrmLeanXUi/

# 2. Restore packages
RUN dotnet restore ./CrmLeanXUi/CrmLeanXUi.csproj

# 3. Copy toàn bộ source code
COPY CrmLeanXUi/. ./CrmLeanXUi/

# 4. Chuyển vào thư mục project
WORKDIR /src/CrmLeanXUi

# 5. Build project
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# 6. Publish project (tối ưu cho Docker)
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

############################################################
# Stage 2: Runtime
############################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 1. Copy output từ stage build
COPY --from=build /app/publish .

# 2. Expose port (nếu cần)
EXPOSE 8080
EXPOSE 8081

# 3. Chạy ứng dụng
ENTRYPOINT ["dotnet", "CrmLeanXUi.dll"]
