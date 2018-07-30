FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
COPY packages/ /packages
COPY NuGet.Docker.Config /root/.nuget/NuGet/NuGet.Config
WORKDIR /src
COPY src/AuthzProxy/AuthzProxy.csproj AuthzProxy/
RUN dotnet restore AuthzProxy/AuthzProxy.csproj
COPY src/ .
WORKDIR /src/AuthzProxy
RUN dotnet build AuthzProxy.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AuthzProxy.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthzProxy.dll"]
