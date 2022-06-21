import React from 'react';
import { render } from '@testing-library/react';

import App from '../App';
import Root from '../Root';

describe('App', () => {
    it('renders app and root components', () => {
        render(
            <Root>
                <App />
            </Root>
        );
        // :D
        expect(true).toBe(true);
    });
});
