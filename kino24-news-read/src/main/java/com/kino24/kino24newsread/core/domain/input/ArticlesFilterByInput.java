package com.kino24.kino24newsread.core.domain.input;

import com.kino24.kino24newsread.core.domain.entity.ArticleCategory;

import java.util.List;
import java.util.UUID;

public record ArticlesFilterByInput(
        UUID id,
        String author,
        String publisher,
        List<ArticleCategory> categories,
        FilterStrategy strategy
) {
}
