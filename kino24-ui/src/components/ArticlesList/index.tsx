import React from 'react';
import { ArticlesListItem } from 'components/ArticlesList/ArticlesListItem';
import { IArticle } from 'models/Article';
import { Grid } from '@mui/material';

interface IArticlesListProps {
    articles: IArticle[];
}

const ArticlesList: React.FC<IArticlesListProps> = ({ articles }) => {
    return (
        <Grid
            mt={8}
            mb={2}
            justifyContent={'space-between'}
            alignItems={'start'}
            spacing={2}
            wrap={'wrap'}
            container
        >
            {articles.map((item) =>
                item ? (
                    <Grid key={item.id} item xs={12} md={6}>
                        <ArticlesListItem article={item} />
                    </Grid>
                ) : null
            )}
        </Grid>
    );
};

export default ArticlesList;
