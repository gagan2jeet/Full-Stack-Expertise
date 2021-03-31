import React from "react";

const VehicleSummary = ({ vehicleSummary }) => {
    return (
        <div>
            <div className="vehicleSummary">
                <h2>{vehicleSummary.name}</h2>
                <div>
                    <img
                        width="200"
                        src={process.env.PUBLIC_URL + '/VehicleIcon.svg'}
                        alt={vehicleSummary.name}
                    />
                </div>
                <p>Years available: {vehicleSummary.yearsAvailable}</p>
            </div>
        </div>
    );
};


export default VehicleSummary;