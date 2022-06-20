package com.kino24.kino24newsread.core.domain.service;

import com.kino24.kino24newsread.core.domain.entity.Comment;
import com.kino24.kino24newsread.repository.jpa.CommentRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class CommentService {
    private final CommentRepository commentRepository;

    public Page<Comment> findCommentsByArticleId(UUID articleId, int page, int size) {
        var pageable = PageRequest.of(page, size);
        return commentRepository.findByArticleId(articleId, pageable);
    }

    public int getCountOfComments(UUID articleId) {
        return commentRepository.countByArticleId(articleId);
    }

    public void save(Comment comment) {
        commentRepository.save(comment);
    }

    public void deleteById(UUID id) {
        commentRepository.deleteById(id);
    }

    public Optional<Comment> findById(UUID commentId) {
        return commentRepository.findById(commentId);
    }
}
