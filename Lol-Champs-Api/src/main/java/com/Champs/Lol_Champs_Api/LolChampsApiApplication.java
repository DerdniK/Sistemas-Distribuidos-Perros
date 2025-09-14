package com.Champs.Lol_Champs_Api;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.ws.config.annotation.EnableWs;

@EnableWs
@SpringBootApplication
@EnableJpaRepositories("com.Champs.Lol_Champs_Api.Repository")
@EntityScan("com.Champs.Lol_Champs_Api.Entities")
public class LolChampsApiApplication {
    public static void main(String[] args) {
        SpringApplication.run(LolChampsApiApplication.class, args);
    }
}
