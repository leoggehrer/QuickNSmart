#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["QuickNSmart.WebApi/QuickNSmart.WebApi.csproj", "QuickNSmart.WebApi/"]
COPY ["QuickNSmart.Contracts/QuickNSmart.Contracts.csproj", "QuickNSmart.Contracts/"]
COPY ["CommonBase/CommonBase.csproj", "CommonBase/"]
COPY ["QuickNSmart.Transfer/QuickNSmart.Transfer.csproj", "QuickNSmart.Transfer/"]
COPY ["QuickNSmart.Logic/QuickNSmart.Logic.csproj", "QuickNSmart.Logic/"]
RUN dotnet restore "QuickNSmart.WebApi/QuickNSmart.WebApi.csproj"
COPY . .
WORKDIR "/src/QuickNSmart.WebApi"
RUN dotnet build "QuickNSmart.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuickNSmart.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickNSmart.WebApi.dll"]