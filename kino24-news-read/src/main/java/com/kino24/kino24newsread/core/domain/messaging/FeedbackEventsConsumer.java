package com.kino24.kino24newsread.core.domain.messaging;

import com.kino24.kino24newsread.core.domain.entity.Article;
import com.kino24.kino24newsread.core.domain.entity.Comment;
import com.kino24.kino24newsread.core.domain.entity.Like;
import com.kino24.kino24newsread.core.domain.entity.User;
import com.kino24.kino24newsread.core.domain.service.CommentService;
import com.kino24.kino24newsread.core.domain.service.LikeService;
import com.kino24.kino24newsread.core.domain.service.NotificationService;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.redis.connection.stream.MapRecord;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.data.redis.stream.StreamListener;
import org.springframework.stereotype.Component;

import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.Map;
import java.util.UUID;


@Component
@RequiredArgsConstructor
public class FeedbackEventsConsumer implements StreamListener<String, MapRecord<String, String, String>> {
    private static final Logger LOG = LoggerFactory.getLogger(FeedbackEventsConsumer.class);

    private final LikeService likeService;
    private final CommentService commentService;

    private final NotificationService notificationService;
    private final ObjectMapper objectMapper;
    private final RedisTemplate<String, String> redisTemplate;
    @Value("${redis.consumer-group.feedback-events}")
    private final String consumerGroup;

    @Override
    public void onMessage(MapRecord<String, String, String> message) {
        var event = message.getValue();

        try {
            var typeRef = new TypeReference<Map<String, Object>>() {
            };
            var payload = objectMapper.readValue(event.get("payload"), typeRef);
            var user = objectMapper.readValue(event.get("user"), User.class);
            switch (event.get("eventType")) {
                case "CommentArticleAdded" -> processCommentAdded(payload, user);
                case "CommentArticleRemoved" -> processCommentRemoved(payload, user);
                case "LikeArticleAdded" -> processArticleLikeAdded(payload, user);
                case "LikeArticleRemoved" -> processArticleLikeRemoved(payload, user);
                case "LikeCommentAdded" -> processCommentLikeAdded(payload, user);
                case "LikeCommentRemoved" -> processCommentLikeRemoved(payload, user);
                default -> LOG.info("Unknown event type {}. Skipping event from processing", event.get("eventType"));
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            redisTemplate.opsForStream().acknowledge(consumerGroup, message);
        }
    }

    private void processCommentAdded(Map<String, Object> payload, User user) {
        UUID articleId = extractUUID(payload, "articleId");
        Article article = new Article();
        article.setId(articleId);

        Comment comment = new Comment();
        comment.setId(extractUUID(payload, "commentId"));
        comment.setArticle(article);
        comment.setText((String) payload.get("text"));
        comment.setAuthor(user);
        comment.setTimestamp(ZonedDateTime.now().withZoneSameInstant(ZoneId.of("UTC")));

        commentService.save(comment);
    }

    private void processCommentRemoved(Map<String, Object> payload, User user) {
        UUID id = extractUUID(payload, "commentId");
        commentService.deleteById(id);
    }

    private void processArticleLikeAdded(Map<String, Object> payload, User user) {
        UUID articleId = extractUUID(payload, "articleId");
        Article article = new Article();
        article.setId(articleId);

        Like like = new Like();
        like.setId(UUID.randomUUID());
        like.setUserId(user.getId());
        like.setArticle(article);
        like.setTimestamp(ZonedDateTime.now().withZoneSameInstant(ZoneId.of("UTC")));

        likeService.save(like);
    }

    private void processArticleLikeRemoved(Map<String, Object> payload, User user) {
        UUID articleId = extractUUID(payload, "articleId");
        likeService.deleteArticleLike(articleId, user.getId());
    }

    private void processCommentLikeAdded(Map<String, Object> payload, User user) {
        UUID commentId = extractUUID(payload, "commentId");
        Comment comment = new Comment();
        comment.setId(commentId);

        Like like = new Like();
        like.setId(UUID.randomUUID());
        like.setUserId(user.getId());
        like.setComment(comment);
        like.setTimestamp(ZonedDateTime.now().withZoneSameInstant(ZoneId.of("UTC")));

        likeService.save(like);
        notificationService.processCommentLike(commentId, user);
    }

    private void processCommentLikeRemoved(Map<String, Object> payload, User user) {
        UUID commentId = extractUUID(payload, "commentId");
        likeService.deleteCommentLike(commentId, user.getId());
    }
    private UUID extractUUID(Map<String, Object> payload, String field) {
        return UUID.fromString((String) payload.get(field));
    }
}
