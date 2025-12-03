package database

import (
	"log"
	
	"LolEvokersGRPC/models"
	"gorm.io/driver/sqlite"
	"gorm.io/gorm"
)

// InitDB conecta y migra la base de datos y devuelve la conexion
func InitDB(path string) *gorm.DB {
	db, err := gorm.Open(sqlite.Open(path), &gorm.Config{})
	if err != nil {
		log.Fatal("Error conectando a la BD:", err)
	}

	// Migracion automatica
	err = db.AutoMigrate(&models.EvokerDB{})
	if err != nil {
		log.Fatal("Error migrando la BD:", err)
	}

	return db
}

