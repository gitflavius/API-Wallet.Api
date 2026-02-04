# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo csproj e restaura dependências
COPY ["Wallet.Api.csproj", "./"]
RUN dotnet restore "Wallet.Api.csproj"

# Copia o restante do código
COPY . .
RUN dotnet publish "Wallet.Api.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Wallet.Api.dll"]
