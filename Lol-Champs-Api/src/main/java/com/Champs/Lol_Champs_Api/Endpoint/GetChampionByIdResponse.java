package com.Champs.Lol_Champs_Api.Endpoint;

import com.Champs.Lol_Champs_Api.Entities.Champion;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlElement;

@XmlRootElement(name = "GetChampionByIdResponse", namespace = "http://champs.com/lol_champs_api")
public class GetChampionByIdResponse {

    private Champion champion;

    @XmlElement
    public Champion getChampion() {
        return champion;
    }

    public void setChampion(Champion champion) {
        this.champion = champion;
    }
}
