package com.kino24.kino24parser.core.domain.model;

import lombok.Builder;
import lombok.Data;

import java.time.ZonedDateTime;
import java.util.UUID;

@Data
@Builder
public class Event {
    private UUID eventId;
    private String eventType;
    private String payload;
    private ZonedDateTime timestamp;
}
