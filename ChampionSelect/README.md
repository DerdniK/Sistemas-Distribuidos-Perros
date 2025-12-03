
# Sistema Distribuido de Gestión de Invocadores
> **Arquitectura Híbrida: REST + gRPC + SOAP + OAuth2**

![Spring Boot](https://img.shields.io/badge/Spring_Boot-3.0+-green?style=flat&logo=springboot)
![Go](https://img.shields.io/badge/Go-1.20+-blue?style=flat&logo=go)
![Ory Hydra](https://img.shields.io/badge/Ory_Hydra-OAuth2-orange?style=flat&logo=ory)
![Docker](https://img.shields.io/badge/Docker-Compose-blue?style=flat&logo=docker)

Este proyecto implementa una arquitectura de microservicios para la materia de Sistemas Distribuidos. El objetivo principal es la integración de un servicio **gRPC en Go** con una API **REST en Spring Boot**, manteniendo la persistencia de datos segregada y asegurando la comunicación mediante **OAuth2**.

---

## Arquitectura del Sistema

El sistema se compone de los siguientes microservicios contenedorizados:

1.  **API REST (Spring Boot):** Actúa como *API Gateway* y orquestador. Recibe peticiones HTTP del cliente y se comunica vía gRPC con el backend.
2.  **Servicio gRPC (Go):** Gestiona la lógica de negocio de los "Invocadores" (Evokers).
3.  **Servicio SOAP (Spring Boot):** Mantiene compatibilidad con el sistema legado (Champions).
4.  **Bases de Datos:**
    **MySQL:** Para el servicio SOAP.
    **SQLite:** Exclusiva para el microservicio gRPC (Desacoplamiento de datos).
5.  **Servicio de Autenticación (Ory Hydra):** Servidor OAuth2 certificado que gestiona la emisión y validación de tokens de acceso (JWT) para proteger la API REST.

---

## Tecnologías y Requisitos

| Requisito | Implementación |
| :--- | :--- |
| **Lenguaje gRPC** | **Go (Golang)** (Distinto al de la REST) |
| **Base de Datos** | **SQLite** (Distinta a MySQL usada en SOAP) |
| **Streaming** | Implementación de **Unary**, **Server Streaming** y **Client Streaming** |
| **Contenerización** | Docker / Podman con `compose` |
| **Seguridad** | **Ory Hydra** (Client Credentials Flow) |

---

## Instrucciones de Ejecución

Este proyecto utiliza **Docker/Podman** para levantar todo el entorno sin necesidad de instalar Go o Java localmente.

### Prerrequisitos
* Docker Desktop o Podman instalado.
* `docker-compose` o `podman-compose`.

### Pasos para levantar

1.  **Clonar el repositorio.**

2.  **Levantar los contenedores:**
    Ejecuta el comando en la raíz del proyecto:
    ```bash
    # Podman
    podman-compose up --build -d

    # Docker
    docker-compose up --build -d
    ```

3.  **Esperar a que inicien:**
    Asegúrate de que los contenedores (`mysql`, `redis`, `rest-service`, `soap-service`, `grpc-server`, `hydra`) estén en estado `running` o `healthy`.

4.  **Configuración de Seguridad (OBLIGATORIO):**
    Una vez que el sistema esté corriendo, debes registrar un cliente en Hydra para poder generar tokens. Sin esto, la API rechazará todas las peticiones (`401 Unauthorized`).

    Ejecuta el siguiente comando en tu terminal:
    ```bash
    podman compose exec hydra hydra clients create --endpoint [http://127.0.0.1:4445](http://127.0.0.1:4445) --id api-client --secret secret --grant-types client_credentials --response-types token --scope "read write"
    ```
    > **Nota:** Si el comando es exitoso, verás un JSON con los detalles del cliente creado.

---

## Seguridad y Autenticación

El sistema utiliza **OAuth2 (Client Credentials Flow)**. Todos los endpoints de la API REST requieren un **JWT** válido en el encabezado `Authorization`.

### 1. Roles y Scopes
* `read`: Requerido para operaciones GET (Consultas).
* `write`: Requerido para operaciones POST (Modificaciones).

### 2. Obtención del Token
Antes de probar la API, solicita tu token a Hydra:

```bash
curl -X POST "http://localhost:4444/oauth2/token" -u "api-client:secret" -d "grant_type=client_credentials" -d "scope=read write"

````

> **Respuesta:** Recibirás un JSON con el `access_token`. Copia este token; lo usarás en los siguientes ejemplos reemplazando `$TOKEN`.

-----

## API Endpoints (REST)

La API REST actúa como fachada para el servicio gRPC. Los errores de gRPC se traducen automáticamente a códigos HTTP estándar (`404`, `503`, `409`, etc.).

### 1\. Consultar Invocador por ID (Unary)

  * **Endpoint:** `GET /api/evoker/{id}`
  * **Descripción:** Obtiene un invocador por su ID.

<!-- end list -->

```bash
curl -v -X GET http://localhost:8081/api/evoker/1 -H "Authorization: Bearer $TOKEN"
```

### 2\. Buscar Invocadores por Nombre (Server Streaming)

  * **Endpoint:** `GET /api/evoker/search?name={nombre}`
  * **Descripción:** Retorna una lista construida a partir del stream de datos de Go. Si omites `name`, retorna todos.

<!-- end list -->

```bash
# Buscar específicos
curl -v -X GET "http://localhost:8081/api/evoker/search?name=Gandalf" -H "Authorization: Bearer $TOKEN"

# Buscar todos
curl -v -X GET "http://localhost:8081/api/evoker/search" -H "Authorization: Bearer $TOKEN"
```

### 3\. Creación Masiva (Client Streaming / Batch)

  * **Endpoint:** `POST /api/evoker/batch`
  * **Descripción:** Envía un JSON Array mediante un stream asíncrono hacia Go.

<!-- end list -->

```bash
curl -v -X POST http://localhost:8081/api/evoker/batch -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '[{"name": "Merlin", "level": 50}, {"name": "Gandalf", "level": 99}, {"name": "Aprendiz", "level": 1}]'
```

-----

## Pruebas Directas a gRPC (Sin REST)

Para verificar la comunicación gRPC pura, puedes conectar **Postman** directamente al contenedor de Go.

  * **Dirección:** `127.0.0.1:50051`
  * **Seguridad:** Plaintext (Sin SSL)

### 1\. Creación Masiva (Client Streaming)

  * **Método:** `CreateEvokers`
  * **Flujo:** Envía múltiples mensajes JSON y finaliza el stream.

<!-- end list -->

```json
// Mensaje 1
{ "name": "TestDirecto_1", "level": 10 }
// Mensaje 2
{ "name": "TestDirecto_2", "level": 20 }
// -> Click "End Streaming"
```

### 2\. Buscar por Nombre (Server Streaming)

  * **Método:** `GetEvokersByName`
  * **Flujo:** Envía una solicitud y recibe múltiples respuestas.

<!-- end list -->

```json
{ "name": "" } // Filtro vacío para traer todos
```

-----

## Detalles Técnicos

### Definición del Contrato (.proto)

```protobuf
service EvokerService {
  rpc GetEvokerById (EvokerByIdRequest) returns (EvokerResponse);          // Unary
  rpc GetEvokersByName (EvokersByNameRequest) returns (stream EvokerResponse); // Server Streaming
  rpc CreateEvokers (stream CreateEvokerRequest) returns (CreateEvokersResponse); // Client Streaming
}
```

### Base de Datos (SQLite)

El archivo `evokers.db` se genera automáticamente en `./data_grpc` mediante **GORM AutoMigrate**.

```sql
CREATE TABLE evoker_dbs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT UNIQUE,
    level INTEGER
);
```
