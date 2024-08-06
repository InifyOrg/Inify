#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlockchainParsersMS.Host/BlockchainParsersMS.Host.csproj", "BlockchainParsersMS.Host/"]
RUN dotnet restore "BlockchainParsersMS.Host/BlockchainParsersMS.Host.csproj"
COPY . .
WORKDIR "/src/BlockchainParsersMS.Host"
RUN dotnet build "BlockchainParsersMS.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlockchainParsersMS.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlockchainParsersMS.Host.dll"]