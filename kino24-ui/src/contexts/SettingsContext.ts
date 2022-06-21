import React from 'react';
import { ColorMode, Layout, LayoutSize } from 'models/enums';

export interface ISettingsContext {
    colorMode: ColorMode;
    onToggleColorMode: () => void;
    layout: Layout;
    onChangeLayout: (newLayout: Layout) => void;
    layoutSize: LayoutSize;
    onChangeLayoutSize: (newLayoutSize: LayoutSize) => void;
}

const SettingsContext = React.createContext<ISettingsContext>({} as ISettingsContext);

export default SettingsContext;
