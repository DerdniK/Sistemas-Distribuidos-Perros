# Champion Select API REST

API REST para gestionar campeones de League of Legends, con caché Redis y conexión a servicio SOAP.

## Requisitos

- Java 17
- Docker y Docker Compose
- Maven

## Configuración

1. Clonar el repositorio:
```bash
git clone <url-repositorio>
cd ChampionSelect
```

2. Construir el proyecto:
```bash
mvn clean package
```

3. Levantar los servicios:
```bash
docker-compose up -d --build
```

## Endpoints

### Obtener un campeón por ID
```http
GET /api/v1/champions/{id}
```

### Listar campeones (paginado)
```http
GET /api/v1/champions?page=0&pageSize=10&sort=name
```

### Crear un campeón
```http
POST /api/v1/champions
Content-Type: application/json

{
    "name": "Ahri",
    "role": "Mage",
    "difficulty": "Moderate"
}
```

### Actualizar un campeón
```http
PUT /api/v1/champions/{id}
Content-Type: application/json

{
    "name": "Ahri",
    "role": "Mage",
    "difficulty": "Hard"
}
```

### Actualización parcial
```http
PATCH /api/v1/champions/{id}
Content-Type: application/json

{
    "difficulty": "Easy"
}
```

### Eliminar un campeón
```http
DELETE /api/v1/champions/{id}
```

## Códigos de Respuesta

- 200: OK - Petición exitosa
- 201: Created - Recurso creado exitosamente
- 204: No Content - Eliminación exitosa
- 400: Bad Request - Error en la validación
- 404: Not Found - Recurso no encontrado
- 409: Conflict - Conflicto (ej: nombre duplicado)
- 502: Bad Gateway - Error en servicio SOAP

## Caché Redis

Los endpoints GET utilizan caché Redis con un tiempo de expiración de 10 minutos.

## Ejemplos con curl

### Crear un campeón
```bash
curl -X POST http://localhost:8081/api/v1/champions \
     -H "Content-Type: application/json" \
     -d '{"name":"Ahri","role":"Mage","difficulty":"Moderate"}'
```

### Obtener un campeón
```bash
curl http://localhost:8081/api/v1/champions/1
```

### Listar campeones
```bash
curl "http://localhost:8081/api/v1/champions?page=0&pageSize=10&sort=name"
```

### Actualizar un campeón
```bash
curl -X PUT http://localhost:8081/api/v1/champions/1 \
     -H "Content-Type: application/json" \
     -d '{"name":"Ahri","role":"Mage","difficulty":"Hard"}'
```

### Eliminar un campeón
```bash
curl -X DELETE http://localhost:8081/api/v1/champions/1
```

## Integración con SOAP

La API REST se comunica con el servicio SOAP para mantener sincronizados los datos. Cada operación de escritura (POST, PUT, PATCH, DELETE) se replica en el servicio SOAP.
