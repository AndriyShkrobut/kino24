import React from 'react';
import { Button, Divider, SelectChangeEvent, Stack, Typography } from '@mui/material';
import { ColorMode, Layout } from 'models/enums';
import { SettingsFormValues } from 'containers/SettingsContainer';
import DarkModeSwitch from 'components/SettingsForm/DarkModeSwitch';
import LayoutSelect from 'components/SettingsForm/LayoutSelect';
import LayoutSizeRadioGroup from 'components/SettingsForm/LayoutSizeRadioGroup';

interface ISettingsFormProps {
    onSubmit: (event: React.FormEvent) => void;
    isSettingsChanged: boolean;
    colorMode: ColorMode;
    onToggleDarkMode: () => void;
    formValues: SettingsFormValues;
    onChangeLayoutSize: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onChangeLayout: (event: SelectChangeEvent<Layout>) => void;
}

const SettingsForm: React.FC<ISettingsFormProps> = ({
    onSubmit,
    isSettingsChanged,
    colorMode,
    onToggleDarkMode,
    formValues,
    onChangeLayoutSize,
    onChangeLayout,
}) => {
    return (
        <form onSubmit={onSubmit}>
            <Stack direction={'row'} alignItems={'center'} justifyContent={'space-between'} pt={2}>
                <Typography variant={'h5'}>Settings</Typography>
                <Button type={'submit'} variant={'contained'} disabled={!isSettingsChanged}>
                    Save
                </Button>
            </Stack>
            <Divider sx={{ my: 2 }} />
            <Stack spacing={4}>
                <DarkModeSwitch colorMode={colorMode} onToggle={onToggleDarkMode} />
                <LayoutSelect layout={formValues.layout} onChange={onChangeLayout} />
                {formValues.layout === Layout.FLUID && (
                    <LayoutSizeRadioGroup
                        layoutSize={formValues.layoutSize}
                        onChange={onChangeLayoutSize}
                    />
                )}
            </Stack>
        </form>
    );
};

export default SettingsForm;
