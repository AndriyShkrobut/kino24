import api from 'api/api';
import AuthStore from 'store/AuthStore';
import { BASE_URL, LIKE_URL } from 'constants/common';
import { ISignIn, ISignUp } from 'models/User';

export const signUserIn = async (userInfo: ISignIn) => {
    return await api
        .post(`${BASE_URL}/api/Auth/signin`, userInfo)
        .then((response) => {
            if (response.data.token) {
                AuthStore.setToken(response.data.token);
                return true;
            }
        })
        .catch((error: any) => {
            return error.data;
        });
};
export const signUserUp = async (userInfo: ISignUp) => {
    return await api
        .post(`${BASE_URL}/api/Auth/signup`, userInfo)
        .then((response) => {
            if (response.data.token) {
                AuthStore.setToken(response.data.token);
                return response.status;
            }
        })
        .catch((error) => {
            if (error.response.status === 400) {
                return error.response.status;
            }
        });
};
export const logOut = async () => {
    return await api.get(`${BASE_URL}/api/Auth/logout`).then(() => {
        AuthStore.removeToken();
    });
};

export const addLike = async (articleId: string) => {
    return await api
        .post(`${LIKE_URL}/api/LikeArticle/add`, { articleId: articleId })
        .then((response) => {
            return response;
        });
};

export const removeLike = async (articleId: string) => {
    return await api
        .post(`${LIKE_URL}/api/LikeArticle/remove`, { articleId: articleId })
        .then((response) => {
            return response;
        });
};
