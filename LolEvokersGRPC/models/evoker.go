package models

type EvokerDB struct {
	ID    int32  `gorm:"primaryKey"` // PK
	Name  string `gorm:"unique"` // unico
	Level int32
}