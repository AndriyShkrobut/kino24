package com.kino24.kino24parser.core.domain.service;

import com.kino24.kino24parser.core.domain.service.extractor.Kino24Subscriber;
import com.kino24.kino24parser.core.domain.model.Kino24Article;
import com.kino24.kino24parser.core.domain.service.publisher.ArticlesPublisher;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
@RequiredArgsConstructor
public class NewArticlesHandler implements Kino24Subscriber {
    @Qualifier("uniqueArticlesPublisher")
    private final ArticlesPublisher articlesPublisher;

    @Override
    public void consume(List<Kino24Article> articles) {
        articlesPublisher.publish(articles);
    }
}
