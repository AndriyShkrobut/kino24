package com.kino24.kino24newsread.repository.jpa;

import com.kino24.kino24newsread.core.domain.entity.Comment;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface CommentRepository extends JpaRepository<Comment, UUID> {

    Page<Comment> findByArticleId(UUID articleId, Pageable pageable);

    int countByArticleId(UUID articleId);

}
