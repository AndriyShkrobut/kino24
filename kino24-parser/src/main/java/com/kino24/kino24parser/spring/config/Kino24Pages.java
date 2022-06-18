package com.kino24.kino24parser.spring.config;

import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.boot.context.properties.ConstructorBinding;

@ConstructorBinding
@ConfigurationProperties(prefix = "kino24.pages")
public record Kino24Pages(String movies, String series) {
}
