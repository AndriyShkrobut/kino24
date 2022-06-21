import React from 'react';
import { AppBar, Box, Button, Container, Toolbar } from '@mui/material';
import { Logo } from 'components/Header/Logo';
import { useHistory } from 'react-router-dom';
import { MenuBar } from './MenuBar';
import AuthStore from 'store/AuthStore';

const Header: React.FC = () => {
    const history = useHistory();
    const handleOpenSignIn = () => {
        history.push('/signIn');
    };
    const [loggedIn, setLoggedIn] = React.useState(false);
    const getId = () => {
        if (AuthStore.getToken()) {
            setLoggedIn(true);
        }
    };
    React.useEffect(() => {
        getId();
    }, [loggedIn]);
    return (
        <AppBar position={'static'} enableColorOnDark>
            <Container>
                <Toolbar sx={{ justifyContent: 'space-between' }}>
                    <Logo />
                    <Box>
                        {loggedIn ? (
                            <Box>
                                <MenuBar />
                            </Box>
                        ) : (
                            <Button variant="outlined" color="inherit" onClick={handleOpenSignIn}>
                                Увійти
                            </Button>
                        )}
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default Header;
