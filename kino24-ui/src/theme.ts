import { ThemeOptions } from '@mui/material';
import Settings from 'providers/Settings';
import { Layout } from 'models/enums';
import { red } from '@mui/material/colors';

type GetThemeType = () => ThemeOptions;

const commonThemeOptions: ThemeOptions = {
    palette: {
        primary: {
            main: red[700],
        },
    },
};

export const getThemeOptions: GetThemeType = () => ({
    ...commonThemeOptions,
    palette: {
        ...commonThemeOptions.palette,
        mode: Settings.colorMode,
    },
    components: {
        ...commonThemeOptions,
        MuiContainer: {
            defaultProps: {
                fixed: Settings.layout === Layout.FIXED,
                maxWidth: Settings.layout === Layout.FLUID ? Settings.layoutSize : false,
            },
        },
    },
});
