
Para acceder a la aplicacion publicada: https://neitek.azurewebsites.net/
Necesario generar base con script GestionDataBaseCreate.sql
Cambiar en appssettings.Development el "Server" al que se conectara
"ConnectionStrings": {
  "GestionConnection": "Server=DESKTOP-1USG4GQ; DataBase=Gestion;TrustServerCertificate=true; Trusted_Connection=True;"
}
