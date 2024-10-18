# test_coink
repositorio para la prueba de seleccion coink developer
## Cómo correr el proyecto .NET 8

Para correr este proyecto, sigue los siguientes pasos:

1. **Clona el repositorio**:
    ```bash
    git clone <URL_DEL_REPOSITORIO>
    cd <NOMBRE_DEL_REPOSITORIO>
    ```

2. **Instala las dependencias**:
    Asegúrate de tener instalado .NET 8. Puedes descargarlo desde [aquí](https://dotnet.microsoft.com/download/dotnet/8.0).

3. **Restaura las dependencias**:
    ```bash
    dotnet restore
    ```

4. **Compila el proyecto**:
    ```bash
    dotnet build
    ```

5. **Corre el proyecto**:
    ```bash
    dotnet run
    ```

6. **Ejecuta las pruebas**:
    ```bash
    dotnet test
    ```

¡Y eso es todo! Ahora deberías poder ver el proyecto corriendo en tu máquina local.

## Restaurar la base de datos PostgreSQL

Para restaurar el archivo `coink.sql` en tu base de datos PostgreSQL, sigue estos pasos:

1. **Asegúrate de tener PostgreSQL instalado**:
    Puedes descargarlo desde [aquí](https://www.postgresql.org/download/).

2. **Crea una nueva base de datos**:
    ```bash
    createdb prueba_coink
    ```

3. **Restaura el archivo `coink.sql`**:
    ```bash
    psql -d prueba_coink -f /ruta/al/archivo/coink.sql
    ```

4. **Verifica la restauración**:
    Puedes conectarte a la base de datos y listar las tablas para asegurarte de que la restauración fue exitosa:
    ```bash
    psql -d prueba_coink
    \dt
    ```

¡Y eso es todo! Ahora deberías tener la base de datos restaurada y lista para usar.