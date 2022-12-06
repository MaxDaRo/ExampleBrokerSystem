FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./*.csproj ./
RUN dotnet restore

COPY ./ ./
RUN dotnet publish -c Development -o out

FROM base AS final
WORKDIR /app
COPY --from=build /src/out .
EXPOSE 4000
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]