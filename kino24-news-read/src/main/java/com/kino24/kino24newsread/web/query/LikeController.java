package com.kino24.kino24newsread.web.query;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.entity.Comment;
import com.kino24.kino24newsread.core.domain.service.LikeService;
import com.kino24.kino24newsread.spring.config.security.UserContext;
import lombok.RequiredArgsConstructor;
import org.springframework.graphql.data.method.annotation.SchemaMapping;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;

@Controller
@RequiredArgsConstructor
public class LikeController {
    private final LikeService likeService;
    private final UserContext userContext;

    @SchemaMapping
    @PreAuthorize("isAuthenticated()")
    public boolean likedByMe(Article article) {
        return likeService.isArticleLikePresent(article.getId(), userContext.getUserId().orElseThrow());
    }
    @SchemaMapping
    public int likesCount(Article article) {
        return likeService.getCountOfArticleLikes(article.getId());
    }

    @SchemaMapping
    @PreAuthorize("isAuthenticated()")
    public boolean likedByMe(Comment comment) {
        return likeService.isCommentLikePresent(comment.getId(), userContext.getUserId().orElseThrow());
    }

    @SchemaMapping
    public int likesCount(Comment comment) {
        return likeService.getCountOfCommentLikes(comment.getId());
    }

}
