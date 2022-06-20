package com.kino24.kino24newsread.spring.config;

import com.kino24.kino24newsread.core.domain.messaging.FeedbackEventsConsumer;
import com.kino24.kino24newsread.core.domain.messaging.NewArticlesConsumer;
import com.kino24.kino24newsread.core.domain.messaging.NotificationsChannelConsumer;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.PropertySource;
import org.springframework.data.redis.connection.RedisConnectionFactory;
import org.springframework.data.redis.connection.stream.Consumer;
import org.springframework.data.redis.connection.stream.ReadOffset;
import org.springframework.data.redis.connection.stream.StreamOffset;
import org.springframework.data.redis.listener.ChannelTopic;
import org.springframework.data.redis.listener.RedisMessageListenerContainer;
import org.springframework.data.redis.listener.adapter.MessageListenerAdapter;
import org.springframework.data.redis.stream.StreamMessageListenerContainer;
import org.springframework.data.redis.stream.Subscription;

import java.time.Duration;
import java.util.UUID;

@Configuration
@RequiredArgsConstructor
@PropertySource("classpath:application.properties")
public class RedisConfig {
    @Value("${redis.stream.news-events}")
    private final String newsDomainEventsStream;
    @Value("${redis.stream.feedback-events}")
    private final String feedbackEventsStream;
    @Value("${redis.consumer-group.news-events}")
    private final String newsDomainEventsConsumerGroup;
    @Value("${redis.consumer-group.feedback-events}")
    private final String feedbackEventsConsumerGroup;
    @Value("${redis.channel.notifications}")
    private final String notificationsChannel;
    private final NewArticlesConsumer newArticlesConsumer;
    private final FeedbackEventsConsumer feedbackEventsConsumer;
    private final NotificationsChannelConsumer notificationsChannelConsumer;
    private final RedisConnectionFactory redisConnectionFactory;

    @Bean
    Subscription newsDomainEventsSubscription() {
        var options =
                StreamMessageListenerContainer.StreamMessageListenerContainerOptions.builder()
                        .pollTimeout(Duration.ofSeconds(1))
                        .build();

        var listenerContainer =
                StreamMessageListenerContainer.create(redisConnectionFactory, options);

        var consumer = Consumer.from(newsDomainEventsConsumerGroup, newsDomainEventsConsumerGroup + UUID.randomUUID());

        var streamOffset = StreamOffset.create(newsDomainEventsStream, ReadOffset.lastConsumed());
        var subscription = listenerContainer.receive(consumer, streamOffset, newArticlesConsumer);
        listenerContainer.start();
        return subscription;
    }

    @Bean
    Subscription feedbackEventsSubscription() {
        var options =
                StreamMessageListenerContainer.StreamMessageListenerContainerOptions.builder()
                        .pollTimeout(Duration.ofSeconds(1))
                        .build();

        var listenerContainer =
                StreamMessageListenerContainer.create(redisConnectionFactory, options);

        var consumer = Consumer.from(feedbackEventsConsumerGroup, feedbackEventsConsumerGroup + UUID.randomUUID());

        var streamOffset = StreamOffset.create(feedbackEventsStream, ReadOffset.lastConsumed());
        var subscription = listenerContainer.receive(consumer, streamOffset, feedbackEventsConsumer);
        listenerContainer.start();
        return subscription;
    }

    @Bean
    MessageListenerAdapter notificationsMessageListener() {
        return new MessageListenerAdapter(notificationsChannelConsumer);
    }

    @Bean
    RedisMessageListenerContainer redisContainer() {
        final RedisMessageListenerContainer container = new RedisMessageListenerContainer();
        container.setConnectionFactory(redisConnectionFactory);
        container.addMessageListener(notificationsMessageListener(), new ChannelTopic(notificationsChannel));
        return container;
    }
}
