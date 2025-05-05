# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar el código
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el archivo .csproj y restauramos
COPY PRUEBA_GSE.csproj ./
RUN dotnet restore "PRUEBA_GSE.csproj"

# Copiamos el resto del código y compilamos
COPY . .
RUN dotnet build "PRUEBA_GSE.csproj" -c Release -o /app/build

# Publicamos
FROM build AS publish
RUN dotnet publish "PRUEBA_GSE.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PRUEBA_GSE.dll"]