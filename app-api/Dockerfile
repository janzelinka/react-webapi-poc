# Stage 1: Build and Apply Migrations
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migration
WORKDIR /app

# Copy project files and restore dependencies
COPY app-api/app-api.csproj ./
RUN dotnet restore

# Copy remaining source code
COPY . ./
RUN dotnet build -c Release -o /app/build

# Generate SQLite database by running migrations
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef && cd app-api && dotnet ef database update

# Stage 2: Final Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the application files
COPY --from=migration /app/build ./

# Copy the generated SQLite database file
COPY --from=migration /app/app-api/database.db /app/database.db

EXPOSE 5000
EXPOSE 443
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "app-api.dll"]