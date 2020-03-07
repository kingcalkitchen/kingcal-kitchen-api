#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["KingCal/KingCal.csproj", "KingCal/"]
COPY ["KingCal.Common/KingCal.Common.csproj", "KingCal.Common/"]
COPY ["KingCal.Service.Items/KingCal.Service.Item.csproj", "KingCal.Service.Items/"]
COPY ["KingCal.Data/KingCal.Data.csproj", "KingCal.Data/"]
COPY ["KingCal.Service.Address/KingCal.Service.Address.csproj", "KingCal.Service.Address/"]
COPY ["KingCal.Service.User/KingCal.Service.User.csproj", "KingCal.Service.User/"]
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