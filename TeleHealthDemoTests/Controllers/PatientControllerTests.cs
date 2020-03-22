using System;
using Xunit;
using TeleHealthDemo.Auth.Interfaces;
using TeleHealthDemo.Data.Interfaces;
using TeleHealthDemo.Validators.Interfaces;
using TeleHealthDemo.Models;
using System.Collections.Generic;
using TeleHealthDemo.Controllers;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace TeleHealthDemoTests
{
    public class PatientControllerTests
    {
        private readonly ITeleHealthDataAccessLayer _dataAccessLayer;
        private readonly IAuthentication _auth;
        private readonly IRequestValidator _validator;
        private IEnumerable<Patient> _patients;
        private PatientController _patientController;

        public PatientControllerTests()
        {
            _dataAccessLayer = Substitute.For<ITeleHealthDataAccessLayer>();
            _auth = Substitute.For<IAuthentication>();
            _validator = Substitute.For<IRequestValidator>();
            _patients = GetPationsData();
            _patientController = new PatientController(_dataAccessLayer, _auth, _validator);
        }

        [Fact]
        public async Task Get_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            //Arrange
            _patientController.ModelState.AddModelError("Error", "Modal State is invalid");

            //Act
            var result = await _patientController.Get(0, 100);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_ShouldReturnUnauthorized_WhenAuthenticationFailed()
        {
            //Arrange
            _auth.IsAuthenticated(null).Returns(false);

            //Act
            var result = await _patientController.Get(0, 100);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Get_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            //Arrange
            _patientController.Get(0, 100).Throws(new Exception());

            //Act
            var result = await _patientController.Get(0, 100);

            //Assert            
            Assert.Equal(500, ((StatusCodeResult)result).StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturnPatientsSuccessfully()
        {
            //Arrange            
            var apiTask = Task.FromResult(_patients);
            _auth.IsAuthenticated(null).Returns(true);
            _dataAccessLayer.GetAllPatients(0, 100).Returns(apiTask);

            //Act
            var result = await _patientController.Get(0, 100);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            //Arrange         
            _patientController.ModelState.AddModelError("Error", "Modal State is invalid");

            //Act
            var result = await _patientController.Post(new Patient());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnUnauthorized_WhenAuthenticationFailed()
        {
            //Arrange         
            _auth.IsAuthenticated(null).Returns(false);

            //Act
            var result = await _patientController.Post(new Patient());

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            //Arrange     
            var patient = new Patient();
            _patientController.Post(patient).Throws(new Exception());

            //Act
            var result = await _patientController.Post(patient);

            //Assert
            Assert.Equal(500, ((StatusCodeResult)result).StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenInputDataAreInvalid()
        {
            //Arrange     
            var patient = new Patient();
            var apiTask = Task.FromResult(patient);
            _auth.IsAuthenticated(null).Returns(true);
            _validator.ValidateRequest(patient).Returns(false);     

            //Act
            var result = await _patientController.Post(patient);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_ShouldPostPatientDataSuccessfully()
        {
            //Arrange     
            var patient = new Patient();
            var apiTask = Task.FromResult(patient);
            _auth.IsAuthenticated(null).Returns(true);
            _validator.ValidateRequest(patient).Returns(true);
            _dataAccessLayer.AddPatient(patient).Returns(apiTask);

            //Act
            var result = await _patientController.Post(patient);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        private IEnumerable<Patient> GetPationsData()
        {
            var patients = new List<Patient>
            {
                new Patient { Id = 1, FirstName = "dfd", LastName = "dfdfhg", DOB = "11/11/1999", Address = "sdsd sdsvbd", PhoneNumber = "0411111110" },
                new Patient { Id = 2, FirstName = "fgfg", LastName = "hghgh", DOB = "11/11/1999", Address = "scvcdsd sdsd", PhoneNumber = "0411111111" },
                new Patient { Id = 3, FirstName = "bnn", LastName = "nbhgh", DOB = "11/11/1999", Address = "sdsd vbvb", PhoneNumber = "0411111112" },
                new Patient { Id = 4, FirstName = "Shadfdnecv", LastName = "ghhgh", DOB = "11/11/1999", Address = "scvcdsd sdsd", PhoneNumber = "0411111113" },
                new Patient { Id = 5, FirstName = "ngng", LastName = "dfdf", DOB = "11/11/1999", Address = "sdsccvd sdsd", PhoneNumber = "0411111114" },
                new Patient { Id = 6, FirstName = "ughgh", LastName = "cvcvc", DOB = "11/11/1999", Address = "dfdd sdsd", PhoneNumber = "0411111115" },
                new Patient { Id = 7, FirstName = "drhh", LastName = "cvcv", DOB = "11/11/1999", Address = "sdsd nmhj", PhoneNumber = "0411111116" },
                new Patient { Id = 8, FirstName = "dfere", LastName = "bvbvb", DOB = "11/11/1999", Address = "zxz sdsd", PhoneNumber = "0411111117" },
                new Patient { Id = 9, FirstName = "hghgh", LastName = "xcxcx", DOB = "11/11/1999", Address = "sdsd sdsxcdfd", PhoneNumber = "0411111118" },
                new Patient { Id = 10, FirstName = "dfdfdf", LastName = "xcxc", DOB = "11/11/1999", Address = "sdxdsdsdsd sdsd", PhoneNumber = "0411111119" }
            };

            return patients;
        }
    }
}
