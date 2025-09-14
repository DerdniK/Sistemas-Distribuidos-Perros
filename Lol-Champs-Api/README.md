# Contenedores

Crear la network donde se alojaran los contenedores:
`podman network create lolchamps-net`

## MySQL Database

Para correr el contenedor de la base de datos:
`podman run -d --name lolchamps-mysql --network lolchamps-net -e MYSQL_ROOT_PASSWORD=password123 -e MYSQL_DATABASE=League_of_Legends_DB -p 3306:3306 -v LOLChamps:/var/lib/mysql mysql:8.1.0`

## League Of Legends API 
Para hacer build de la imagen de la Api es necesario el siguiente comando:
`podman build -t lolchamps:1 .`

Y para correr un contenedor en donde podamos correr la images es de la siguiente manera:
`podman run -d --name lolchamps-api -p 8080:8080 --network lolchamps-net -e SPRING_DATASOURCE_URL="jdbc:mysql://lolchamps-mysql:3306/League_of_Legends_DB?useSSL=false&allowPublicKeyRetrieval=true&serverTimezone=UTC" lolchamps:1`


## Test en Insomnia y Postman
Esta es la url para hacer los post y ver el wsdl
`http://localhost:8080/ws/champions.wsdl`
