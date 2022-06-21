import React from 'react';
import ReactDOM from 'react-dom';

import App from './App';
import Root from './Root';

const root = document.getElementById('root');

if (root) {
    ReactDOM.render(
        <Root>
            <App />
        </Root>,
        root
    );
}
