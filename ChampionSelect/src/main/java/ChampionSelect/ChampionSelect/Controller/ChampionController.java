package ChampionSelect.ChampionSelect.Controller;

import ChampionSelect.ChampionSelect.Exception.ConflictException;
import ChampionSelect.ChampionSelect.Exception.SoapClientException;
import ChampionSelect.ChampionSelect.Models.Champion;
import ChampionSelect.ChampionSelect.Service.ChampionService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;

import java.net.URI;
import java.util.List;

@RestController
@RequestMapping("/api/v1/champions")
@CrossOrigin(origins = "*")
public class ChampionController {

    private final ChampionService championService;

    public ChampionController(ChampionService championService) {
        this.championService = championService;
    }

    @GetMapping("/{id}")
    public ResponseEntity<Champion> getChampionById(@PathVariable Long id) {
        return championService.getChampionById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping
    public ResponseEntity<List<Champion>> getAllChampions(
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "10") int pageSize,
            @RequestParam(defaultValue = "name") String sort) {
        List<Champion> champions = championService.getAllChampions(page, pageSize, sort);
        return ResponseEntity.ok(champions);
    }

    @PostMapping
    public ResponseEntity<Champion> createChampion(@Valid @RequestBody Champion champion) {
        try {
            Champion created = championService.createChampion(champion);
            URI location = ServletUriComponentsBuilder
                    .fromCurrentRequest()
                    .path("/{id}")
                    .buildAndExpand(created.getId())
                    .toUri();
            return ResponseEntity.created(location).body(created);
        } catch (ConflictException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).build();
        } catch (SoapClientException e) {
            return ResponseEntity.status(HttpStatus.BAD_GATEWAY).build();
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<Champion> updateChampion(
            @PathVariable Long id,
            @Valid @RequestBody Champion champion) {
        return championService.updateChampion(id, champion)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @PatchMapping("/{id}")
    public ResponseEntity<Champion> patchChampion(
            @PathVariable Long id,
            @RequestBody Champion champion) {
        return championService.patchChampion(id, champion)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteChampion(@PathVariable Long id) {
        return championService.deleteChampion(id) 
            ? ResponseEntity.noContent().build()
            : ResponseEntity.notFound().build();
    }
}
