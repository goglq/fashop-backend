#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FashopBackend/FashopBackend.csproj", "FashopBackend/"]
RUN dotnet restore "FashopBackend/FashopBackend.csproj"
COPY . .
WORKDIR "/src/FashopBackend"
RUN dotnet build "FashopBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FashopBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashopBackend.dll"]