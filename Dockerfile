FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DynDNSNet/DynDNSNet.csproj", "DynDNSNet/"]
RUN dotnet restore "DynDNSNet/DynDNSNet.csproj"
COPY . .
WORKDIR "/src/DynDNSNet"
RUN dotnet build "DynDNSNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynDNSNet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynDNSNet.dll"]
