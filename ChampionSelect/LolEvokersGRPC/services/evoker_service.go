package services

import (
	"context"
	"fmt"
	"io"

	"LolEvokersGRPC/models"
	pb "LolEvokersGRPC/proto" // Asegúrate que este import coincida con tu go.mod

	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"
	"gorm.io/gorm"
)

// Definimos la estructura del servidor
type EvokerServer struct {
	pb.UnimplementedEvokerServiceServer
	DB *gorm.DB // Inyección de dependencia de la base de datos
}

// Constructor
func NewEvokerServer(db *gorm.DB) *EvokerServer {
	return &EvokerServer{DB: db}
}

// --- MÉTODOS CORREGIDOS ---

// 1. UNARY: GetEvokerById
// CORRECCIÓN: El receptor ahora es (s *EvokerServer), no (s *server)
func (s *EvokerServer) GetEvokerById(ctx context.Context, req *pb.EvokerByIdRequest) (*pb.EvokerResponse, error) {
	var evoker models.EvokerDB

	// Usamos s.DB (la base de datos inyectada)
	result := s.DB.First(&evoker, req.Id)

	if result.Error != nil {
		return nil, status.Errorf(codes.NotFound, "El invocador con ID %d no existe", req.Id)
	}

	return &pb.EvokerResponse{
		Id:    evoker.ID,
		Name:  evoker.Name,
		Level: evoker.Level,
	}, nil
}

// 2. SERVER STREAMING: GetEvokersByName
func (s *EvokerServer) GetEvokersByName(req *pb.EvokersByNameRequest, stream pb.EvokerService_GetEvokersByNameServer) error {
	// CORRECCIÓN: Agregamos [] para decir que es una LISTA de invocadores, no uno solo.
	var evokers []models.EvokerDB 

	// Buscamos coincidencias
	s.DB.Where("name LIKE ?", "%"+req.Name+"%").Find(&evokers)

	// Ahora sí podemos recorrer la lista
	for _, e := range evokers {
		if err := stream.Send(&pb.EvokerResponse{
			Id:    e.ID,
			Name:  e.Name,
			Level: e.Level,
		}); err != nil {
			return err
		}
	}
	return nil
}

// 3. CLIENT STREAMING: CreateEvokers
func (s *EvokerServer) CreateEvokers(stream pb.EvokerService_CreateEvokersServer) error {
	count := 0
	for {
		req, err := stream.Recv()

		if err == io.EOF {
			return stream.SendAndClose(&pb.CreateEvokersResponse{
				EvokersCount: int32(count),
				Message:      fmt.Sprintf("Se han creado %d invocadores exitosamente.", count),
			})
		}
		if err != nil {
			return err
		}

		// --- VALIDACIONES ---
		if req.Name == "" {
			return status.Error(codes.InvalidArgument, "El nombre del invocador no puede estar vacío")
		}
		if req.Level < 0 {
			return status.Error(codes.InvalidArgument, "El nivel no puede ser negativo")
		}

		// Validación de duplicados
		var existe models.EvokerDB
		result := s.DB.Where("name = ?", req.Name).First(&existe)

		if result.RowsAffected > 0 {
			msg := fmt.Sprintf("El invocador '%s' ya existe", req.Name)
			return status.Error(codes.AlreadyExists, msg)
		}

		// Guardar
		nuevo := models.EvokerDB{Name: req.Name, Level: req.Level}
		if err := s.DB.Create(&nuevo).Error; err != nil {
			return status.Error(codes.Internal, "Error al guardar en BD")
		}

		count++
	}
}