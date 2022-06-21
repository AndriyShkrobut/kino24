export interface IUser {
    firstName: string;
    lastName: string;
    id: string;
    userName: string;
    email: string;
}

export interface ISignUp {
    name?: string;
    surname?: string;
    email?: string;
    password?: string;
    confirmPassword?: string;
}

export interface ISignIn {
    email?: string;
    password?: string;
    rememberMe: boolean;
}
