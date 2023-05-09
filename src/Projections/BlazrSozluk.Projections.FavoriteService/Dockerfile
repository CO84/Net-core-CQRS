#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Projections/BlazrSozluk.Projections.FavoriteService/BlazrSozluk.Projections.FavoriteService.csproj", "src/Projections/BlazrSozluk.Projections.FavoriteService/"]
COPY ["src/Common/BlazorSozluk.Common/BlazorSozluk.Common.csproj", "src/Common/BlazorSozluk.Common/"]
RUN dotnet restore "src/Projections/BlazrSozluk.Projections.FavoriteService/BlazrSozluk.Projections.FavoriteService.csproj"
COPY . .
WORKDIR "/src/src/Projections/BlazrSozluk.Projections.FavoriteService"
RUN dotnet build "BlazrSozluk.Projections.FavoriteService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazrSozluk.Projections.FavoriteService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazrSozluk.Projections.FavoriteService.dll"]