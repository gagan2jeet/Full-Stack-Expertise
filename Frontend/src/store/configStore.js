import { createStore, applyMiddleware } from 'redux';
import { composeWithDevTools } from 'redux-devtools-extension'
import requestReducer from '../reducers/reducers';
const composedEnhancer = composeWithDevTools(
    // Add whatever middleware you actually want to use here
    applyMiddleware()
)

const basicReduxStore = createStore(requestReducer, composedEnhancer);
export default basicReduxStore;