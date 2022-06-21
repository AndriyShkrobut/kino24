import { IAuthenticationProps } from 'models/Authentication';

export const signUpFields: IAuthenticationProps[] = [
    {
        gridP1: 12,
        autoComplete: 'given-name',
        name: 'name',
        id: 'name',
        label: `Ім'я`,
        type: 'text',
    },
    {
        gridP1: 12,
        autoComplete: 'family-name',
        name: 'surname',
        id: 'surname',
        label: 'Прізвище',
        type: 'text',
    },
    {
        gridP1: 12,
        autoComplete: 'email',
        name: 'email',
        id: 'email',
        label: 'Пошта',
        type: 'email',
    },
    {
        gridP1: 12,
        autoComplete: 'new-password',
        name: 'password',
        id: 'password',
        label: 'Пароль',
        type: 'password',
    },
    {
        gridP1: 12,
        autoComplete: 'new-password',
        name: 'confirmPassword',
        id: 'confirmPassword',
        label: 'Підтвердження паролю',
        type: 'password',
    },
];

export const signInFields: IAuthenticationProps[] = [
    {
        gridP1: 12,
        autoComplete: 'email',
        name: 'email',
        id: 'email',
        label: 'Пошта',
        type: 'email',
    },
    {
        gridP1: 12,
        autoComplete: 'current-password',
        name: 'password',
        id: 'password',
        label: 'Пароль',
        type: 'password',
    },
];
