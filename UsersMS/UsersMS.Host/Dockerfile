#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UsersMS.Host/UsersMS.Host.csproj", "UsersMS.Host/"]
RUN dotnet restore "UsersMS.Host/UsersMS.Host.csproj"
COPY . .
WORKDIR "/src/UsersMS.Host"
RUN dotnet build "UsersMS.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersMS.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersMS.Host.dll"]