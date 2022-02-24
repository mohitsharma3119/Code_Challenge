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
import dateFormat from 'dateformat';
import CurrencyFormat from 'react-currency-format';

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
});


export default function Dealership() {
    const classes = useStyles();

    const [dealerships, setDealerships] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:44380/api/Dealerships/GetDealerships').then((response) => {
            setDealerships(response.data);
            console.log(response);
        });
    }, []);

    return (
        <TableContainer component={Paper} >
            <Table className={classes.table} >
                <TableHead>
                    <TableRow>
                        <TableCell >DealNumber</TableCell>
                        <TableCell align="center">CustomerName</TableCell>
                        <TableCell align="center">DealershipName</TableCell>
                        <TableCell align="center">Vehicle</TableCell>
                        <TableCell align="center">Price</TableCell>
                        <TableCell align="center">Date</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {dealerships.map((row) => (
                        <TableRow key={row.dealNumber}>
                            <TableCell component="th" scope="row">{row.dealNumber}</TableCell>
                            <TableCell align="center">{row.customerName}</TableCell>
                            <TableCell align="center">{row.dealershipName}</TableCell>
                            <TableCell align="center">{row.vehicle}</TableCell>
                            <TableCell align="right">
                                <CurrencyFormat 
                                    value={row.price}
                                     displayType={'text'}
                                     thousandSeparator={true} 
                                     prefix={'CAD$ '} />                                
                            </TableCell>
                            <TableCell align="right"> {dateFormat(row.date, "mmmm dS, yyyy")}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}