import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchPatientData } from './components/FetchPatientData';
import { AddPatientData } from './components/AddPatientData';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />  
        <Route path='/add-data' component={AddPatientData} />   
        <Route path='/fetch-data' component={FetchPatientData} />
      </Layout>
    );
  }
}
