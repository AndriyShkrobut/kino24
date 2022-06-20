package com.kino24.kino24newsread.core.domain.messaging;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.service.ArticleService;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.connection.stream.MapRecord;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.stream.StreamListener;
import org.springframework.stereotype.Component;

import java.util.List;
import java.util.Map;

@Component
@RequiredArgsConstructor
public class NewArticlesConsumer implements StreamListener<String, MapRecord<String, String, String>> {
    private static final Logger LOG = LoggerFactory.getLogger(NewArticlesConsumer.class);

    private final ArticleService articleService;
    private final ObjectMapper objectMapper;
    private final RedisTemplate<String, String> redisTemplate;
    @Value("${redis.consumer-group.news-events}")
    private final String consumerGroup;

    @Override
    public void onMessage(MapRecord<String, String, String> message) {
        var event = message.getValue();
        if ("ArticlesAdded".equals(event.get("eventType"))) {
            processArticlesAdded(event);
        } else {
            LOG.info("Unknown event type {}. Skipping event from processing.", event.get("eventType"));
        }
        redisTemplate.opsForStream().acknowledge(consumerGroup, message);
    }

    @SneakyThrows
    private void processArticlesAdded(Map<String, String> event) {
        var typeRef = new TypeReference<List<Article>>() {
        };
        var articles = objectMapper.readValue(event.get("payload"), typeRef);
        articleService.save(articles);
        LOG.info("{} articles created.", articles.size());
    }
}
