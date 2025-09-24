package com.Champs.Lol_Champs_Api.Endpoint;

import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;

import com.Champs.Lol_Champs_Api.Entities.Champion;
import com.Champs.Lol_Champs_Api.Service.ChampionService;

@Endpoint
public class ChampionEndpoint {

    private static final String NAMESPACE_URI = "http://champs.com/lol_champs_api";
    private final ChampionService championService;

    public ChampionEndpoint(ChampionService championService) {
        this.championService = championService;
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "CreateChampionRequest")
    @ResponsePayload
    public CreateChampionResponse createChampion(@RequestPayload CreateChampionRequest request) {
        Champion champion = new Champion();
        champion.setName(request.getChampion().getName());
        champion.setRole(request.getChampion().getRole());
        champion.setDifficulty(request.getChampion().getDifficulty());

        Champion savedChampion = championService.createChampion(champion);

        CreateChampionResponse response = new CreateChampionResponse();
        response.setChampion(savedChampion);
        response.setMessage("Champion created successfully!");
        return response;
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "GetChampionByIdRequest")
    @ResponsePayload
    public GetChampionByIdResponse getChampionById(@RequestPayload GetChampionByIdRequest request) {
        Long id = request.getId();
        System.out.println("GetChampionById request id=" + id);

        if (id == null || id <= 0) {
            throw new IllegalArgumentException("ID inválido");
        }

        GetChampionByIdResponse response = new GetChampionByIdResponse();

        java.util.Optional<Champion> maybe = championService.findById(id);
        if (maybe.isPresent()) {
            response.setChampion(maybe.get());
        } else {
            System.out.println("Champion con id=" + id + " no encontrado.");
            response.setChampion(null);
        }

        return response;
    }

}
