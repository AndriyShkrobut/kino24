package com.kino24.kino24newsread.web.query;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.entity.Comment;
import com.kino24.kino24newsread.core.domain.service.CommentService;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.stereotype.Controller;

@Controller
@RequiredArgsConstructor
public class CommentController {
    private final CommentService commentService;

    @SchemaMapping
    public int commentsCount(Article article) {
        return commentService.getCountOfComments(article.getId());
    }

    @SchemaMapping
    public Page<Comment> comments(Article article, @Argument int page, @Argument int size) {
        return commentService.findCommentsByArticleId(article.getId(), page, size);
    }

}
