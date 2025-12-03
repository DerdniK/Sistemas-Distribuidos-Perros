package com.Champs.Lol_Champs_Api.Endpoint;

import com.Champs.Lol_Champs_Api.Entities.Champion;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlElement;

@XmlRootElement(name = "CreateChampionResponse", namespace = "http://champs.com/lol_champs_api")
public class CreateChampionResponse {

    private Champion champion;
    private String message;

    // Champion
    @XmlElement
    public Champion getChampion() {
        return champion;
    }

    public void setChampion(Champion champion) {
        this.champion = champion;
    }

    
    @XmlElement
    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
