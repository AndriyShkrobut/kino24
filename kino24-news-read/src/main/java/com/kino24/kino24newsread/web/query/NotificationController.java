package com.kino24.kino24newsread.web.query;

import com.kino24.kino24newsread.core.domain.entity.Notification;
import com.kino24.kino24newsread.core.domain.messaging.NotificationsChannelConsumer;
import com.kino24.kino24newsread.core.domain.messaging.NotificationsObserver;
import com.kino24.kino24newsread.spring.config.security.JwtTokenParser;
import lombok.RequiredArgsConstructor;
import org.reactivestreams.Publisher;
import org.springframework.graphql.data.method.annotation.Argument;
import org.springframework.graphql.data.method.annotation.SubscriptionMapping;
import org.springframework.stereotype.Controller;
import reactor.core.publisher.Flux;

import java.util.UUID;

@Controller
@RequiredArgsConstructor
public class NotificationController {
    private final NotificationsChannelConsumer notificationsChannelConsumer;

    private final JwtTokenParser jwtTokenParser;

    @SubscriptionMapping
    Publisher<Notification> notification(@Argument String token) {
        UUID userId = jwtTokenParser.getUserIdFromJWT(token);
        return Flux.create(sink -> {
            NotificationsObserver observer = notification -> {
                if (notification.getUserId().equals(userId)) {
                    sink.next(notification);
                }
            };
            notificationsChannelConsumer.addObserver(observer);
            sink.onDispose(() -> notificationsChannelConsumer.removeObserver(observer));
        });
    }
}
