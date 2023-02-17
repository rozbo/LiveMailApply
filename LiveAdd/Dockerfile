FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LiveAdd/LiveAdd.csproj", "LiveAdd/"]
RUN dotnet restore "LiveAdd/LiveAdd.csproj"
COPY . .
WORKDIR "/src/LiveAdd"
RUN dotnet build "LiveAdd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LiveAdd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LiveAdd.dll"]
