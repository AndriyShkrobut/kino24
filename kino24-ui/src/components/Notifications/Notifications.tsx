import React from 'react';
import { Box, Collapse, Alert, AlertTitle } from '@mui/material';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';

interface INotification {
    title: string;
    text: string;
    isOpen: boolean;
}

const Notification: React.FC<INotification> = ({ title, text, isOpen }) => {
    const [open, setOpen] = React.useState(isOpen);

    return (
        <Box sx={{ width: '100%' }}>
            <Collapse in={open}>
                <Alert
                    severity="error"
                    action={
                        <IconButton
                            aria-label="close"
                            color="inherit"
                            size="small"
                            onClick={() => {
                                setOpen(false);
                            }}
                        >
                            <CloseIcon fontSize="inherit" />
                        </IconButton>
                    }
                    sx={{ mb: 2 }}
                >
                    <AlertTitle>{title}</AlertTitle>
                    {/* Operation is a successfull */}
                    {text} — <strong>check it out!</strong>
                </Alert>
            </Collapse>
        </Box>
    );
};
export default Notification;
