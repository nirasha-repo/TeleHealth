import React, { Component } from 'react';

export class AddPatientData extends Component {  

    constructor(props) {
        super(props);

        this.state = {
            patient: {},
            loading: false
        };
    }

    // validate each field while typing
    static validateInput(event) {        
        var inputValue = event.target.value; 
        var isEmpty = inputValue == '';
        var isValid = isEmpty || /^[a-zA-Z ]+$/.test(inputValue);    
                
        if (!isValid) {   
            event.target.className = "invalid-field";            
        }
        else {
            event.target.className = "valid-field";           
        }
    }

    // This will handle the submit form event. 
    static async handleSave(event) {        
        event.preventDefault();  

        var firstName = event.target.firstName.value;
        var lastName = event.target.lastName.value;
        var dob = event.target.dob.value;
        var address = event.target.address.value;
        var phoneNumber = event.target.phoneNumber.value;

        var isEmpty = firstName == '';
        var isValid = isEmpty || /^[a-zA-Z ]+$/.test(firstName);  

        if (isValid) {
            var patient = {
                firstName: firstName,
                lastName: lastName,
                dob: dob,
                address: address,
                phoneNumber: phoneNumber
            };

            // Simple POST request with a JSON body using fetch
            // API keys shouldnt be hardcoded. It should be done via oAuth service
            const requestOptions = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'TXlBdXRob3JpemF0aW9uVG9rZW4='
                },
                body: JSON.stringify(patient)
            };

            const response = await fetch('api/Patient', requestOptions);
            const isUserAdded = response.ok;

            if (isUserAdded) {
                var inputFields = document.querySelectorAll('input');
                var isDataFilled = true;
                inputFields.forEach(input => {
                    if (isDataFilled && input.value == '') {
                        isDataFilled = false;
                    }
                });

                if (isDataFilled) {
                    inputFields.forEach(input => {
                        input.value = '';
                    });
                }

                alert("Patient details added successfully!");
            }
            else {
                alert("Internal error! Patient details are not added.");
            }
        }
        else {
            console.error('Invalid input field data!');
        }
    }
     
    // Returns the HTML Form to the render() method.  
    static renderAddPatientForm() {         
        return (
            <div>
                <form onSubmit={this.handleSave}>
                    <table className='table table-striped'>
                        <thead>
                            <tr>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>DOB</th>
                                <th>Address</th>
                                <th>Phone Number</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr >
                                <td><input onChange={this.validateInput} autoComplete="off" name="firstName" required></input></td>
                                <td><input autoComplete="off" name="lastName" required></input></td>
                                <td><input autoComplete="off" name="dob" required></input></td>
                                <td><input autoComplete="off" name="address" required></input></td>
                                <td><input autoComplete="off" name="phoneNumber" required></input></td>
                            </tr>
                            <tr>
                                <td colSpan="5">
                                    <button type="submit" className="button">Add</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </form >
            </div>                        
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : AddPatientData.renderAddPatientForm();

        return (
            <div>
                <h1>Add Patient Details</h1>
                {contents}
            </div>
        );
    }
}
