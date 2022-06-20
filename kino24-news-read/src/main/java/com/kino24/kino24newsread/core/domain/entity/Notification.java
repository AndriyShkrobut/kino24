package com.kino24.kino24newsread.core.domain.entity;

import lombok.Data;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.time.ZonedDateTime;
import java.util.UUID;

@Data
@Entity
@Table(name = "notifications")
public class Notification {
    @Id
    private UUID id;

    private UUID userId;

    private String type;

    @Column(columnDefinition = "text")
    private String payload;

    private ZonedDateTime timestamp;
}
