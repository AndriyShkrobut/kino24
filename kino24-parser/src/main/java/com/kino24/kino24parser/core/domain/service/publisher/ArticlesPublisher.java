package com.kino24.kino24parser.core.domain.service.publisher;

import com.kino24.kino24parser.core.domain.model.Kino24Article;

import java.util.List;

public interface ArticlesPublisher {
    void publish(List<Kino24Article> articles);
}
