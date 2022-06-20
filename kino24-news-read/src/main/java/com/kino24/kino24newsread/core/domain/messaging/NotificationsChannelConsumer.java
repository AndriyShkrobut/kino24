package com.kino24.kino24newsread.core.domain.messaging;

import com.kino24.kino24newsread.core.domain.entity.Notification;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.data.redis.connection.Message;
import org.springframework.data.redis.connection.MessageListener;
import org.springframework.stereotype.Component;

import java.util.HashSet;
import java.util.Set;

@Component
@RequiredArgsConstructor
public class NotificationsChannelConsumer implements MessageListener {
    private final ObjectMapper objectMapper;
    private Set<NotificationsObserver> observers = new HashSet<>();

    @Override
    @SneakyThrows
    public void onMessage(Message message, byte[] pattern) {
        var notification = objectMapper.readValue(message.toString(), Notification.class);
        observers.forEach(observer -> observer.onNext(notification));
    }

    public void addObserver(NotificationsObserver observer) {
        observers.add(observer);
    }

    public void removeObserver(NotificationsObserver observer) {
        observers.remove(observer);
    }
}
