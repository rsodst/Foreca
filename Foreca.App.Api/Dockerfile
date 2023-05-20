FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Foreca.App.Api/Foreca.App.Api.csproj", "Foreca.App.Api/"]
COPY ["Foreca.Infrastructure.Database/Foreca.Infrastructure.Database.csproj", "Foreca.Infrastructure.Database/"]
COPY ["Foreca.Infrastructure/Foreca.Infrastructure.csproj", "Foreca.Infrastructure/"]
COPY ["Foreca.Domain/Foreca.Domain.csproj", "Foreca.Domain/"]
COPY ["Foreca.Shared/Foreca.Shared.csproj", "Foreca.Shared/"]
COPY ["Foreca.App/Foreca.App.csproj", "Foreca.App/"]
RUN dotnet restore "Foreca.App.Api/Foreca.App.Api.csproj"
COPY . .
WORKDIR "/src/Foreca.App.Api"
RUN dotnet build "Foreca.App.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foreca.App.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Foreca.App.Api.dll"]
