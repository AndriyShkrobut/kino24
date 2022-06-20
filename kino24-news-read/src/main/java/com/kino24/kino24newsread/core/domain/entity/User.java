package com.kino24.kino24newsread.core.domain.entity;

import lombok.Data;

import javax.persistence.Embeddable;
import java.util.UUID;

@Data
@Embeddable
public class User {
    private UUID id;
    private String firstName;
    private String lastName;
}
