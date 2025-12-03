// ChampionSelect/ChampionSelect/Repository/ChampionRepository.java
package ChampionSelect.ChampionSelect.Repository;

import ChampionSelect.ChampionSelect.Models.Champion;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface ChampionRepository extends JpaRepository<Champion, Long> {
    Optional<Champion> findByName(String name);
    Page<Champion> findAll(Pageable pageable);
    boolean existsByName(String name);
}
