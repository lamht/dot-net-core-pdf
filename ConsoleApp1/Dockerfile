FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update && \
    apt-get install -y --no-install-recommends zlib1g fontconfig libfreetype6 libx11-6 libxext6 libxrender1 && \
    curl -o /usr/lib/libwkhtmltox.so --location  https://github.com/rdvojmoc/DinkToPdf/raw/v1.0.8/v0.12.4/64%20bit/libwkhtmltox.so && \
    apt-get install -y libc6-dev libgdiplus
COPY --from=build app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp.dll"]