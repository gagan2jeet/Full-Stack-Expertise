import {
    SEARCH_VEHICLESUMMARY_REQUEST,
    SEARCH_VEHICLESUMMARY_SUCCESS,
    SEARCH_VEHICLESUMMARY_SUCCESS_EMPTY,
    SEARCH_VEHICLESUMMARY_FAILURE,
    SEARCH_WITH_MODEL
} from "../util/RequestTypes";

// STATE
const defaultState = {
    loading: true,
    vehicleSummaries: [],
    errorMessage: null,
    noDataMessage: null,
    useSearch: false,
    searchString: null
};

// REDUCER
export default function requestReducer(state = defaultState, action) {

    switch (action.type) {
        case SEARCH_VEHICLESUMMARY_REQUEST:
            return {
                ...state,
                loading: true,
                errorMessage: null,
                noDataMessage: null
            };
        case SEARCH_VEHICLESUMMARY_SUCCESS:
            return {
                ...state,
                loading: false,
                vehicleSummaries: action.payload
            };
        case SEARCH_VEHICLESUMMARY_SUCCESS_EMPTY:
            return {
                ...state,
                loading: false,
                noDataMessage: action.payload
            };
        case SEARCH_VEHICLESUMMARY_FAILURE:
            return {
                ...state,
                loading: false,
                errorMessage: action.error
            };
        case SEARCH_WITH_MODEL:
            return {
                ...state,
                useSearch: action.searchString === '' ? false : true,
                searchString: action.searchString === '' ? null : action.searchString
            };
        default:
            {
                return state;
            }
    }
}