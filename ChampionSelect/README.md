
# Sistema Distribuido de Gestión de Invocadores (REST + gRPC + SOAP)

Este proyecto implementa una arquitectura de microservicios híbrida para la materia de Sistemas Distribuidos. El objetivo principal es la integración de un servicio **gRPC en Go** con una API **REST en Spring Boot**, manteniendo la persistencia de datos segregada.

## Arquitectura del Sistema

El sistema se compone de los siguientes microservicios contenedorizados:

1.  **API REST (Spring Boot):** Actúa como *API Gateway* y orquestador. Recibe peticiones HTTP del cliente y se comunica vía gRPC con el servicio de backend.
2.  **Servicio gRPC (Go):** Gestiona la lógica de negocio de los "Invocadores" (Evokers).
3.  **Servicio SOAP (Spring Boot):** Mantiene compatibilidad con el sistema anterior (Champions).
4.  **Bases de Datos:**
    * **MySQL:** Para el servicio SOAP.
    * **SQLite:** Exclusiva para el microservicio gRPC (Desacoplamiento de datos).


-----

## Tecnologías y Requisitos Cumplidos

| Requisito | Implementación |
| :--- | :--- |
| **Lenguaje gRPC** | **Go (Golang)** (Distinto al de la REST) |
| **Base de Datos** | **SQLite** (Distinta a MySQL usada en SOAP) |
| **Streaming** | Implementación de **Unary**, **Server Streaming** y **Client Streaming** |
| **Contenerización** | Docker / Podman con `podman-compose` |

-----

## Instrucciones de Ejecución

Este proyecto utiliza **Docker/Podman** para levantar todo el entorno sin necesidad de instalar Go o Java localmente.

### Prerrequisitos

  * Docker Desktop o Podman instalado.
  * `docker-compose` o `podman-compose`.

### Pasos para levantar

1.  Clonar el repositorio.
2.  Ejecutar el comando de composición en la raíz del proyecto:

<!-- end list -->

```bash
# Podman
podman-compose up --build -d

# Docker
docker-compose up --build -d
```

3.  Esperar a que los contenedores (MySQL, Redis, REST, SOAP, gRPC) estén en estado `running`.

-----

## API Endpoints y Pruebas (Cómo probar)

La API REST expone los siguientes endpoints que conectan internamente con gRPC. Gracias al manejo de excepciones implementado, los errores de gRPC se traducen automáticamente a códigos HTTP estándar.

### 1. Consultar Invocador por ID (Unary)
* **Endpoint:** `GET /api/evoker/{id}`
* **Método gRPC:** `GetEvokerById`
* **Respuestas:**
    * `200 OK`: Retorna el nombre y nivel del invocador.
    * `404 Not Found`: Si el ID no existe en SQLite.
    * `503 Service Unavailable`: Si el servidor gRPC está caído.

**Prueba con Curl:**
```bash
curl -v -X GET http://localhost:8081/api/evoker/1
````

### 2\. Buscar Invocadores por Nombre (Server Streaming)

  * **Endpoint:** `GET /api/evoker/search?name={nombre}`
  * **Método gRPC:** `GetEvokersByName`
  * **Descripción:** El cliente envía un nombre y recibe una lista JSON construida a partir del flujo (stream) de datos que envía Go.
  * **Nota:** Si omites el parámetro `name`, retorna todos los registros.

**Prueba con Curl:**

```bash
# Buscar específicos
curl -v -X GET "http://localhost:8081/api/evoker/search?name=Gandalf"

# Buscar todos
curl -v -X GET "http://localhost:8081/api/evoker/search"
```

### 3\. Creación Masiva de Invocadores (Client Streaming / Batch)

  * **Endpoint:** `POST /api/evoker/batch`
  * **Método gRPC:** `CreateEvokers`
  * **Descripción:** Recibe un JSON Array. Spring abre un stream asíncrono hacia Go y envía los registros uno a uno. Al finalizar, retorna un resumen.
  * **Mapeo de Errores (HTTP Status):**
      * `400 Bad Request`: Si el nombre está vacío o el nivel es negativo.
      * `409 Conflict`: Si se intenta crear un nombre que ya existe (Violación de UNIQUE).
      * `500 Internal Server Error`: Fallos inesperados en el streaming.

**Prueba con Curl (Caso Exitoso):**

```bash
curl -v -X POST http://localhost:8081/api/evoker/batch \
-H "Content-Type: application/json" \
-d '[
    {"name": "Merlin", "level": 50},
    {"name": "Gandalf", "level": 99},
    {"name": "Aprendiz", "level": 1}
]'
```

-----

### 4\. Pruebas Directas al Servidor gRPC (Sin pasar por REST)

Para cumplir con el requisito de *"cliente gRPC para streaming"*, se utilizan conexiones directas al microservicio de Go utilizando **Postman (Modo gRPC)**.

  * **Dirección del Servidor:** `127.0.0.1:50051`
  * **Seguridad:** Plaintext (Sin SSL/TLS)

#### 1\. Creación Masiva (Client Streaming)

  * **Método gRPC:** `CreateEvokers`
  * **Tipo:** Client Streaming.
  * **Descripción:** Se mantiene el canal abierto para enviar múltiples mensajes JSON. Al finalizar el envío (`End Streaming`), el servidor procesa el lote y retorna el conteo total.

**Instrucciones de Prueba (Postman):**

```json
// PASO 1: Iniciar "Invoke" y enviar Mensaje 1
{
  "name": "TestDirecto_1",
  "level": 10
}
// (Clic en "Send")

// PASO 2: Enviar Mensaje 2
{
  "name": "TestDirecto_2",
  "level": 20
}
// (Clic en "Send")

// PASO 3: Clic en "End Streaming"
// Respuesta esperada: "Se han creado 2 invocadores exitosamente..."
```

#### 2\. Buscar por Nombre (Server Streaming)

  * **Método gRPC:** `GetEvokersByName`
  * **Tipo:** Server Streaming.
  * **Descripción:** Se envía una solicitud con un filtro y el servidor responde con múltiples mensajes en el tiempo (uno por cada registro encontrado).

**Instrucciones de Prueba (Postman):**

```json
// PASO 1: Enviar solicitud con filtro vacío (Traer todos)
{
  "name": ""
}

// PASO 2: Observar el log de respuestas
// Respuesta 1: { "name": "Merlin", ... }
// Respuesta 2: { "name": "Gandalf", ... }
```

#### 3\. Consultar por ID (Unary)

  * **Método gRPC:** `GetEvokerById`
  * **Tipo:** Unary.
  * **Descripción:** Petición simple de solicitud-respuesta.

**Instrucciones de Prueba (Postman):**

```json
// Enviar ID existente
{
  "id": 1
}

// Respuesta esperada: Objeto EvokerResponse con los datos.
```

-----

## Definición del Contrato (.proto)

El archivo `servicio.proto` define la estructura de comunicación:

```protobuf
service EvokerService {
  rpc GetEvokerById (EvokerByIdRequest) returns (EvokerResponse);          // Unary
  rpc GetEvokersByName (EvokersByNameRequest) returns (stream EvokerResponse); // Server Streaming
  rpc CreateEvokers (stream CreateEvokerRequest) returns (CreateEvokersResponse); // Client Streaming
}
```

-----

## Base de Datos y Scripts

  * **Motor:** SQLite.
  * **Ubicación:** El archivo `evokers.db` se genera automáticamente en la carpeta `./data_grpc` del volumen montado.
  * **Creación de Tablas:** Se utiliza **GORM AutoMigrate** en Go. Al iniciar el contenedor, el sistema detecta si la tabla `evoker_dbs` existe; si no, la crea automáticamente con la siguiente estructura (Script SQL equivalente):

<!-- end list -->

```sql
CREATE TABLE evoker_dbs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT UNIQUE,
    level INTEGER
);
```

-----

## Evidencia de Invocación (Fragmento de Código)

A continuación se muestra cómo el cliente Java (`GrpcClientService.java`) invoca al servidor Go:

```java
// Ejemplo de llamada Unary
public String getEvoker(int id) {
    // 1. Construir el Request (Protobuf)
    EvokerByIdRequest request = EvokerByIdRequest.newBuilder().setId(id).build();
    
    // 2. Invocar al Stub (Cliente gRPC)
    EvokerResponse response = blockingStub.getEvokerById(request);
    
    // 3. Retornar respuesta
    return response.getName();
}
```
