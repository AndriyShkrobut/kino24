import React, { useMemo, useState } from 'react';
import SettingsContext, { ISettingsContext } from 'contexts/SettingsContext';
import { createTheme, ThemeProvider } from '@mui/material';
import { getThemeOptions } from 'theme';
import Settings from 'providers/Settings';
import { Layout, LayoutSize } from 'models/enums';

const SettingsProvider: React.FC = ({ children }) => {
    const [colorMode, setColorMode] = useState(Settings.colorMode);
    const [layout, setLayout] = useState(Settings.layout);
    const [layoutSize, setLayoutSize] = useState(Settings.layoutSize);

    const theme = useMemo(() => createTheme(getThemeOptions()), [colorMode, layout, layoutSize]);

    const onToggleColorMode = () => {
        Settings.toggleColorMode();
        setColorMode(Settings.colorMode);
    };

    const onChangeLayout = (newLayout: Layout) => {
        Settings.setLayout(newLayout);
        setLayout(newLayout);
    };

    const onChangeLayoutSize = (newLayoutSize: LayoutSize) => {
        Settings.setLayoutSize(newLayoutSize);
        setLayoutSize(newLayoutSize);
    };

    const value: ISettingsContext = {
        colorMode,
        onToggleColorMode,
        layout,
        onChangeLayout,
        layoutSize,
        onChangeLayoutSize,
    };

    return (
        <ThemeProvider theme={theme}>
            <SettingsContext.Provider value={value}>{children}</SettingsContext.Provider>
        </ThemeProvider>
    );
};

export default SettingsProvider;
