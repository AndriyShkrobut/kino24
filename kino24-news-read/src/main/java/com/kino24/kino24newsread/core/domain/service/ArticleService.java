package com.kino24.kino24newsread.core.domain.service;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.input.ArticlesFilterByInput;
import com.kino24.kino24newsread.core.domain.input.ArticlesOrderByInput;
import com.kino24.kino24newsread.repository.jpa.ArticleRepository;
import com.kino24.kino24newsread.repository.jpa.builder.ArticlesSpecificationBuilder;
import com.kino24.kino24newsread.repository.jpa.builder.PageRequestBuilder;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class ArticleService {
    private final ArticleRepository articleRepository;

    public void save(List<Article> articles) {
        articleRepository.saveAll(articles);
    }

    public Page<Article> find(int page, int size, ArticlesOrderByInput orderBy, ArticlesFilterByInput filterBy) {
        var specification = ArticlesSpecificationBuilder.of(filterBy.strategy())
                .equals("id", filterBy.id())
                .equalsIgnoreCase("author", filterBy.author())
                .equalsIgnoreCase("publisher", filterBy.publisher())
                .hasCategories(filterBy.categories())
                .build();

        var pageable = PageRequestBuilder.of(page, size)
                .sortedBy("timestamp", orderBy.timestamp())
                .build();

        return articleRepository.findAll(specification, pageable);
    }

    public Page<String> findAuthors(int page, int size, String search) {
        PageRequest pageRequest = PageRequest.of(page, size);
        search = search != null ? search.toLowerCase() : "";
        return articleRepository.findDistinctAuthors(pageRequest, search);
    }

    public Page<String> findPublishers(int page, int size, String search) {
        PageRequest pageRequest = PageRequest.of(page, size);
        search = search != null ? search.toLowerCase() : "";
        return articleRepository.findDistinctPublishers(pageRequest, search);
    }
}
