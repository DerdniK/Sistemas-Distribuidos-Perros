package ChampionSelect.ChampionSelect;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cache.annotation.EnableCaching;

@SpringBootApplication
@EnableCaching
public class ChampionSelectApplication {
    public static void main(String[] args) {
        SpringApplication.run(ChampionSelectApplication.class, args);
    }
}
