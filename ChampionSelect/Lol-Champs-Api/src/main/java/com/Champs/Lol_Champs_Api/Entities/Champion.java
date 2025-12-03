package com.Champs.Lol_Champs_Api.Entities;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.xml.bind.annotation.XmlAccessType;

@XmlRootElement(name = "Champion", namespace = "http://champs.com/lol_champs_api")
@XmlAccessorType(XmlAccessType.FIELD)
@Entity
public class Champion {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @XmlElement(name = "name", namespace = "http://champs.com/lol_champs_api")
    private String name;

    @XmlElement(name = "role", namespace = "http://champs.com/lol_champs_api")
    private String role;

    @XmlElement(name = "difficulty", namespace = "http://champs.com/lol_champs_api")
    private String difficulty;

    
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getRole() { return role; }
    public void setRole(String role) { this.role = role; }

    public String getDifficulty() { return difficulty; }
    public void setDifficulty(String difficulty) { this.difficulty = difficulty; }
}
