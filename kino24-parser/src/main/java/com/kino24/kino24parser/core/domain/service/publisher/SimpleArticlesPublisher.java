package com.kino24.kino24parser.core.domain.service.publisher;

import com.kino24.kino24parser.core.domain.model.Kino24Article;
import com.kino24.kino24parser.core.domain.model.Event;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.connection.stream.StreamRecords;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Service;

import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class SimpleArticlesPublisher implements ArticlesPublisher {

    @Value("${redis.stream.news-events}")
    private final String stream;
    private final RedisTemplate<String, String> redisTemplate;
    private final ObjectMapper objectMapper;

    @Override
    @SneakyThrows
    public void publish(List<Kino24Article> articles) {
        var event = Event.builder()
                .eventId(UUID.randomUUID())
                .eventType("ArticlesAdded")
                .payload(objectMapper.writeValueAsString(articles))
                .timestamp(ZonedDateTime.now().withZoneSameInstant(ZoneId.of("UTC")))
                .build();

        var message = StreamRecords
                .objectBacked(event)
                .withStreamKey(stream);

        redisTemplate.opsForStream().add(message);
    }
}
