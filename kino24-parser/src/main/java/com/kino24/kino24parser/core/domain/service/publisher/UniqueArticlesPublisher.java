package com.kino24.kino24parser.core.domain.service.publisher;

import com.kino24.kino24parser.core.domain.model.Kino24Article;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.core.ListOperations;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Objects;

@Service
@RequiredArgsConstructor
public class UniqueArticlesPublisher implements ArticlesPublisher {

    private static final int CACHE_BUFFER_SIZE = 150;

    @Value("${redis.key.latest-news}")
    private final String recentArticlesKey;
    private final ArticlesPublisher publisherWrapper;
    private final RedisTemplate<String, String> redisTemplate;

    @Override
    @Transactional
    public void publish(List<Kino24Article> articles) {
        var listOperations = redisTemplate.opsForList();
        List<String> publishedTitles = getRecentlyPublishedTitles(listOperations);

        List<Kino24Article> newArticles = articles.stream()
                .filter(article -> !publishedTitles.contains(article.title()))
                .toList();

        if (newArticles.size() > 0) {
            updateCache(listOperations, newArticles);
            publisherWrapper.publish(newArticles);
        }
    }

    private List<String> getRecentlyPublishedTitles(ListOperations<String, String> listOperations) {
        var publishedTitles = listOperations.range(recentArticlesKey, 0, -1);
        return Objects.requireNonNull(publishedTitles);
    }

    private void updateCache(ListOperations<String, String> listOperations, List<Kino24Article> newArticles) {
        var newTitles = newArticles.stream().map(Kino24Article::title).toList();
        listOperations.leftPushAll(recentArticlesKey, newTitles);
        listOperations.trim(recentArticlesKey, 0, CACHE_BUFFER_SIZE - 1);
    }
}
