import React from 'react';
import CircularProgress from '@material-ui/core/CircularProgress';
import { Box } from "@material-ui/core";

export default function Spinner() {
    return (
        <Box display="flex" width={1} mt={5} justifyContent="center">
            <CircularProgress />
        </Box>
    );
}