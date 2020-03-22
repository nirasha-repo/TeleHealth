import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
            <h1>Patient Dashboard</h1>     
            <p>This dashboard helps to add and list patient details. Plese navigate through the top right menu to proceed.</p>
      </div>
    );
  }
}
