package com.ejemplo.helloworldapi;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloController {
    @GetMapping("/hello")
    public String hello() {
        return "Hola mundo! Soy Gustavo desde Spring";
    }
}
