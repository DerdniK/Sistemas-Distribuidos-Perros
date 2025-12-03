package ChampionSelect.ChampionSelect.Config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.Customizer;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.servlet.util.matcher.MvcRequestMatcher;
import org.springframework.web.servlet.handler.HandlerMappingIntrospector;

@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http, HandlerMappingIntrospector introspector) throws Exception {
        MvcRequestMatcher.Builder mvcMatcherBuilder = new MvcRequestMatcher.Builder(introspector);

        http
            .csrf(csrf -> csrf.disable())
            .authorizeHttpRequests(auth -> auth
                // definir las rutas
                .requestMatchers(mvcMatcherBuilder.pattern(HttpMethod.GET, "/**")).hasAuthority("SCOPE_read")
                .requestMatchers(mvcMatcherBuilder.pattern(HttpMethod.POST, "/**")).hasAuthority("SCOPE_write")
                .requestMatchers(mvcMatcherBuilder.pattern(HttpMethod.PUT, "/**")).hasAuthority("SCOPE_write")
                .requestMatchers(mvcMatcherBuilder.pattern(HttpMethod.PATCH, "/**")).hasAuthority("SCOPE_write")
                .requestMatchers(mvcMatcherBuilder.pattern(HttpMethod.DELETE, "/**")).hasAuthority("SCOPE_write")
                
                .anyRequest().authenticated()
            )
            .oauth2ResourceServer(oauth2 -> oauth2.jwt(Customizer.withDefaults()));

        return http.build();
    }
}