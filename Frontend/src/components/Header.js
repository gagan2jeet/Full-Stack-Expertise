import React from "react";
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';

const Header = () => {
    return (
        <div>
            <AppBar position="static">
                <Toolbar variant="dense">
                    <Typography variant="h6" color="inherit">
                        Vehicle List
            </Typography>
                </Toolbar>
            </AppBar>
            <Typography variant="h5" component="h2">
                Lotus
        </Typography>

        </div>
    );
};

export default Header;