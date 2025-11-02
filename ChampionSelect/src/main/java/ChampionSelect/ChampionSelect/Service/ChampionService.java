package ChampionSelect.ChampionSelect.Service;

import ChampionSelect.ChampionSelect.Client.ChampionSoapClient;
import ChampionSelect.ChampionSelect.Exception.ConflictException;
import ChampionSelect.ChampionSelect.Models.Champion;
import ChampionSelect.ChampionSelect.Repository.ChampionRepository;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.CachePut;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.cache.annotation.Caching;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class ChampionService {
    
    private final ChampionRepository championRepository;
    private final ChampionSoapClient soapClient;

    public ChampionService(ChampionRepository championRepository, ChampionSoapClient soapClient) {
        this.championRepository = championRepository;
        this.soapClient = soapClient;
    }

    @Cacheable(value = "champions", key = "#id")
    public Optional<Champion> getChampionById(long id) {
        return championRepository.findById(id);
    }
    
    @Cacheable(value = "championList", key = "{#page, #pageSize, #sort}")
    public List<Champion> getAllChampions(int page, int pageSize, String sort) {
        PageRequest pageRequest = PageRequest.of(page, pageSize, Sort.by(sort));
        return championRepository.findAll(pageRequest).getContent();
    }
    
    @Transactional
    @CacheEvict(value = "championList", allEntries = true) 
    public Champion createChampion(Champion newChampion) {
        if (championRepository.existsByName(newChampion.getName())) {
            throw new ConflictException("Ya existe un campeón con ese nombre");
        }
        
        Champion savedChampion = championRepository.save(newChampion);
        
        try {
            soapClient.createChampion(savedChampion);
        } catch (Exception e) {
        }
        
        return savedChampion;
    }

    @Transactional
    @Caching(evict = {
            @CacheEvict(value = "championList", allEntries = true)
    }, put = {
            @CachePut(value = "champions", key = "#id")
    })
    public Optional<Champion> updateChampion(long id, Champion championDetails) {
        return championRepository.findById(id).map(existingChampion -> {
            existingChampion.setName(championDetails.getName());
            existingChampion.setRole(championDetails.getRole());
            existingChampion.setDifficulty(championDetails.getDifficulty());
            
            Champion updatedChampion = championRepository.save(existingChampion);
            
            try {
                soapClient.updateChampion(id, updatedChampion);
            } catch (Exception e) {
            }
            
            return updatedChampion;
        });
    }

    @Transactional
    @Caching(evict = {
            @CacheEvict(value = "championList", allEntries = true)
    }, put = {
            @CachePut(value = "champions", key = "#id")
    })
    public Optional<Champion> patchChampion(long id, Champion championPatch) {
        return championRepository.findById(id).map(existingChampion -> {
            if (championPatch.getName() != null) {
                existingChampion.setName(championPatch.getName());
            }
            if (championPatch.getRole() != null) {
                existingChampion.setRole(championPatch.getRole());
            }
            if (championPatch.getDifficulty() != null) {
                existingChampion.setDifficulty(championPatch.getDifficulty());
            }
            
            Champion patchedChampion = championRepository.save(existingChampion);
            
            try {
                soapClient.updateChampion(id, patchedChampion);
            } catch (Exception e) {
            }
            
            return patchedChampion;
        });
    }

    @Transactional
    @Caching(evict = {
            @CacheEvict(value = "champions", key = "#id"),
            @CacheEvict(value = "championList", allEntries = true)
    })
    public boolean deleteChampion(long id) {
        return championRepository.findById(id).map(champion -> {
            championRepository.deleteById(id);
            
            try {
                soapClient.deleteChampion(id);
            } catch (Exception e) {
            }
            
            return true;
        }).orElse(false);
    }
}