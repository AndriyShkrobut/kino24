package com.kino24.kino24newsread.core.domain.service;

import com.kino24.kino24newsread.core.domain.entity.Like;
import com.kino24.kino24newsread.repository.jpa.LikeRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
@RequiredArgsConstructor
public class LikeService {
    private final LikeRepository likeRepository;

    public boolean isArticleLikePresent(UUID articleId, UUID userId) {
        return likeRepository.existsByArticleIdAndUserId(articleId, userId);
    }
    public int getCountOfArticleLikes(UUID articleId) {
        return likeRepository.countByArticleId(articleId);
    }
    public boolean isCommentLikePresent(UUID commentId, UUID userId) {
        return likeRepository.existsByCommentIdAndUserId(commentId, userId);
    }
    public int getCountOfCommentLikes(UUID commentId) {
        return likeRepository.countByCommentId(commentId);
    }

    public void save(Like like) {
        likeRepository.save(like);
    }

    public void deleteArticleLike(UUID articleId, UUID userId) {
        likeRepository.deleteByArticleIdAndUserId(articleId, userId);
    }

    public void deleteCommentLike(UUID commentId, UUID userId) {
        likeRepository.deleteByCommentIdAndUserId(commentId, userId);
    }
}
