FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node AS node-builder
WORKDIR /node
RUN apt update
RUN apt install -y yarn
RUN npm install typescript
COPY ["Foreca.App.Web/ClientApp", "/node"]
RUN yarn install
RUN yarn build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Foreca.App.Web/Foreca.App.Web.csproj", "Foreca.App.Web/"]
COPY ["Foreca.App/Foreca.App.csproj", "Foreca.App/"]
COPY ["Foreca.Infrastructure/Foreca.Infrastructure.csproj", "Foreca.Infrastructure/"]
COPY ["Foreca.Domain/Foreca.Domain.csproj", "Foreca.Domain/"]
COPY ["Foreca.Shared/Foreca.Shared.csproj", "Foreca.Shared/"]
COPY ["Foreca.Infrastructure.Database/Foreca.Infrastructure.Database.csproj", "Foreca.Infrastructure.Database/"]
RUN dotnet restore "Foreca.App.Web/Foreca.App.Web.csproj"
COPY . .
WORKDIR "/src/Foreca.App.Web"
RUN dotnet build "Foreca.App.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Foreca.App.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node-builder /node/dist ./wwwroot
ENTRYPOINT ["dotnet", "Foreca.App.Web.dll"]
