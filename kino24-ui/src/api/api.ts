import axios from 'axios';
import AuthStore from 'store/AuthStore';
import { createBrowserHistory } from 'history';

const CancelToken = axios.CancelToken;
const source = CancelToken.source();

export const history = createBrowserHistory();

interface HttpResponse {
    headers: any;
    data: any;
}

axios.interceptors.request.use(
    (config: any) => {
        const token = AuthStore.getToken() as string;
        if (token) {
            config.headers['Authorization'] = 'Bearer ' + token;
        }
        config.headers['Content-Type'] = 'application/json';
        return config;
    },
    (error) => {
        Promise.reject(error);
    }
);

axios.interceptors.response.use(
    (res) => res,
    (err) => {
        if (err.response?.status === 401) {
            source.cancel();
            AuthStore.removeToken();
            const str = window.location.pathname;
            if (str !== '/signIn') {
                localStorage.setItem('pathName', str);
            }
            history.push('/signIn');
            window.location.reload();
        }
        return Promise.reject(err);
    }
);

const get = async (url: string, data?: any, paramsSerializer?: any): Promise<HttpResponse> => {
    const response = await axios.get(url, {
        params: data,
        paramsSerializer: paramsSerializer,
    });
    return response;
};

const post = async (url: string, data?: any) => {
    const response = await axios.post(url, data, {
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
    });
    return response;
};

export default { get, post };
