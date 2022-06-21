import * as React from 'react';
import { useHistory } from 'react-router-dom';
import { Button, CssBaseline, Grid, Box, Typography, Container } from '@mui/material';
import SignUpControls from '.';
import { signUpFields } from 'constants/authentication';
import { signUserUp } from 'actions/userActions';
import { confirmPassword } from './Validatiod';
import Notification from 'components/Notifications/Notifications';
import { ISignUp } from 'models/User';

const SignUp = () => {
    const initialState: ISignUp = {
        name: '',
        surname: '',
        email: '',
        password: '',
        confirmPassword: '',
    };
    const [formState, setFormState] = React.useState(initialState);
    const history = useHistory();
    const handleOnChange = (evt: any) => {
        evt.preventDefault();
        const value = evt.target.value;
        setFormState({
            ...formState,
            [evt.target.name]: value,
        });
    };
    const [valid, setValid] = React.useState(true);
    const handleSubmit = async (event: any) => {
        setValid(true);
        event.preventDefault();
        const validPass = confirmPassword(
            formState.password?.toString(),
            formState.confirmPassword?.toString()
        );
        if (!validPass) {
            setValid(false);
        }

        if (validPass) {
            if (await signUserUp(formState)) {
                history.push('/articles');
                location.reload();
            }
        }
    };

    return (
        <>
            {!valid && (
                <Notification title="Помилка паролю" text="Паролі не співпадають" isOpen={true} />
            )}
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Typography component="h1" variant="h5" mb={4}>
                        Реєстрація
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 3 }}>
                        <Grid container spacing={2}>
                            {signUpFields.map(
                                ({ gridP1, autoComplete, name, id, label, type }, i) => (
                                    <SignUpControls
                                        key={i}
                                        gridP1={gridP1}
                                        autoComplete={autoComplete}
                                        name={name}
                                        id={id}
                                        label={label}
                                        type={type}
                                        onChange={handleOnChange}
                                    />
                                )
                            )}
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            disabled={
                                !formState.name ||
                                !formState.surname ||
                                !formState.email ||
                                !formState.password ||
                                !formState.confirmPassword
                            }
                        >
                            Створити обліковий запис
                        </Button>
                    </Box>
                </Box>
            </Container>
        </>
    );
};
export default SignUp;
