import * as React from 'react';
import { Link, useHistory } from 'react-router-dom';
import { Button, CssBaseline, Grid, Box, Typography, Container } from '@mui/material';
import SignUpControls from '.';
import { signInFields } from 'constants/authentication';
import { signUserIn } from 'actions/userActions';
import Notification from 'components/Notifications/Notifications';
import { ISignIn } from 'models/User';

const SignIn = () => {
    const initialState: ISignIn = {
        email: '',
        password: '',
        rememberMe: true,
    };
    const [state, setState] = React.useState(initialState);
    const [stateNotification, setNotification] = React.useState(false);
    const history = useHistory();
    const handleSubmit = async (event: any) => {
        event.preventDefault();

        if (await signUserIn(state)) {
            history.push('/articles');
            location.reload();
        } else {
            setNotification(true);
        }
    };
    const handleOnChange = (evt: any) => {
        evt.preventDefault();
        const value = evt.target.value;
        setState({
            ...state,
            [evt.target.name]: value,
        });
    };
    return (
        <>
            {!setNotification && (
                <Notification title="Помилка даних" text="Спробуйте ще раз" isOpen={true} />
            )}
            <Container component="main" maxWidth="xs">
                <Notification
                    isOpen={stateNotification}
                    title="Помилка Входу"
                    text="Невірна пошта або пароль"
                />

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
                        Вхід в обліковий запис
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
                        <Grid container spacing={2}>
                            {signInFields.map(({ gridP1, autoComplete, name, id, label, type }) => (
                                <SignUpControls
                                    key={id}
                                    gridP1={gridP1}
                                    autoComplete={autoComplete}
                                    name={name}
                                    id={id}
                                    label={label}
                                    type={type}
                                    onChange={handleOnChange}
                                />
                            ))}
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            disabled={!state.email || !state.password}
                        >
                            Увійти
                        </Button>
                        <Grid container>
                            <Grid item>
                                Немає обліково запису? <Link to="/signUp">{'Зареєструватись'}</Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </>
    );
};
export default SignIn;
