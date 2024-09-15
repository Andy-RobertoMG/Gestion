# Guía de Acceso a la Aplicación

## Acceso a la Aplicación

La aplicación está disponible en el siguiente enlace:
[https://neitek.azurewebsites.net/](https://neitek.azurewebsites.net/)

## Preparación de la Base de Datos

Antes de ejecutar la aplicación, es necesario preparar la base de datos. Utiliza el script `GestionDataBaseCreate.sql` para crear la base de datos.

## Configuración de la Conexión

Modifica el archivo `appsettings.Development.json` para configurar la cadena de conexión a la base de datos. Asegúrate de actualizar el valor de `Server` en la sección `ConnectionStrings` según corresponda.

Ejemplo de configuración:

```json
{
  "ConnectionStrings": {
    "GestionConnection": "Server=DESKTOP-1USG4GQ; DataBase=Gestion; TrustServerCertificate=true; Trusted_Connection=True;"
  }
}
