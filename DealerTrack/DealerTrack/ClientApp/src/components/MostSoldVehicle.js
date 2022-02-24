import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import axios from 'axios';

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
});


export default function MostSoldVehicle() {
    const classes = useStyles();

    const [vehicles, setvehicles] = useState('');
    const [count, setCount] = useState('');

    useEffect(() => {
        axios.get('https://localhost:44380/api/Dealerships/GetMostSoldVehicle').then((response) => {
            console.log(response.data);
            setvehicles(response.data.vehicleName);
            setCount(response.data.soldCount);
        });
    }, []);

    return (
        <TableContainer component={Paper} >
            <Table className={classes.table} >
                <TableHead>
                    <TableRow>
                        <TableCell align="left">Vehicle</TableCell>
                        <TableCell align="right">No of Times Sold</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    <TableRow key={vehicles}>
                        <TableCell component="th" scope="row">{vehicles}</TableCell>
                        <TableCell align="right">{count}</TableCell>
                    </TableRow>
                </TableBody>
            </Table>
        </TableContainer>
    );
}