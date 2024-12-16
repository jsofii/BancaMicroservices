# Imagen base de .NET para construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos .csproj de todos los proyectos
COPY MicroservicioClientes.API/*.csproj MicroservicioClientes.API/
COPY MicroservicioClientes.Application/*.csproj MicroservicioClientes.Application/
COPY MicroservicioClientes.Infrastructure/*.csproj MicroservicioClientes.Infrastructure/
COPY MicroservicioClientes.Domain/*.csproj MicroservicioClientes.Domain/

# Restaurar dependencias
RUN dotnet restore MicroservicioClientes.API/MicroservicioClientes.API.csproj

# Copiar el resto del código fuente
COPY . .

# Publicar el proyecto en la carpeta 'out'
RUN dotnet publish MicroservicioClientes.API/MicroservicioClientes.API.csproj -c Release -o out

# Imagen base de .NET para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto 80
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "MicroservicioClientes.API.dll"]
