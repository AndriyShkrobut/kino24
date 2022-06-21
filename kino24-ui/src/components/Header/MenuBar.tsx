import React, { useContext } from 'react';
import { IconButton, Menu, MenuItem, Stack, Typography } from '@mui/material';
import { AccountCircle } from '@mui/icons-material';
import Brightness4Icon from '@mui/icons-material/Brightness4';
import Brightness7Icon from '@mui/icons-material/Brightness7';

import { logOut } from 'actions/userActions';
import SettingsContext from 'contexts/SettingsContext';

export const MenuBar = () => {
    const settingsContext = useContext(SettingsContext);
    const [anchorUserMenu, setAnchorUserMenu] = React.useState<HTMLElement | null>(null);

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorUserMenu(event.currentTarget);
    };

    const handleCloseUserMenu = () => {
        setAnchorUserMenu(null);
    };

    const handeLogOut = async () => {
        await logOut();
        location.reload();
    };

    return (
        <React.Fragment>
            <Stack direction={'row'} spacing={4}>
                <IconButton
                    sx={{ ml: 1 }}
                    onClick={settingsContext.onToggleColorMode}
                    color="inherit"
                >
                    {settingsContext.colorMode === 'dark' ? (
                        <Brightness7Icon />
                    ) : (
                        <Brightness4Icon />
                    )}
                </IconButton>
                <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }} color="inherit">
                    <AccountCircle />
                </IconButton>
            </Stack>
            <Menu
                sx={{ mt: '45px' }}
                anchorEl={anchorUserMenu}
                anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
                transformOrigin={{ vertical: 'top', horizontal: 'right' }}
                open={Boolean(anchorUserMenu)}
                onClose={handleCloseUserMenu}
                keepMounted
            >
                <MenuItem onClick={handleCloseUserMenu}>
                    <Typography onClick={handeLogOut}>Вийти</Typography>
                </MenuItem>
            </Menu>
        </React.Fragment>
    );
};
