package com.kino24.kino24newsread.repository.jpa.builder;

import com.kino24.kino24newsread.core.domain.input.SortDirection;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;

@RequiredArgsConstructor
public class PageRequestBuilder {
    private final int page;
    private final int pageSize;
    private Sort sort = Sort.unsorted();

    public static PageRequestBuilder of(int page, int pageSize) {
        return new PageRequestBuilder(page, pageSize);
    }

    public PageRequestBuilder sortedBy(String field, SortDirection direction) {
        if (direction != null) {
            sort = switch (direction) {
                case ASC -> sort.and(Sort.by(field).ascending());
                case DESC -> sort.and(Sort.by(field).descending());
            };
        }
        return this;
    }

    public Pageable build() {
        return PageRequest.of(page, pageSize, sort);
    }
}
