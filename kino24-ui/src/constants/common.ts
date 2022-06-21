import { OptionsObject } from 'notistack';
import { ArticleCategory } from '../models/enums';

export const LOCALE_UA = 'uk-UA';

// Articles
export const PAGE_SIZES = [10, 20, 30];
export const DEFAULT_PAGE_NUMBER = 1;
export const DEFAULT_PAGE_SIZE = PAGE_SIZES[0];

export const BASE_URL = process.env.REACT_APP_KINO24_USER_API;
export const LIKE_URL = process.env.REACT_APP_KINO24_LIKE_API;
export const SNACKBAR_CONFIG: OptionsObject = {
    variant: 'info',
    autoHideDuration: 5000,
    anchorOrigin: { vertical: 'bottom', horizontal: 'right' },
};

export const ArticleCategoryName = {
    [ArticleCategory.MOVIES]: 'ФІЛЬМИ',
    [ArticleCategory.SERIES]: 'СЕРІАЛИ',
};
