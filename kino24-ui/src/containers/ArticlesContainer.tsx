import React, { useEffect, useState } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import { useQuery } from '@apollo/client';
import { Box, CircularProgress, Grid, Pagination } from '@mui/material';

import ArticlesList from 'components/ArticlesList';

import { DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE, PAGE_SIZES } from 'constants/common';
import {
    GET_ARTICLES,
    GetArticles,
    GetArticlesVariables,
    GET_ARTICLES_AUTHORIZED,
} from 'graphql/queries/GetArticles';
import useMounted from 'hooks/useMounted';
import AuthStore from 'store/AuthStore';
import PageSize from '../components/PageSize/PageSize';

enum QueryParams {
    PAGE = 'page',
    SIZE = 'size',
}

const ArticlesContainer = () => {
    const location = useLocation();
    const history = useHistory();
    const isMounted = useMounted();
    const [pageNumber, setPageNumber] = useState(DEFAULT_PAGE_NUMBER);
    const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE);
    const [totalPages, setTotalPages] = useState(0);
    const { data, error, loading } = AuthStore.getToken()
        ? useQuery<GetArticles, GetArticlesVariables>(GET_ARTICLES_AUTHORIZED, {
              variables: { page: pageNumber - 1, size: pageSize },
          })
        : useQuery<GetArticles, GetArticlesVariables>(GET_ARTICLES, {
              variables: { page: pageNumber - 1, size: pageSize },
          });

    const handlePageChange = (event: React.ChangeEvent<unknown>, newPage: number) => {
        setPageNumber(newPage);
    };

    const handleSizeChange = (newSize: number) => {
        setPageSize(newSize);
    };

    useEffect(() => {
        if (!data) return;

        setTotalPages(data.articles.totalPages);
    }, [data]);

    useEffect(() => {
        if (totalPages && pageNumber > totalPages) {
            setPageNumber(totalPages);
        }
    }, [pageNumber, totalPages]);

    useEffect(() => {
        const queryParamsString = location.search.slice(1);
        const searchParams = new URLSearchParams(queryParamsString);

        const pageNumberQueryParam = Number(searchParams.get(QueryParams.PAGE));
        if (pageNumberQueryParam) {
            const isValidPageNumber = pageNumberQueryParam > 0;
            if (isValidPageNumber) {
                setPageNumber(pageNumberQueryParam);
            }
        }

        const pageSizeQueryParam = Number(searchParams.get(QueryParams.SIZE));
        if (pageSizeQueryParam) {
            const isValidPageSize = PAGE_SIZES.includes(pageSizeQueryParam);
            if (isValidPageSize) {
                setPageSize(pageSizeQueryParam);
            }
        }
    }, []);

    useEffect(() => {
        if (!isMounted) return;

        const queryParamsString = location.search.slice(1);
        const currentSearchParams = new URLSearchParams(queryParamsString);

        currentSearchParams.set(QueryParams.PAGE, String(pageNumber));
        currentSearchParams.set(QueryParams.SIZE, String(pageSize));

        history.replace(`${location.pathname}?${currentSearchParams.toString()}`);
    }, [pageNumber, pageSize]);

    return (
        <React.Fragment>
            {!loading && !error && data?.articles ? (
                <ArticlesList articles={data.articles.content} />
            ) : (
                <Box sx={{ display: 'flex', justifyContent: 'center', p: 4 }}>
                    <CircularProgress />
                </Box>
            )}
            {totalPages ? (
                <Grid container justifyContent={'space-between'} alignItems={'center'}>
                    <Grid item xs={3} />
                    <Grid item xs={6} justifyContent={'center'}>
                        <Grid container justifyContent={'center'}>
                            <Grid item>
                                <Pagination
                                    variant={'text'}
                                    page={pageNumber}
                                    count={totalPages}
                                    onChange={handlePageChange}
                                />
                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid item xs={3}>
                        <Grid container justifyContent={'end'}>
                            <Grid item>
                                <PageSize size={pageSize} onChange={handleSizeChange} />
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            ) : null}
        </React.Fragment>
    );
};

export default ArticlesContainer;
