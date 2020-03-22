using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeleHealthDemo.Models;
using System.Net;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using TeleHealthDemo.Auth;
using TeleHealthDemo.Auth.Interfaces;
using TeleHealthDemo.Data;
using TeleHealthDemo.Data.Interfaces;
using TeleHealthDemo.Validators.Interfaces;


namespace TeleHealthDemo.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly ITeleHealthDataAccessLayer _dal;
        private readonly IAuthentication _auth;
        private readonly IRequestValidator _validator;

        public PatientController(ITeleHealthDataAccessLayer dal, IAuthentication auth, IRequestValidator validator)
        {
            _dal = dal;
            _auth = auth;
            _validator = validator;
        }

        // GET: api/Patient
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = (typeof(Patient)))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery]int pageNo, [FromQuery]int pageSize)
        {
            IEnumerable<Patient> patients;
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // simple auth. This can be properly done through some Authentication service like OAuth
                var isAuthenticated = _auth.IsAuthenticated(Request);

                if(!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }
                
                patients = await _dal.GetAllPatients(pageNo, pageSize);                

                if (patients == null || !patients.Any())
                {
                    return NotFound("Patient Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when retrieving patient data for the requested Page No: {@PageNo} and Page Size: {@PageSize} {@Error}", pageNo, pageSize, ex);
                return StatusCode(500);
            }

            return Ok(patients);
        }

        // POST api/Patient
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = (typeof(Patient)))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var isAuthenticated = _auth.IsAuthenticated(Request);

                if (!isAuthenticated)
                {
                    return Unauthorized("Unauthorized Access!");
                }

                var isValid = _validator.ValidateRequest(patient);

                if (!isValid)
                {
                    return BadRequest("Invalid Data!");
                }

                Patient addedPatient = await _dal.AddPatient(patient);              

                return Ok(addedPatient);
            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error when adding a patient for the Patient Id: {@PatientId} {@Error}", patient.Id, ex);
                return StatusCode(500);
            }
        }      
    }
}
