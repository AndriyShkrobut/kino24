package com.kino24.kino24newsread.spring.config.security;

import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Component;

import java.util.Optional;
import java.util.UUID;

@Component
public class UserContext {

    public Optional<UUID> getUserId() {
        var authentication = SecurityContextHolder.getContext().getAuthentication();

        if (authentication == null) {
            return Optional.empty();
        }

        return Optional.ofNullable(authentication.getPrincipal())
                .filter(principal -> !principal.equals("anonymousUser"))
                .filter(principal -> principal instanceof UUID)
                .map(principal -> (UUID) principal);
    }

}
