import React, { useState } from "react";
import Switch from '@material-ui/core/Switch';

const Search = (props) => {
    const [searchValue, setSearchValue] = useState("");
    const [checkedSwitch, setSwitchValue] = useState(false);

    const handleSearchInputChanges = (e) => {
        setSearchValue(e.target.value);
        if (e.target.value === '') {
            resetInputField();
        }
    }

    const handleSwitchChange = (e) => {
        setSwitchValue(e.target.checked);
    }

    const resetInputField = () => {
        setSearchValue("");
        props.search("", checkedSwitch);
    }

    const callSearchFunction = (e) => {
        e.preventDefault();
        props.search(searchValue, checkedSwitch);
    }

    return (
        <div>
            <label>Toggle below to fetch the data from API</label>
            <form className="search">
                <input
                    value={searchValue}
                    onChange={handleSearchInputChanges}
                    type="text"
                />

                <Switch
                    checked={checkedSwitch}
                    onChange={handleSwitchChange}
                    color="primary"
                    inputProps={{ 'aria-label': 'primary checkbox' }}
                />
                <input onClick={callSearchFunction} type="submit" value="SEARCH" />
            </form>
        </div>
    );
}

export default Search;