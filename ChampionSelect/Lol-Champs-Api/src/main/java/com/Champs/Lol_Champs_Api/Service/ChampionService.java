package com.Champs.Lol_Champs_Api.Service;

import com.Champs.Lol_Champs_Api.Entities.Champion;
import com.Champs.Lol_Champs_Api.Repository.ChampionRepository;
import org.springframework.stereotype.Service;
import jakarta.transaction.Transactional;
import java.util.Optional;

@Service
public class ChampionService {

    private final ChampionRepository championRepository;

    public ChampionService(ChampionRepository championRepository) {
        this.championRepository = championRepository;
    }

    @Transactional
    public Champion createChampion(Champion champion) {
        if (champion.getName() == null || champion.getName().isEmpty()) {
            throw new IllegalArgumentException("El nombre del campeón es obligatorio");
        }
        return championRepository.save(champion);
    }

    public Optional<Champion> findById(Long id) {
        if (id == null || id <= 0) {
            throw new IllegalArgumentException("ID inválido");
        }
        return championRepository.findById(id);
    }
}
