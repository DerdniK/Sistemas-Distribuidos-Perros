package main

import (
	"fmt"
	"log"
	"net"

	"LolEvokersGRPC/database"
	"LolEvokersGRPC/services"
	pb "LolEvokersGRPC/proto"

	"google.golang.org/grpc"
)

func main() {
	// 1. Inicializar Base de Datos (Obtenemos la instancia)
	db := database.InitDB("/data/evokers.db")

	// 2. Configurar el Listener
	lis, err := net.Listen("tcp", "0.0.0.0:50051")
	if err != nil {
		log.Fatalf("Fallo al escuchar: %v", err)
	}

	// 3. Crear servidor gRPC
	grpcServer := grpc.NewServer()

	// 4. Registrar el servicio
	// AQUÍ ocurre la magia: Pasamos la BD al servicio
	evokerService := services.NewEvokerServer(db) 
	pb.RegisterEvokerServiceServer(grpcServer, evokerService)

	fmt.Println("🔮 Servidor de Invocadores gRPC escuchando en puerto :50051")
	if err := grpcServer.Serve(lis); err != nil {
		log.Fatalf("Fallo al servir: %v", err)
	}
}