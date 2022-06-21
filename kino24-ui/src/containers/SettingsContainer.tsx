import React, { useContext, useMemo, useState } from 'react';
import { SelectChangeEvent } from '@mui/material';
import SettingsContext from 'contexts/SettingsContext';
import { Layout, LayoutSize } from 'models/enums';
import SettingsForm from 'components/SettingsForm';

export type SettingsFormValues = {
    layout: Layout;
    layoutSize: LayoutSize;
};

const SettingsContainer = () => {
    const settingsContext = useContext(SettingsContext);
    const [settingsFormValues, setSettingsFormValues] = useState<SettingsFormValues>({
        layout: settingsContext.layout,
        layoutSize: settingsContext.layoutSize,
    });

    const isSettingsChanged = useMemo(() => {
        const { layout, layoutSize } = settingsFormValues;
        const { layout: currentLayout, layoutSize: currentLayoutSize } = settingsContext;

        return layout !== currentLayout || layoutSize !== currentLayoutSize;
    }, [settingsFormValues, settingsContext]);

    const handleChangeLayout = (event: SelectChangeEvent<Layout>) => {
        const value = event.target.value as Layout;
        setSettingsFormValues((prevState) => ({ ...prevState, layout: value }));
    };

    const handleChangeLayoutSize = (event: React.ChangeEvent<HTMLInputElement>) => {
        const value = event.target.value as LayoutSize;
        setSettingsFormValues((prevState) => ({ ...prevState, layoutSize: value }));
    };

    const handleSettingsSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        const { layout, layoutSize } = settingsFormValues;
        settingsContext.onChangeLayout(layout);
        settingsContext.onChangeLayoutSize(layoutSize);
    };

    return (
        <SettingsForm
            onSubmit={handleSettingsSubmit}
            isSettingsChanged={isSettingsChanged}
            colorMode={settingsContext.colorMode}
            onToggleDarkMode={settingsContext.onToggleColorMode}
            formValues={settingsFormValues}
            onChangeLayoutSize={handleChangeLayoutSize}
            onChangeLayout={handleChangeLayout}
        />
    );
};

export default SettingsContainer;
