#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StarsFoodAPI/StarsFoodAPI.csproj", "StarsFoodAPI/"]
COPY ["StarFood.Application/StarFood.Application.csproj", "StarFood.Application/"]
COPY ["StarFood.Domain/StarFood.Domain.csproj", "StarFood.Domain/"]
COPY ["StarFood.Infrastructure/StarFood.Infrastructure.csproj", "StarFood.Infrastructure/"]
RUN dotnet restore "StarsFoodAPI/StarsFoodAPI.csproj"
COPY . .
WORKDIR "/src/StarsFoodAPI"
RUN dotnet build "StarsFoodAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarsFoodAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarsFoodAPI.dll"]