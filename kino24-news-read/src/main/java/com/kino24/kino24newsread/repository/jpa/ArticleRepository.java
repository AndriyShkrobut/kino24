package com.kino24.kino24newsread.repository.jpa;

import com.kino24.kino24newsread.core.domain.entity.Article;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface ArticleRepository extends JpaRepository<Article, UUID>, JpaSpecificationExecutor<Article> {
    @Query(
            value = "select distinct(author) from Article where lower(author) like %:search% and author is not null",
            countQuery = "select count(distinct(author)) from Article where lower(author) like %:search% and author is not null"
    )
    Page<String> findDistinctAuthors(Pageable pageable, String search);

    @Query(
            value = "select distinct(publisher) from Article where lower(publisher) like %:search% and publisher is not null",
            countQuery = "select count(distinct(publisher)) from Article where lower(publisher) like %:search% and publisher is not null"
    )
    Page<String> findDistinctPublishers(Pageable pageable, String search);
}
