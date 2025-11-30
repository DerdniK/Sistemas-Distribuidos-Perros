package database

import (
	"log"
	
	"LolEvokersGRPC/models" // Importamos tu modelo
	"gorm.io/driver/sqlite"
	"gorm.io/gorm"
)

// InitDB conecta y migra la base de datos, y devuelve la conexión
func InitDB(path string) *gorm.DB {
	db, err := gorm.Open(sqlite.Open(path), &gorm.Config{})
	if err != nil {
		log.Fatal("Error conectando a la BD:", err)
	}

	// Migración automática
	err = db.AutoMigrate(&models.EvokerDB{})
	if err != nil {
		log.Fatal("Error migrando la BD:", err)
	}

	return db
}

