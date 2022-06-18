package com.kino24.kino24parser.core.domain.model;

import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.Collection;
import java.util.List;
import java.util.Set;
import java.util.UUID;
import java.util.stream.Collectors;

public record Kino24Article(

        UUID id,
        String title,
        String publicationDate,
        String author,
        String publisher,
        String imageLink,
        String content,
        String linkToOriginal,
        Set<String> categories,

        ZonedDateTime timestamp
) {
    public static Kino24Article mergeCategories(List<Kino24Article> articles) {
        var categories = articles.stream()
                .map(Kino24Article::categories)
                .flatMap(Collection::stream)
                .collect(Collectors.toSet());

        return Kino24Article.of(
                articles.get(0).title,
                articles.get(0).publicationDate,
                articles.get(0).author,
                articles.get(0).publisher,
                articles.get(0).imageLink,
                articles.get(0).content,
                articles.get(0).linkToOriginal,
                categories
        );
    }
    public static Kino24Article of(String title,
                                   String publicationDate,
                                   String author,
                                   String publisher,
                                   String imageLink,
                                   String content,
                                   String linkToOriginal,
                                   Set<String> categories) {
        return new Kino24Article(
                UUID.randomUUID(),
                title,
                publicationDate,
                author,
                publisher,
                imageLink,
                content,
                linkToOriginal,
                categories,
                ZonedDateTime.now().withZoneSameInstant(ZoneId.of("UTC"))
        );
    }
}
