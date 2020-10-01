#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Wings.Saas/Server/Wings.Saas.Server.csproj", "Wings.Saas/Server/"]
COPY ["Wings.Saas/Client/Wings.Saas.Client.csproj", "Wings.Saas/Client/"]
COPY ["Wings.Saas/Shared/Wings.Saas.Shared.csproj", "Wings.Saas/Shared/"]
RUN dotnet restore "Wings.Saas/Server/Wings.Saas.Server.csproj"
COPY . .
WORKDIR "/src/Wings.Saas/Server"
RUN dotnet build "Wings.Saas.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wings.Saas.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wings.Saas.Server.dll"]