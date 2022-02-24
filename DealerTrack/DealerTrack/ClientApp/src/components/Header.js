import React, { Component } from "react";
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { withStyles } from '@material-ui/core/styles';
import { NavLink } from 'react-router-dom';

const styles = theme => ({
    '@global': {
        ul: {
            margin: 0,
            padding: 0,
            listStyle: 'none',
        },
    },
    appBar: {
        borderBottom: `1px solid ${theme.palette.divider}`,
    },
    toolbar: {
        flexWrap: 'wrap',
    },
    toolbarTitle: {
        flexGrow: 1,
    },
    link: {
        margin: theme.spacing(1, 1.5),
    },
});



class Header extends Component {
    render() {
        const { classes } = this.props;
        return (
            <div>
                <AppBar position="static" color="default" elevation={0} className={classes.appBar}>
                    <Toolbar className={classes.toolbar}>
                        <Typography variant="h6" color="inherit" noWrap className={classes.toolbarTitle}>
                            SE Coding Challenge Mohit Sharma
                    </Typography>
                        <nav>
                            <NavLink variant="button" color="textPrimary" to='/' className={classes.link}>
                                Upload CSV
                            </NavLink>
                            <NavLink variant="button" color="textPrimary" to='/data' className={classes.link}>
                                Get Data
                            </NavLink>
                            <NavLink variant="button" color="textPrimary" to='/mostsold' className={classes.link}>
                                Most Sold Vehicle
                            </NavLink>
                        </nav>
                    </Toolbar>
                </AppBar>
            </div>
        )
    }
}

export default withStyles(styles)(Header);