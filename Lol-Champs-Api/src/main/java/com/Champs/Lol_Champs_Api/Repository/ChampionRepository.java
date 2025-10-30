package com.Champs.Lol_Champs_Api.Repository;

import org.springframework.data.jpa.repository.JpaRepository;
import com.Champs.Lol_Champs_Api.Entities.Champion;

public interface ChampionRepository extends JpaRepository<Champion, Long> {
}
