# Cavipetrol Test

> Buscador de clientes por número de identificación

## 🚀 Requisitos Previos

Antes de instalar, asegúrate de tener:
* **Lenguaje:** Node.js v22+
* **Gestor:** npm 12.0.2
* **Herramientas:** Git, .NET 10, Visual Studio Code, Visual Studio Comunity, SQL Server v2019+

## 🛠️ Instalación

Sigue estos pasos para configurar el proyecto frontend localmente:

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Blast-Seal/CavipetrolTest.git
   ```
2. Entra al directorio:
   ```bash
   cd CavipetrolTest
   cd Frontend
   ```
3. Instala las dependencias:
   ```bash
   npm install
   ```

## 💻 Modo de Uso

Consideraciones:

1. Se deben ejecutar ambos proyectos en primer orden Backend por la configuracion con base de datos, por ultimo frontend.

A continuación las instrucciones rápidas para ejecutar el proyecto backend:

## ⚙️ Configuración

Si el proyecto usa variables de entorno:
1. Configurar la cadena de conexion con la Base de datos SQL Server en el archivo appsettings.Development
   * `DefaultDBConnection`: Data Source=localhost\\SQLEXPRESS;Initial Catalog=basededatos;User ID=usuario;Password=clave; MultipleActiveResultSets=True; TrustServerCertificate=True; Trusted_Connection=SSPI

2. Reemplaza los datos de conexión con tu motor de base de datos

### Ejecución básica Backend
1. En la terminal entra al directorio:
```bash
    cd CavipetrolTest
    cd ApiClientes
    cd CavipetrolTestBack
    cd CavipetrolTestBack.API
```

2. Ejecuta el siguiente comando:
```bash
    dotnet run --launch-profile "Development"
```

3. El sistema automaticamente se conectara a la base de datos y creara la base de datos

4. Ejecutar en base de datos el script adjunto ubicado en (/Backend/SQL Server/Scripts/StoreProcedure.sql)

5. Validar que se encuentre el servicio backend ejecutandose por el puerto (5001)

```bash
    https://localhost:5001/swagger/index.html
```
6. Hacer uso del endpoint de creación de Cliente a través de SWAGGER ("/api/Cliente/New") para registrar clientes de prueba, modifica y envia los datos de Identificacion, Nombre, Apellido y Email


### Ejecución básica Frontend
1. En la terminal entra al directorio:
```bash
    cd CavipetrolTest
    cd ClientesFrontend
    cd CavipetrolTestFront
```

2. Ejecuta el siguiente comando para instalar las dependencias:
```bash
    npm install
```
3. Ejecuta el siguiente comando para arrancar el servicio en modo desarrollo:
```bash
    npm run start
```

4. El proyecto subira localmente por el puerto Angular por defecto (4200)
```bash
    http://localhost:4200/
```

5. Validar el funcionamiento del buscador de Clientes diligenciando en pantalla la identificacion del cliente.