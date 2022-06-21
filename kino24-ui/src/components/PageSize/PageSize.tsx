import React from 'react';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import { PAGE_SIZES } from 'constants/common';

interface IPageSizeProps {
    size: number;
    onChange: (newSize: number) => void;
}

const PageSize: React.FC<IPageSizeProps> = ({ size, onChange }) => {
    const handleChange = (event: SelectChangeEvent) => {
        const newSize = Number(event.target.value);

        onChange(newSize);
    };

    return (
        <FormControl sx={{ width: 75 }} size={'small'}>
            <InputLabel id="page-size">Розмір</InputLabel>
            <Select
                labelId={'page-size'}
                id={'page-size'}
                label={'Розмір'}
                value={String(size)}
                onChange={handleChange}
            >
                {PAGE_SIZES.map((item) => (
                    <MenuItem key={item} value={item}>
                        {item}
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

export default PageSize;
