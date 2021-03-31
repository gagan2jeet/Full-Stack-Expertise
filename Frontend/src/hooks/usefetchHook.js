import { useCallback } from 'react';

function useVehicleSummaryFetch() {
    const requestFetch = useCallback(async (apiURL) => {
        const response = await fetch(apiURL);
        const responseData = await response.json();
        return responseData;
    }, [])

    const get = useCallback((apiURL) => {
        return requestFetch(apiURL);
    }, [requestFetch]);

    // Can define post and put here
    return { get }
}

export default useVehicleSummaryFetch
