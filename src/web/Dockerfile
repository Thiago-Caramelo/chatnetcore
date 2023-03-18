#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Update package lists and install necessary dependencies
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs

# Install npm
RUN npm install -g npm

WORKDIR /src
COPY ["src/web/ChatNetCore.csproj", "src/web/"]
COPY ["src/data/Data/Data.csproj", "src/data/Data/"]
COPY ["src/business/Business/Business.csproj", "src/business/Business/"]
COPY ["src/bot/Bot/Bot.csproj", "src/bot/Bot/"]
RUN dotnet restore "src/web/ChatNetCore.csproj"
COPY . .
WORKDIR "/src/src/web"
RUN dotnet build "ChatNetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatNetCore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatNetCore.dll"]