import React from 'react';
import { ColorMode } from 'models/enums';
import { FormControl, FormLabel, Switch } from '@mui/material';

interface IColorModeSwitchProps {
    colorMode: ColorMode;
    onToggle: () => void;
}

const DarkModeSwitch: React.FC<IColorModeSwitchProps> = ({ colorMode, onToggle }) => (
    <FormControl>
        <FormLabel htmlFor={'colorMode'}>Dark Theme</FormLabel>
        <Switch
            id={'colorMode'}
            name={'colorMode'}
            checked={colorMode === ColorMode.DARK}
            onChange={onToggle}
        />
    </FormControl>
);

export default DarkModeSwitch;
