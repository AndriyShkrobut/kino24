import React from 'react';
import {
    Box,
    Button,
    Card,
    CardActions,
    CardContent,
    CardMedia,
    Chip,
    Stack,
    Typography,
} from '@mui/material';
import { IArticle } from 'models/Article';
import ArticleActions from 'components/ArticlesList/ArticleActions';
import AuthStore from 'store/AuthStore';
import { ArticleCategoryName } from 'constants/common';

interface IArticlesListItemProps {
    article: IArticle;
}

export const ArticlesListItem: React.FC<IArticlesListItemProps> = ({ article }) => {
    const { id, imageLink, linkToOriginal, title, likesCount, likedByMe, publicationDate } =
        article;

    return (
        <Card variant={'outlined'}>
            {imageLink && <CardMedia component="img" src={imageLink} />}
            <CardContent>
                <Typography variant="h6" noWrap title={title}>
                    {title}
                </Typography>
                <Stack
                    direction={'row'}
                    justifyContent={'space-between'}
                    alignItems={'center'}
                    mt={2}
                >
                    <Box>
                        {article.categories.map((item) => (
                            <Chip key={item} label={ArticleCategoryName[item]} variant={'filled'} />
                        ))}
                    </Box>
                    <Typography variant="subtitle2" color="text.secondary">
                        {publicationDate}
                    </Typography>
                </Stack>
            </CardContent>
            <CardActions sx={{ justifyContent: 'space-between', alignItems: 'center' }}>
                {AuthStore.getToken() && (
                    <ArticleActions articleId={id} likesCount={likesCount} likedByMe={likedByMe} />
                )}
                {linkToOriginal && (
                    <Button href={linkToOriginal} target={'_blank'} rel={'noreferrer noopenere'}>
                        Читати повністю
                    </Button>
                )}
            </CardActions>
        </Card>
    );
};
