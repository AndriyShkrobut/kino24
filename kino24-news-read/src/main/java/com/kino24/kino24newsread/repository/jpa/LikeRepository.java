package com.kino24.kino24newsread.repository.jpa;

import com.kino24.kino24newsread.core.domain.entity.Like;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.util.UUID;

@Repository
public interface LikeRepository extends JpaRepository<Like, UUID> {

    boolean existsByArticleIdAndUserId(UUID articleId, UUID userId);

    boolean existsByCommentIdAndUserId(UUID articleId, UUID userId);

    int countByArticleId(UUID articleId);

    int countByCommentId(UUID commentId);

    @Modifying
    @Transactional
    void deleteByArticleIdAndUserId(UUID articleId, UUID userId);

    @Modifying
    @Transactional
    void deleteByCommentIdAndUserId(UUID commentId, UUID userId);

}
