import React, { useEffect } from 'react';
import Header from "../src/components/Header";
import VehicleSummary from "../src/components/VehicleSummary";
import Search from "../src/components/Search";
import './App.css';
import Spinner from '../src/components/Spinner/Spinner';
import { useDispatch, useSelector } from 'react-redux';

import useVehicleSummaryFetch from '../src/hooks/usefetchHook';
import useDispatchResponse from '../src/hooks/dispatchHook';

const VEHICLE_SUMMARY_API_URL = 'https://localhost:44387/vehicle-checks/makes/Lotus';
const VEHICLE_SUMMARY_SEARCH_API_URL = 'https://localhost:44387/vehicle-checks/search?make=Lotus&searchString=';

const App = () => {
    const { vehicleSummaries, errorMessage, noDataMessage, loading, useSearch, searchString } = useSelector(state => state);
    const dispatch = useDispatch();
    const { get } = useVehicleSummaryFetch();
    const { dispatchSuccess, dispatchError, dispatchSearch, dispatchSearchModel } = useDispatchResponse();
    useEffect(() => {
        get(VEHICLE_SUMMARY_API_URL)
            .then(jsonResponse => {
                dispatchSuccess(dispatch, jsonResponse, null);
            }).catch(function (error) {
                dispatchError(dispatch, error);
            });
    }, [dispatch]);

    const search = (searchValue, checkedSwitch) => {
        if (checkedSwitch) {
            dispatchSearch(dispatch);

            get(`${VEHICLE_SUMMARY_SEARCH_API_URL}${searchValue}`)
                .then(jsonResponse => {
                    dispatchSuccess(dispatch, jsonResponse, searchValue);
                }).catch(function (error) {
                    dispatchError(dispatch, error);
                });
        }
        else {
            dispatchSearchModel(dispatch, vehicleSummaries, searchValue);
        }
    };


    return (
        <div className="App">
            <Header />
            <Search search={search} />
            <p className="App-intro">Vehicle details below</p>
            <div className="vehiclesummaries">
                {loading && !errorMessage ?
                    (<Spinner />) : errorMessage ?
                        (<div className="errorMessage">{errorMessage}</div>) : noDataMessage ?
                            ((<div className="errorMessage">{noDataMessage}</div>)) : useSearch ?
                                (
                                    vehicleSummaries.filter(vehicle => vehicle.name.indexOf(searchString) !== -1).map((vehicleSummary, index) => (
                                        <VehicleSummary key={`${index}-${vehicleSummary.name}`} vehicleSummary={vehicleSummary} />
                                    ))
                                ):
                                (
                                vehicleSummaries.map((vehicleSummary, index) => (
                                    <VehicleSummary key={`${index}-${vehicleSummary.name}`} vehicleSummary={vehicleSummary} />
                                ))
                            )}
            </div>
        </div>
    );
};

export default App;
