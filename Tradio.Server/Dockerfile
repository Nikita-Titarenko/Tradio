FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Tradio.Server/Tradio.Server.csproj", "Tradio.Server/"]
COPY ["Tradio.Application/Tradio.Application.csproj", "Tradio.Application/"]
COPY ["Tradio.Domain/Tradio.Domain.csproj", "Tradio.Domain/"]
COPY ["Tradio.Infrastructure/Tradio.Infrastructure.csproj", "Tradio.Infrastructure/"]

RUN dotnet restore "Tradio.Server/Tradio.Server.csproj"

COPY . .

WORKDIR "/src/Tradio.Server"
RUN dotnet publish "Tradio.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Tradio.Server.dll"]