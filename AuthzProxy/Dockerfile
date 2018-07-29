FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY AuthzProxy/AuthzProxy.csproj AuthzProxy/
RUN dotnet restore AuthzProxy/AuthzProxy.csproj
COPY . .
WORKDIR /src/AuthzProxy
RUN dotnet build AuthzProxy.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AuthzProxy.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthzProxy.dll"]
