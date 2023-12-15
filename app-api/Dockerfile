# Use the appropriate .NET runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the appropriate .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ing-app-api/ing-app-api.csproj", "ing-app-api/"]
RUN dotnet restore "ing-app-api/ing-app-api.csproj"
COPY . .
WORKDIR "/src/ing-app-api"
RUN dotnet build "ing-app-api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ing-app-api.csproj" -c Release -o /app/publish

# Final image for running the published application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ing-app-api.dll"]