import { useEffect, useRef } from 'react';

const useMounted = () => {
    const isMounted = useRef(false);

    useEffect(() => {
        if (isMounted.current) return;

        isMounted.current = true;
    }, []);

    return isMounted.current;
};

export default useMounted;
