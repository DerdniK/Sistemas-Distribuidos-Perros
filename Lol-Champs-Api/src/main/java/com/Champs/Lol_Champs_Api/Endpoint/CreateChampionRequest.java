package com.Champs.Lol_Champs_Api.Endpoint;

import com.Champs.Lol_Champs_Api.Entities.Champion;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "CreateChampionRequest", namespace = "http://champs.com/lol_champs_api")
public class CreateChampionRequest {

    private Champion champion;

    @XmlElement(name = "champion", namespace = "http://champs.com/lol_champs_api")
    public Champion getChampion() {
        return champion;
    }

    public void setChampion(Champion champion) {
        this.champion = champion;
    }
}
