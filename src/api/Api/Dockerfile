#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/api/Api/Api.csproj", "src/api/Api/"]
COPY ["src/business/Business/Business.csproj", "src/business/Business/"]
COPY ["src/bot/Bot/Bot.csproj", "src/bot/Bot/"]
COPY ["src/data/Data/Data.csproj", "src/data/Data/"]
RUN dotnet restore "src/api/Api/Api.csproj"
COPY . .
WORKDIR "/src/src/api/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]