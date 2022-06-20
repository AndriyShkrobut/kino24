package com.kino24.kino24newsread.repository.jpa.builder;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.entity.ArticleCategory;
import com.kino24.kino24newsread.core.domain.input.FilterStrategy;
import org.springframework.data.jpa.domain.Specification;

import java.util.List;

public sealed interface ArticlesSpecificationBuilder
        permits ArticlesAllMatchSpecificationBuilder, ArticlesAnyMatchSpecificationBuilder {
    ArticlesSpecificationBuilder equalsIgnoreCase(String field, String value);

    ArticlesSpecificationBuilder equals(String field, Object value);

    ArticlesSpecificationBuilder hasCategories(List<ArticleCategory> categories);

    Specification<Article> build();

    static ArticlesSpecificationBuilder of(FilterStrategy strategy) {
        return switch (strategy) {
            case ALL_MATCH -> new ArticlesAllMatchSpecificationBuilder();
            case ANY_MATCH -> new ArticlesAnyMatchSpecificationBuilder();
        };
    }
}
