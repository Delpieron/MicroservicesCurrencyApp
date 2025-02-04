FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PdfGeneratorMicroService.csproj", "./"]
RUN dotnet restore "./PdfGeneratorMicroService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PdfGeneratorMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PdfGeneratorMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfGeneratorMicroService.dll"]