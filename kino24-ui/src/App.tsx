import React from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import { Container } from '@mui/material';
import Header from 'components/Header';
import SignUp from 'components/Authentication/SignUp';
import SignIn from 'components/Authentication/SignIn';
import ArticlesContainer from 'containers/ArticlesContainer';
import NotificationProvider from 'providers/NotificationProvider';

const App = () => {
    return (
        <NotificationProvider>
            <Header />
            <Container sx={{ pb: 8 }}>
                <Switch>
                    <Route path={'/articles'} exact>
                        <ArticlesContainer />
                    </Route>

                    <Route path={'/signUp'} exact>
                        <SignUp />
                    </Route>

                    <Route path={'/signIn'} exact>
                        <SignIn />
                    </Route>
                    <Redirect from={'/'} to={'/articles'} />
                </Switch>
            </Container>
        </NotificationProvider>
    );
};

export default App;
