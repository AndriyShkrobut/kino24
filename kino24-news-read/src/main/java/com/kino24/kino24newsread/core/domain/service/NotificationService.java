package com.kino24.kino24newsread.core.domain.service;

import com.kino24.kino24newsread.core.domain.entity.Notification;
import com.kino24.kino24newsread.core.domain.entity.User;
import com.kino24.kino24newsread.repository.jpa.NotificationRepository;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Service;

import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.Map;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class NotificationService {
    private final RedisTemplate<String, String> redisTemplate;
    private final ObjectMapper objectMapper;
    private final NotificationRepository notificationRepository;
    private final CommentService commentService;
    @Value("${redis.channel.notifications}")
    private final String notificationsChannel;

    @SneakyThrows
    public void processCommentLike(UUID commentId, User user) {
        var commentOpt = commentService.findById(commentId);
        if (commentOpt.isEmpty()) {
            return;
        }

        Notification notification = new Notification();
        notification.setId(UUID.randomUUID());
        notification.setUserId(commentOpt.get().getAuthor().getId());
        notification.setType("CommentLiked");
        notification.setTimestamp(ZonedDateTime.now().withZoneSameInstant(ZoneId.of("UTC")));
        notification.setPayload(objectMapper.writeValueAsString(
                Map.of(
                        "user", user,
                        "commentId", commentId,
                        "articleId", commentOpt.get().getArticle().getId()
                )
        ));

        notificationRepository.save(notification);

        String message = objectMapper.writeValueAsString(notification);
        redisTemplate.convertAndSend(notificationsChannel, message);
    }
}
