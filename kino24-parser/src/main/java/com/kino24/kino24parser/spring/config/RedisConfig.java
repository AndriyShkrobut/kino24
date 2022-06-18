package com.kino24.kino24parser.spring.config;

import lombok.RequiredArgsConstructor;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;

@Component
@RequiredArgsConstructor
public class RedisConfig {
    private final RedisTemplate<String, String> redisTemplate;

    @PostConstruct
    private void setup() {
        redisTemplate.setEnableTransactionSupport(true);
    }

}
