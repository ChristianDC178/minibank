
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY /libs /libs
COPY /Minibank.CustomersSrv/service .

ARG csproj_path=MiniBank.CustomersSrv.Api/MiniBank.CustomersSrv.Api.csproj
RUN echo $csproj_path
RUN dotnet restore $csproj_path
COPY . .
WORKDIR "/src/."
RUN dotnet build "MiniBank.CustomersSrv.Api/MiniBank.CustomersSrv.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MiniBank.CustomersSrv.Api/MiniBank.CustomersSrv.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["MiniBank.CustomersSrv.Api.dll"]