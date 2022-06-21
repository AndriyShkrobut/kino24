import { ArticleCategory } from 'models/enums';

export interface IArticle {
    id: string;
    title: string;
    publicationDate: string;
    imageLink?: string;
    content: string;
    linkToOriginal: string;
    categories: ArticleCategory[];
    timestamp: string;
    likesCount: number;
    likedByMe?: boolean;
    commentsCount: number;
    comments?: {
        content: IComment[];
    };
}

export interface IAuthor {
    id: string;
    firstName: string;
    lastName: string;
}

export interface IComment {
    id: string;
    text: string;
    likesCount: number;
    likedByMe?: boolean;
    author: IAuthor;
}
