#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/PermissionManagement/PermissionManagementAPI/PermissionManagementAPI.csproj", "Services/PermissionManagement/PermissionManagementAPI/"]
COPY ["Services/PermissionManagement/Permissions.Application/Permissions.Application.csproj", "Services/PermissionManagement/Permissions.Application/"]
COPY ["Services/PermissionManagement/Permissions.Domain/Permissions.Domain.csproj", "Services/PermissionManagement/Permissions.Domain/"]
COPY ["Services/PermissionManagement/Permissions.Infrastructure/Permissions.Infrastructure.csproj", "Services/PermissionManagement/Permissions.Infrastructure/"]
RUN dotnet restore "./Services/PermissionManagement/PermissionManagementAPI/PermissionManagementAPI.csproj"
COPY . .
WORKDIR "/src/Services/PermissionManagement/PermissionManagementAPI"
RUN dotnet build "./PermissionManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PermissionManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermissionManagementAPI.dll"]