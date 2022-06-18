package com.kino24.kino24parser.core.domain.service.extractor;

import com.kino24.kino24parser.core.domain.model.Kino24Article;

import java.util.List;

public interface Kino24Subscriber {
    void consume(List<Kino24Article> articles);
}
