import { useCallback } from 'react';
import {
    SEARCH_VEHICLESUMMARY_REQUEST,
    SEARCH_VEHICLESUMMARY_SUCCESS,
    SEARCH_VEHICLESUMMARY_SUCCESS_EMPTY,
    SEARCH_VEHICLESUMMARY_FAILURE,
    SEARCH_WITH_MODEL
} from "../util/RequestTypes";

function useDispatchResponse() {
    const dispatchSuccess = useCallback((dispatch, jsonResponse, searchValue) => {
        if (jsonResponse.succeeded) {
            if (jsonResponse.data.models.length === 0) {
                dispatch({
                    type: SEARCH_VEHICLESUMMARY_SUCCESS_EMPTY,
                    payload: searchValue === null ? 'No data found' : `No model found for ${searchValue}`
                });
            }
            else {
                dispatch({
                    type: SEARCH_VEHICLESUMMARY_SUCCESS,
                    payload: jsonResponse.data.models
                });
            }
        } else {
            dispatch({
                type: SEARCH_VEHICLESUMMARY_FAILURE,
                error: jsonResponse.data.error
            });
        }
    }, []);

    const dispatchError = useCallback((dispatch, error) => {
        dispatch({
            type: SEARCH_VEHICLESUMMARY_FAILURE,
            error: error.message
        });
    }, []);

    const dispatchSearch = useCallback((dispatch) => {
        dispatch({
            type: SEARCH_VEHICLESUMMARY_REQUEST
        });
    }, []);

    const dispatchSearchModel = useCallback((dispatch, vehicleSummaries, searchString) => {
        dispatch({
            type: SEARCH_WITH_MODEL,
            payload: vehicleSummaries,
            searchString: searchString
        });
    }, []);

    return { dispatchSuccess, dispatchError, dispatchSearch, dispatchSearchModel }
}

export default useDispatchResponse;