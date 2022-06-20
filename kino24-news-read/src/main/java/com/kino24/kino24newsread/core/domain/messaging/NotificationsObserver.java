package com.kino24.kino24newsread.core.domain.messaging;

import com.kino24.kino24newsread.core.domain.entity.Notification;

@FunctionalInterface
public interface NotificationsObserver {
    void onNext(Notification notification);
}
