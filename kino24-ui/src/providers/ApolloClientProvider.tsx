import React from 'react';
import { ApolloClient, ApolloProvider, HttpLink, InMemoryCache, split } from '@apollo/client';
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';
import { getMainDefinition } from '@apollo/client/utilities';
import AuthStore from 'store/AuthStore';
import { setContext } from '@apollo/client/link/context';

const API_URL = process.env.REACT_APP_KINO24_NEWS_READ_API || 'http://localhost:8080';
const WS_API_URL = process.env.REACT_APP_KINO24_NEWS_READ_WS_API || 'ws://localhost:8080';

const httpLink = new HttpLink({
    uri: `${API_URL}/graphql`,
});

const wsLink = new GraphQLWsLink(
    createClient({
        url: `${WS_API_URL}/graphql`,
    })
);

const splitLink = split(
    ({ query }) => {
        const definition = getMainDefinition(query);
        return definition.kind === 'OperationDefinition' && definition.operation === 'subscription';
    },
    wsLink,
    httpLink
);

const authLink = setContext((_, { headers }) => {
    const token = AuthStore.getToken();
    return {
        headers: {
            ...headers,
            authorization: token ? `Bearer ${token} ` : '',
        },
    };
});

export const client = new ApolloClient({
    link: authLink.concat(splitLink),
    cache: new InMemoryCache(),
});

const ApolloClientProvider: React.FC = ({ children }) => {
    return <ApolloProvider client={client}>{children}</ApolloProvider>;
};

export default ApolloClientProvider;
