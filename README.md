# EPS Admissions - Prueba de Concepto (PoC)

## Descripción

Este proyecto corresponde a una **Prueba de Concepto (PoC)** desarrollada con **.NET 10**, cuyo objetivo es demostrar una solución para el proceso de admisión de pacientes aplicando principios de **Clean Architecture**, persistencia políglota y procesamiento de eventos en tiempo real.

La solución fue diseñada para evidenciar:

* Separación de responsabilidades mediante Clean Architecture.
* Persistencia políglota utilizando SQL Server y MongoDB.
* Consistencia eventual mediante el patrón **Outbox**.
* Notificaciones en tiempo real utilizando **SignalR**.
* Una arquitectura desacoplada, mantenible y fácilmente extensible.

---

# Arquitectura de la Solución

La solución está organizada siguiendo los principios de **Clean Architecture**, separando las responsabilidades en diferentes proyectos.

```text
EpsAdmissions
│
├── EpsAdmissions.Api
├── EpsAdmissions.Application
├── EpsAdmissions.Domain
├── EpsAdmissions.Infrastructure
└── EpsAdmissions.Blazor
```

## Responsabilidad de cada proyecto

| Proyecto                         | Responsabilidad                                                                    |
| -------------------------------- | ---------------------------------------------------------------------------------- |
| **EpsAdmissions.Domain**         | Entidades del dominio, eventos de dominio y contratos principales.                 |
| **EpsAdmissions.Application**    | Casos de uso, DTOs, interfaces y orquestación de la lógica de negocio.             |
| **EpsAdmissions.Infrastructure** | Implementación de repositorios, Entity Framework Core, MongoDB y patrón Outbox.    |
| **EpsAdmissions.Api**            | API REST, configuración de dependencias y publicación de eventos mediante SignalR. |
| **EpsAdmissions.Blazor**         | Dashboard para visualizar en tiempo real las admisiones registradas.               |

---

# Tecnologías Utilizadas

* .NET 10
* ASP.NET Core Web API
* Blazor Server
* Entity Framework Core
* SQL Server
* MongoDB
* SignalR
* Docker
* Clean Architecture
* Patrón Outbox

---

# Diagrama Mermaid
   ```mermaid
flowchart LR

    UI[Blazor Dashboard]

    API[ASP.NET Core API]

    APP[Application Layer]

    DOMAIN[Domain Layer]

    INFRA[Infrastructure Layer]

    SQL[(SQL Server)]

    MONGO[(MongoDB)]

    OUTBOX[(Outbox)]

    BG[Background Service]

    HUB[SignalR Hub]

    UI -->|HTTP| API

    API --> APP

    APP --> DOMAIN

    APP --> INFRA

    INFRA --> SQL

    INFRA --> MONGO

    INFRA --> OUTBOX

    BG --> OUTBOX

    BG --> HUB

    HUB -->|Tiempo Real| UI
```
---

# Estrategia de Persistencia

La solución implementa un enfoque de **persistencia políglota**, donde cada tecnología es utilizada según sus fortalezas.

## SQL Server

Se utiliza para almacenar la información transaccional del proceso de admisión.

* Admisiones
* Mensajes del Outbox

## MongoDB

Se utiliza para almacenar la historia clínica del paciente como documento, aprovechando la flexibilidad del modelo documental.

---

# Flujo de la Solución

```text
Cliente
   │
   ▼
POST /api/admissions
   │
   ▼
Guardar Admisión (SQL Server)
   │
   ▼
Guardar Historia Clínica (MongoDB)
   │
   ▼
Registrar Evento en Outbox
   │
   ▼
BackgroundService procesa eventos pendientes
   │
   ▼
SignalR publica el evento
   │
   ▼
Dashboard Blazor recibe la notificación en tiempo real
```

---

# Requisitos

Antes de ejecutar el proyecto asegúrese de contar con:

* .NET 10 SDK
* Docker Desktop
* Git

---

# Ejecución del Proyecto

## 1. Clonar el repositorio

```bash
git clone https://github.com/Jgomezdesarrollador/eps-admisiones-dig-web.git
```

## 2. Ingresar al directorio

```bash
cd EpsAdmissions
```

## 3. Levantar la infraestructura

```bash
docker compose up -d
```

Esto iniciará los siguientes servicios:

* SQL Server
* MongoDB

## 4. Aplicar las migraciones

```bash
dotnet ef database update --project src/EpsAdmissions.Infrastructure --startup-project src/EpsAdmissions.Api
```

## 5. Ejecutar la API

```bash
dotnet run --project src/EpsAdmissions.Api
```

La API quedará disponible en:

```text
https://localhost:7136
```

## 6. Ejecutar el Dashboard

```bash
dotnet run --project src/EpsAdmissions.Blazor
```

El Dashboard estará disponible en:

```text
https://localhost:7172/dashboard-admissions
```

---

# Funcionamiento

Cada vez que se registra una nueva admisión:

1. Se almacena la información transaccional en SQL Server.
2. Se almacena la historia clínica en MongoDB.
3. Se registra un evento en la tabla **Outbox**.
4. Un `BackgroundService` procesa el evento pendiente.
5. El evento es publicado mediante **SignalR**.
6. El Dashboard recibe automáticamente la notificación sin necesidad de actualizar la página.

---

# Decisiones Técnicas

## Clean Architecture

La solución fue estructurada utilizando Clean Architecture para mantener la lógica de negocio independiente de la infraestructura, facilitando la mantenibilidad y escalabilidad del sistema.

## Persistencia Políglota

Se eligió SQL Server para la información transaccional y MongoDB para el almacenamiento de historias clínicas, aprovechando las fortalezas de cada motor de base de datos.

## Patrón Outbox

El patrón Outbox garantiza la consistencia entre la base de datos y la publicación de eventos, evitando pérdidas de mensajes cuando ocurren fallos durante el procesamiento.

## SignalR

SignalR permite actualizar el Dashboard en tiempo real, eliminando la necesidad de realizar consultas periódicas al servidor.

---

# Posibles Mejoras

Como evolución de esta Prueba de Concepto podrían incorporarse las siguientes funcionalidades:

* Autenticación y autorización.
* Integración con Azure Service Bus o RabbitMQ.
* Políticas de reintento mediante Polly.
* Health Checks.
* Observabilidad con OpenTelemetry.
* Pruebas unitarias.
* Pruebas de integración.
* Despliegue mediante Kubernetes.

---

# Autor

**Jonathan Gómez**
