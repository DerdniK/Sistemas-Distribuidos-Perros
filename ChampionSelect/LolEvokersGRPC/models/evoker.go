package models

// EvokerDB es la estructura que GORM usará para crear la tabla
type EvokerDB struct {
	ID    int32  `gorm:"primaryKey"`
	Name  string `gorm:"unique"` // Restricción única
	Level int32
}