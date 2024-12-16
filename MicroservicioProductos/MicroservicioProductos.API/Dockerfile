# Imagen base de .NET para construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos .csproj de todos los proyectos
COPY MicroservicioProductos.API/*.csproj MicroservicioProductos.API/
COPY MicroservicioProductos.Application/*.csproj MicroservicioProductos.Application/
COPY MicroservicioProductos.Infrastructure/*.csproj MicroservicioProductos.Infrastructure/
COPY MicroservicioProductos.Domain/*.csproj MicroservicioProductos.Domain/

# Restaurar dependencias
RUN dotnet restore MicroservicioProductos.API/MicroservicioProductos.API.csproj

# Copiar el resto del código fuente
COPY . .

# Publicar el proyecto en la carpeta 'out'
RUN dotnet publish MicroservicioProductos.API/MicroservicioProductos.API.csproj -c Release -o out

# Imagen base de .NET para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto 80
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "MicroservicioProductos.API.dll"]