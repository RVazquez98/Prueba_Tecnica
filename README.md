# Music School Management API

Esta es una API RESTful desarrollada en ASP.NET Core para la gesti�n de Escuelas de M�sica, Profesores y Alumnos. Soporta operaciones CRUD, asignaciones y consultas avanzadas, utilizando SQL Server y Stored Procedures.

## Requisitos previos

- .NET 6 o superior
- SQL Server o LocalDB
- Visual Studio 2022+ o VS Code
- (Opcional) Postman para pruebas

## Instalaci�n y Primeros Pasos

1. **Clona el repositorio:**
    ```bash
    git clone https://github.com/RVazquez98/Prueba_Tecnica.git
    cd tu-repo
    ```

2. **Configura la cadena de conexi�n en `appsettings.json`:**
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=MusicSchoolDb;Trusted_Connection=True;"
    }
    ```

3. **Restaura los paquetes y compila:**
    ```bash
    dotnet restore
    dotnet build
    ```

4. **Inicializar la base de datos con Entity Framework Core:**

   Antes de ejecutar los scripts de los Stored Procedures, aseg�rate de crear primero la base de datos y las tablas usando Entity Framework Core.

   - Instala la herramienta de EF Core (solo una vez por equipo):
     ```bash
     dotnet tool install --global dotnet-ef
     ```
   - Crea la migraci�n inicial:
     ```bash
     dotnet ef migrations add InitialCreate
     ```
   - Aplica la migraci�n para crear la base y las tablas:
     ```bash
     dotnet ef database update
     ```

   Con estos pasos, la base de datos y las tablas estar�n listas para usar la API.

5. **Agregar Stored Procedures:**

   Despu�s de tener la base de datos y tablas, ejecuta el archivo `stored_procedures.sql` incluido en la ra�z del proyecto sobre la base `MusicSchoolDb` para crear todos los Stored Procedures requeridos.

6. **Ejecutar el proyecto:**
    ```bash
    dotnet run
    ```

El API estar� disponible en `https://localhost:7099` (o el puerto que corresponda).

Puedes probar los endpoints usando **Postman**, **Swagger** o cualquier cliente HTTP.

## Estructura del proyecto

- `Controllers/` � Controladores de la API.
- `Models/` � Clases de datos (DTOs, entidades).
- `Services/` � L�gica de acceso a base de datos.
- `stored_procedures.sql`  � Scripts de los Stored Procedures requeridos.

## Principales endpoints disponibles

| Entidad   | Endpoint                                    | M�todo | Descripci�n                          |
|-----------|---------------------------------------------|--------|--------------------------------------|
| Escuela   | /api/schools                                | POST   | Crear escuela                        |
| Escuela   | /api/schools/{id}                           | PUT    | Actualizar escuela                   |
| Escuela   | /api/schools/{id}                           | DELETE | Eliminar escuela                     |
| Alumno    | /api/students                               | POST   | Crear alumno                         |
| Alumno    | /api/students/{id}                          | PUT    | Actualizar alumno                    |
| Alumno    | /api/students/{id}                          | DELETE | Eliminar alumno                      |
| Profesor  | /api/teachers                               | POST   | Crear profesor                       |
| Profesor  | /api/teachers/{id}                          | PUT    | Actualizar profesor                  |
| Profesor  | /api/teachers/{id}                          | DELETE | Eliminar profesor                    |
| Asignaci�n| /api/studentteachers/assign                 | POST   | Asignar alumno a profesor            |
| Asignaci�n| /api/studentteachers/remove                 | POST   | Quitar alumno de profesor            |
| Consulta alumnos  | /api/studentteachers/by-teacher/{teacherId} | GET    | Alumnos inscritos por profesor |
| Consulta escuelas | /api/teachers/{id}/schools-students         | GET    | Escuelas y alumnos por profesor |

## Notas adicionales

- **Ejecuta primero los scripts de los Stored Procedures despu�s de crear las tablas con EF Core.**
- Si agregas nuevas entidades, recuerda actualizar los SP y el servicio de base de datos.
- Todos los scripts necesarios para las operaciones del sistema se encuentran en el archivo `stored_procedures.txt` en la ra�z del proyecto.

---

**Autor:** Ra�l Ignacio V�zquez L�pez
