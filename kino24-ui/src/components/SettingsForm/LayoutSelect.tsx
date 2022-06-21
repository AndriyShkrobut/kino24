import React from 'react';
import { Layout } from 'models/enums';
import { FormControl, FormLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';

interface ILayoutSelect {
    layout: Layout;
    onChange: (event: SelectChangeEvent<Layout>) => void;
}

const LayoutSelect: React.FC<ILayoutSelect> = ({ layout, onChange }) => (
    <FormControl>
        <FormLabel htmlFor={'layout'}>Layout</FormLabel>
        <Select id={'layout'} name={'layout'} value={layout} onChange={onChange}>
            <MenuItem value={Layout.FLUID}>Fluid</MenuItem>
            <MenuItem value={Layout.FIXED}>Fixed</MenuItem>
        </Select>
    </FormControl>
);

export default LayoutSelect;
