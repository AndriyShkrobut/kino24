import React from 'react';
import { LayoutSize } from 'models/enums';
import { FormControl, FormControlLabel, FormLabel, Radio, RadioGroup } from '@mui/material';

interface ILayoutSizeRadioGroupProps {
    layoutSize: LayoutSize;
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const LayoutSizeRadioGroup: React.FC<ILayoutSizeRadioGroupProps> = ({ layoutSize, onChange }) => (
    <FormControl>
        <FormLabel htmlFor={'layoutSize'}>Layout Size</FormLabel>
        <RadioGroup
            id={'layoutSize'}
            name={'layoutSize'}
            value={layoutSize}
            onChange={onChange}
            row
        >
            <FormControlLabel control={<Radio />} label={'XS'} value={LayoutSize.XS} />
            <FormControlLabel control={<Radio />} label={'SM'} value={LayoutSize.SM} />
            <FormControlLabel control={<Radio />} label={'MD'} value={LayoutSize.MD} />
            <FormControlLabel control={<Radio />} label={'LG'} value={LayoutSize.LG} />
            <FormControlLabel control={<Radio />} label={'XL'} value={LayoutSize.XL} />
        </RadioGroup>
    </FormControl>
);

export default LayoutSizeRadioGroup;
