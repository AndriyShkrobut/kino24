package com.kino24.kino24newsread.repository.jpa.specification;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.entity.ArticleCategory;
import org.springframework.data.jpa.domain.Specification;

public class ArticleSpecifications {

    private ArticleSpecifications() {
    }

    public static Specification<Article> equalsIgnoreCase(String field, String value) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.equal(
                criteriaBuilder.lower(root.get(field)),
                value.toLowerCase()
        );
    }

    public static Specification<Article> equals(String field, Object value) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.equal(root.get(field), value);
    }

    public static Specification<Article> hasCategory(ArticleCategory category) {
        return (root, query, criteriaBuilder) -> criteriaBuilder.isMember(category, root.get("categories"));
    }

}
