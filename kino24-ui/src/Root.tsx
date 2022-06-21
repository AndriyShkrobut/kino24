import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import { CssBaseline } from '@mui/material';

import ApolloClientProvider from 'providers/ApolloClientProvider';
import SettingsProvider from 'providers/SettingsProvider';
import { SnackbarProvider } from 'notistack';

const Root: React.FC = ({ children }) => {
    return (
        <React.StrictMode>
            <SettingsProvider>
                <SnackbarProvider maxSnack={5}>
                    <CssBaseline />
                    <Router>
                        <ApolloClientProvider>{children}</ApolloClientProvider>
                    </Router>
                </SnackbarProvider>
            </SettingsProvider>
        </React.StrictMode>
    );
};

export default Root;
