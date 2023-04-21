# dotnet 7 CLI App
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app
COPY . /app

RUN dotnet publish -c Release -o out

# Runtime Image
FROM mcr.microsoft.com/dotnet/runtime:7.0

WORKDIR /app
COPY --from=build /app/out ./

COPY examples.txt ./
COPY prompt.txt ./

ENTRYPOINT ["dotnet", "clichat.dll"]