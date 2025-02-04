FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebMicroService.csproj", "./"]
RUN dotnet restore "./WebMicroService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebMicroService.dll"] 