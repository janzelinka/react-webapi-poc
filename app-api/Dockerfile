FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migration
WORKDIR /app

COPY app-api/app-api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o /app/build

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef && cd app-api && dotnet ef database update

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=migration /app/build ./

COPY --from=migration /app/app-api/database.db /app/database.db

EXPOSE 5000
EXPOSE 443
EXPOSE 80

ENTRYPOINT ["dotnet", "app-api.dll"]