import React from 'react';
import { useHistory } from 'react-router-dom';
import { Typography } from '@mui/material';

export const Logo = () => {
    const history = useHistory();

    const handleGoHome = () => history.push('/');

    return (
        <Typography
            variant="h5"
            component="a"
            noWrap
            onClick={handleGoHome}
            color="inherit"
            sx={{ textDecoration: 'none', cursor: 'pointer' }}
        >
            КІНО 24
        </Typography>
    );
};
