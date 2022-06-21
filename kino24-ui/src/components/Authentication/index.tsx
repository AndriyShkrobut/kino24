import React from 'react';
import { TextField, Grid } from '@mui/material';
import { IAuthenticationProps } from 'models/Authentication';

const AuthorithationControls: React.FC<IAuthenticationProps> = ({
    gridP1,
    autoComplete,
    name,
    id,
    label,
    type,
    onChange,
}) => {
    return (
        <>
            <Grid item xs={gridP1}>
                <TextField
                    autoComplete={autoComplete}
                    name={name}
                    id={id}
                    label={label}
                    required
                    fullWidth
                    type={type}
                    onChange={onChange}
                />
            </Grid>
        </>
    );
};

export default AuthorithationControls;
