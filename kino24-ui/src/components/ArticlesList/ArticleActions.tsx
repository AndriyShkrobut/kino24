import React from 'react';
import { Box, Button } from '@mui/material';
import FavoriteIcon from '@mui/icons-material/Favorite';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import { addLike, removeLike } from 'actions/userActions';

export interface IArticleActions {
    articleId: string;
    likedByMe?: boolean;
    likesCount: number;
}

const ArticleActions: React.FC<IArticleActions> = ({ articleId, likedByMe, likesCount }) => {
    const [currLikedByMe, setCurrLikedByMe] = React.useState(likedByMe);
    const [currLikesCount, setCurrLikesCount] = React.useState(likesCount);

    const handleOnClickLike = () => {
        if (currLikedByMe) {
            removeLike(articleId).then(() => {
                setCurrLikesCount(currLikesCount - 1);
                setCurrLikedByMe(!currLikedByMe);
            });
        } else {
            addLike(articleId).then(() => {
                setCurrLikesCount(currLikesCount + 1);
                setCurrLikedByMe(!currLikedByMe);
            });
        }
    };
    return (
        <Box>
            <Button
                startIcon={currLikedByMe ? <FavoriteIcon /> : <FavoriteBorderIcon />}
                onClick={handleOnClickLike}
            >
                {currLikesCount}
            </Button>
        </Box>
    );
};

export default ArticleActions;
