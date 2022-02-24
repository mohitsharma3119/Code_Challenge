import React, { Component } from "react";
import CssBaseline from '@material-ui/core/CssBaseline';
import Container from '@material-ui/core/Container';
import {  BrowserRouter, Route, Switch } from 'react-router-dom';
import Home from './Home';
import MostSoldVehicle from './MostSoldVehicle';
import Dealership from './Dealership';
import { withStyles } from '@material-ui/core/styles';
import Header from './Header';

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
  heroContent: {
    padding: theme.spacing(8, 0, 6),
  },
  cardHeader: {
    backgroundColor:
      theme.palette.type === 'light' ? theme.palette.grey[200] : theme.palette.grey[700],
  },
  cardPricing: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'baseline',
    marginBottom: theme.spacing(2),
  },
});



class App extends Component {
  render() {
    const { classes } = this.props;
    return (
      <React.Fragment>
        <CssBaseline />
        <BrowserRouter>
          <Header />
          <Container maxWidth="lg" component="main" className={classes.heroContent}>
            <Switch>
              <Route path='/' exact component={Home} />
              <Route path='/data' component={Dealership} />
              <Route path='/mostsold' component={MostSoldVehicle} />
            </Switch>
          </Container>
        </BrowserRouter>
      </React.Fragment>
    )
  }
}

export default withStyles(styles)(App);