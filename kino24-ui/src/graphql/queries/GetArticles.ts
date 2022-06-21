import { gql } from '@apollo/client';
import { IArticle } from 'models/Article';

export type GetArticles = {
    articles: {
        totalElements: number;
        totalPages: number;
        content: IArticle[];
    };
};

export type GetArticlesVariables = {
    page: number;
    size: number;
};

export const GET_ARTICLES = gql`
    query GetArticles($page: Int!, $size: Int!) {
        articles(page: $page, size: $size) {
            totalElements
            totalPages
            content {
                id
                title
                publicationDate
                imageLink
                linkToOriginal
                categories
                timestamp
            }
        }
    }
`;

export const GET_ARTICLES_AUTHORIZED = gql`
    query GetArticles($page: Int!, $size: Int!) {
        articles(page: $page, size: $size) {
            totalElements
            totalPages
            content {
                id
                title
                publicationDate
                imageLink
                linkToOriginal
                categories
                timestamp
                likesCount
                likedByMe
            }
        }
    }
`;

export const GET_ARTICLE_BY_ID = gql`
    query GetArticles($articleId: ID!) {
        articles(filterBy: { id: $articleId }) {
            content {
                id
                title
                publicationDate
                author
                publisher
                imageLink
                content
                linkToOriginal
                categories
                timestamp
                likesCount
                likedByMe
                commentsCount
                comments {
                    content {
                        id
                        text
                        likesCount
                        likedByMe
                        author {
                            id
                            firstName
                            lastName
                        }
                    }
                }
            }
        }
    }
`;
