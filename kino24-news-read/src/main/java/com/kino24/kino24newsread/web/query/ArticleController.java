package com.kino24.kino24newsread.web.query;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.input.ArticlesFilterByInput;
import com.kino24.kino24newsread.core.domain.input.ArticlesOrderByInput;
import com.kino24.kino24newsread.core.domain.service.ArticleService;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.QueryMapping;
import org.springframework.stereotype.Controller;

@Controller
@RequiredArgsConstructor
public class ArticleController {
    private final ArticleService articleService;

    @QueryMapping
    public Page<Article> articles(@Argument int page, @Argument int size,
                                  @Argument ArticlesOrderByInput orderBy, @Argument ArticlesFilterByInput filterBy) {
        return articleService.find(page, size, orderBy, filterBy);
    }

    @QueryMapping
    public Page<String> authors(@Argument int page, @Argument int size, @Argument String search) {
        return articleService.findAuthors(page, size, search);
    }

    @QueryMapping
    public Page<String> publishers(@Argument int page, @Argument int size, @Argument String search) {
        return articleService.findPublishers(page, size, search);
    }
}
