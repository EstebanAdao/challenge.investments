#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY challenge.investments.api/*.csproj ./challenge.investments.api/
COPY challenge.investments.service/*.csproj ./challenge.investments.service/
COPY challenge.investments.infrastructure/*.csproj ./challenge.investments.infrastructure/
COPY challenge.investments.domain/*.csproj ./challenge.investments.domain/
COPY challenge.investments.tests.unit/*.csproj ./challenge.investments.tests.unit/
COPY challenge.investments.tests.integration/*.csproj ./challenge.investments.tests.integration/
RUN dotnet restore
COPY . /src
WORKDIR "/src/challenge.investments.api"
RUN dotnet build "challenge.investments.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "challenge.investments.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "challenge.investments.api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT ASPNETCORE_ENVIRONMENT=Production dotnet challenge.investments.api.dll