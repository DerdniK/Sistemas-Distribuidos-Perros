package ChampionSelect.ChampionSelect.Models;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import java.io.Serializable;

@Entity
@Table(name = "champion")
public class Champion implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @NotBlank(message = "El nombre es obligatorio")
    @Column(unique = true)
    private String name;

    @NotBlank(message = "El rol es obligatorio")
    private String role;

    @NotBlank(message = "La dificultad es obligatoria")
    private String difficulty;

    // Constructores
    public Champion() {}

    public Champion(String name, String role, String difficulty) {
        this.name = name;
        this.role = role;
        this.difficulty = difficulty;
    }

    // Getters y Setters
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }

    public String getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(String difficulty) {
        this.difficulty = difficulty;
    }
}