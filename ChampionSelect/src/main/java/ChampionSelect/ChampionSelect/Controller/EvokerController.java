package ChampionSelect.ChampionSelect.Controller;

import ChampionSelect.ChampionSelect.Dtos.EvokerInput;
import ChampionSelect.ChampionSelect.Service.GrpcClientService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/evoker")
public class EvokerController {

    @Autowired
    private GrpcClientService grpcService;

    // GET /api/evoker/1
    @GetMapping("/{id}")
    public String getById(@PathVariable int id) {
        return grpcService.getEvoker(id);
    }

    // GET /api/evoker/search?name=D3RDN1K
    @GetMapping("/search")
    public List<String> searchByName(@RequestParam(defaultValue = "") String name) {
        return grpcService.getEvokersByName(name);
    }

    // POST /api/evoker/batch - batch es para recibir varios invocadores
    // Recibe un JSON array
    @PostMapping("/batch")
    public String createBatch(@RequestBody List<EvokerInput> evokers) {
        return grpcService.createEvokers(evokers);
    }
}