using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleHealthDemo.Data.Interfaces;
using TeleHealthDemo.Models;

namespace TeleHealthDemo.Data
{
    public class TeleHealthDataAccessLayer : ITeleHealthDataAccessLayer
    {
        TeleHealthDBContext _telehealthDbContext = new TeleHealthDBContext();

        // Returns patients according to pagination details (pageNo is zero based)
        public async Task<IEnumerable<Patient>> GetAllPatients(int pageNo, int pageSize)
        {
            IEnumerable<Patient> patients = await _telehealthDbContext.Patient.Skip(pageNo * pageSize).Take(pageSize).ToListAsync();
            return patients;
        }

        // Adding new patient record
        public async Task<Patient> AddPatient(Patient patient)
        {
            _telehealthDbContext.Patient.Add(patient);
            await _telehealthDbContext.SaveChangesAsync();

            return patient;
        }
    }
}
