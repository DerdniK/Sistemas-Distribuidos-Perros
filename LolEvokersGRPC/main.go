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
	// Inicializar BD 
	db := database.InitDB("/data/evokers.db")

	// Listener
	lis, err := net.Listen("tcp", "0.0.0.0:50051")
	if err != nil {
		log.Fatalf("Fallo al escuchar: %v", err)
	}

	// servidor gRPC
	grpcServer := grpc.NewServer()

	// servicio
	evokerService := services.NewEvokerServer(db) 
	pb.RegisterEvokerServiceServer(grpcServer, evokerService)

	fmt.Println("Servidor de Invocadores gRPC escuchando en puerto :50051")
	if err := grpcServer.Serve(lis); err != nil {
		log.Fatalf("Fallo al servir: %v", err)
	}
}