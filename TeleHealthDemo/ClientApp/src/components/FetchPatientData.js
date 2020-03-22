import React, { Component } from 'react';

export class FetchPatientData extends Component {
  static displayName = FetchPatientData.name;

  constructor (props) {
    super(props);
    this.state = { patients: [], loading: true};

      // API keys shouldnt be hardcoded. It should be done via oAuth service
      const requestOptions = {
          method: 'GET',
          headers: {
              'Content-Type': 'application/json',
              'Authorization': 'TXlBdXRob3JpemF0aW9uVG9rZW4='
          }
      };       

      fetch('api/Patient?pageNo=0&pageSize=500', requestOptions)
          .then(async response => { 
              const data = await response.json();

              // check for error response
              if (!response.ok) {
                  // get error message from body or default to response status
                  const error = (data && data.message) || response.status;
                  return Promise.reject(error);
              }

              this.setState({ patients: data, loading: false })
          })
          .catch(error => {
              this.setState({ patients: [], loading: false });
              console.error('There was an error!', error);
          });
    }

    static renderPatientsTable(patients) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Patient Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>DOB</th>
            <th>Address</th>
            <th>Phone Number</th>
          </tr>
        </thead>
        <tbody>
            {patients.map(patient =>
                <tr key={patient.id}>
                    <td>{patient.id}</td>
                    <td>{patient.firstName}</td>
                    <td>{patient.lastName}</td>
                    <td>{patient.dob}</td>
                    <td>{patient.address}</td>
                    <td>{patient.phoneNumber}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchPatientData.renderPatientsTable(this.state.patients);

    return (
      <div>
        <h1>Patient Details</h1>
        {contents}
      </div>
    );
  }
}
 