# Tareas API

API REST para gestión de tareas. Proyecto de aprendizaje y portfolio, construido por niveles de complejidad arquitectónica creciente.

## Stack

- .NET 10
- Entity Framework Core
- SQL Server (en Docker)

## Estado del proyecto

**Level 2 — Clean Architecture** ✅

Arquitectura en cuatro capas con inversión de dependencias:

- **Domain** — entidades del negocio, sin dependencias externas.
- **Application** — casos de uso (handlers), interfaces (`ITareaRepository`), commands y DTOs internos.
- **Infrastructure** — `DbContext`, repositorios, migraciones de EF Core.
- **Api** — controllers HTTP delgados que traducen entre HTTP y casos de uso.

La capa de Api desconoce los detalles de persistencia. Cambiar de SQL Server a otra BD solo requeriría tocar Infrastructure.
## Estructura
```
Tareas.slnx
├── compose.yaml                          # SQL Server 2022
└── src/
    ├── Tareas.Domain/                    # Entidades y enums (sin dependencias)
    │   ├── Entities/
    │   │   └── Tarea.cs
    │   └── Enums/
    │       └── TareaStatus.cs
    │
    ├── Tareas.Application/               # Casos de uso, interfaces, DTOs internos
    │   ├── DTOs/
    │   │   └── TareaDto.cs
    │   ├── Interfaces/
    │   │   └── ITareaRepository.cs
    │   ├── UseCases/
    │   │   └── Tareas/
    │   │       ├── CreateTarea/
    │   │       ├── DeleteTarea/
    │   │       ├── GetAllTareas/
    │   │       ├── GetTareaById/
    │   │       └── UpdateTarea/
    │   └── DependencyInjection.cs
    │
    ├── Tareas.Infrastructure/            # EF Core, repositorios, persistencia
    │   ├── Migrations/
    │   ├── Persistence/
    │   │   ├── TareasDbContext.cs
    │   │   └── TareaRepository.cs
    │   └── DependencyInjection.cs
    │
    └── Tareas.Api/                       # Controllers y composición
        ├── Controllers/
        │   └── TareasController.cs
        ├── DTOs/
        │   └── TareaDtos.cs
        └── Program.cs
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
- **Level 2** — Clean Architecture (Application + Infrastructure) ✅
- **Level 3** — CQRS con MediatR
- **Level 4** — Event-driven
- **Level 5** — Extensiones (IA y arquitectura avanzada) *(opcional)*

## Notas

- La app móvil (Flutter) vive en un repositorio separado.
- Las credenciales nunca se versionan: en desarrollo van en User Secrets, en producción se inyectan por variables de entorno.
