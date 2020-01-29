FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["KingCal/KingCal.csproj", "KingCal/"]
RUN dotnet restore "KingCal/KingCal.csproj"
COPY . .
WORKDIR "/src/KingCal"
RUN dotnet build "KingCal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KingCal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KingCal.dll"]