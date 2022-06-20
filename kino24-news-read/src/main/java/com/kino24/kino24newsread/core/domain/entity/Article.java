package com.kino24.kino24newsread.core.domain.entity;

import lombok.Data;

import javax.persistence.CollectionTable;
import javax.persistence.Column;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Table;
import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;

@Data
@Entity
@Table(name = "articles")
public class Article {
    @Id
    private UUID id;

    @Column(nullable = false)
    private String title;

    private String publicationDate;

    private String author;

    private String publisher;

    private String imageLink;

    @Column(nullable = false)
    private String content;

    private String linkToOriginal;

    @Enumerated(value = EnumType.STRING)
    @ElementCollection
    @CollectionTable(name = "article_category", joinColumns = @JoinColumn(name = "article_id"))
    @Column(name = "category")
    private List<ArticleCategory> categories;

    private ZonedDateTime timestamp;
}
