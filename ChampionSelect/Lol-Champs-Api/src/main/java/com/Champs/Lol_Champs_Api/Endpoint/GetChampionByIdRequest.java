package com.Champs.Lol_Champs_Api.Endpoint;

import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlAccessType;

@XmlRootElement(name = "GetChampionByIdRequest", namespace = "http://champs.com/lol_champs_api")
@XmlAccessorType(XmlAccessType.FIELD)
public class GetChampionByIdRequest {

    @XmlElement(name = "id", namespace = "http://champs.com/lol_champs_api", required = true)
    private Long id;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }
}
