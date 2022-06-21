export const confirmPassword = (
    password: string | undefined,
    confirmPassword: string | undefined
) => {
    return password === confirmPassword;
};
