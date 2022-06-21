require('dotenv').config();

module.exports = {
    client: {
        service: {
            name: 'articles-service',
            url: process.env.REACT_APP_KINO24_NEWS_READ_API + '/graphql',
        },
        includes: ['src/graphql/**/**/*.ts'],
        tagName: 'gql',
    },
};
