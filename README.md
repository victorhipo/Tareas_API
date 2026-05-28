# Tareas API

API REST para gestión de tareas. Proyecto de aprendizaje y portfolio, construido por niveles de complejidad arquitectónica creciente.

## Stack

- .NET 10
- Entity Framework Core
- SQL Server (en Docker)

## Estado del proyecto

**Level 1 — Minimal REST API** ✅

CRUD completo de tareas sobre una arquitectura de dos proyectos (`Api` + `Domain`), con el dominio sin dependencias externas como base para los siguientes niveles.

## Estructura
```
Tareas.sln
├── docker-compose.yml      # SQL Server 2022
└── src/
├── Tareas.Api/             # Web API: controllers, EF Core, DI
│   ├── Controllers/
│   ├── Data/               # DbContext
│   ├── DTOs/
│   └── Migrations/
└── Tareas.Domain/          # Entidades y enums (sin dependencias)
├── Entities/
└── Enums/
```
## Puesta en marcha

### 1. Levantar SQL Server

```bash
docker compose up -d
```

SQL Server queda escuchando en `localhost:1434` (mapeado al 1433 interno del contenedor).

### 2. Configurar la connection string

La cadena de conexión se gestiona con User Secrets para no exponer credenciales en el repositorio:

```bash
cd src/Tareas.Api
dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost,1434;Database=TareasDb;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True;"
```

La password debe coincidir con la definida en `docker-compose.yml`.

### 3. Aplicar las migraciones

```bash
dotnet ef database update
```

(En entorno de desarrollo, las migraciones también se aplican automáticamente al arrancar.)

### 4. Ejecutar la API

```bash
dotnet run
```

La API queda escuchando en la URL que indique la consola (p. ej. `http://localhost:5184`). El contrato OpenAPI se expone en `/openapi/v1.json` y puede importarse en Postman, Insomnia u otras herramientas.

## Endpoints

| Método | Ruta               | Descripción         |
|--------|--------------------|---------------------|
| GET    | `/api/tareas`      | Listar todas        |
| GET    | `/api/tareas/{id}` | Obtener una         |
| POST   | `/api/tareas`      | Crear               |
| PUT    | `/api/tareas/{id}` | Actualizar          |
| DELETE | `/api/tareas/{id}` | Borrar              |

## Modelo: `Tarea`

| Campo         | Tipo                                                        |
|---------------|-------------------------------------------------------------|
| `Id`          | `Guid`                                                      |
| `Title`       | `string` (requerido, máx. 200)                              |
| `Description` | `string?` (máx. 2000)                                       |
| `DueDate`     | `DateTime?`                                                 |
| `Status`      | `enum` (Pendiente / EnProgreso / Completada / Cancelada)    |
| `CreatedAt`   | `DateTime` (UTC)                                            |

## Roadmap

- **Level 1** — Minimal REST API ✅
- **Level 2** — Clean Architecture (Application + Infrastructure)
- **Level 3** — CQRS
- **Level 4** — Event-driven
- **Level 5** — Extensiones (IA y arquitectura avanzada) *(opcional)*

## Notas

- La app móvil (Flutter) vive en un repositorio separado.
- Las credenciales nunca se versionan: en desarrollo van en User Secrets, en producción se inyectan por variables de entorno.