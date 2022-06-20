package com.kino24.kino24newsread.repository.jpa.builder;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.entity.ArticleCategory;
import com.kino24.kino24newsread.repository.jpa.specification.ArticleSpecifications;
import org.springframework.data.jpa.domain.Specification;

import java.util.List;

public final class ArticlesAllMatchSpecificationBuilder implements ArticlesSpecificationBuilder {
    private Specification<Article> specification = Specification.where(null);

    @Override
    public ArticlesAllMatchSpecificationBuilder equalsIgnoreCase(String field, String value) {
        if (value != null) {
            specification = specification.and(ArticleSpecifications.equalsIgnoreCase(field, value));
        }
        return this;
    }

    @Override
    public ArticlesSpecificationBuilder equals(String field, Object value) {
        if (value != null) {
            specification = specification.and(ArticleSpecifications.equals(field, value));
        }
        return this;
    }

    @Override
    public ArticlesAllMatchSpecificationBuilder hasCategories(List<ArticleCategory> categories) {
        if (categories != null) {
            categories.forEach(category ->
                    specification = specification.and(ArticleSpecifications.hasCategory(category))
            );
        }
        return this;
    }

    @Override
    public Specification<Article> build() {
        return specification;
    }
}
