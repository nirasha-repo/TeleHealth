using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleHealthDemo.Models;

namespace TeleHealthDemo.Data.Interfaces
{
    public interface ITeleHealthDataAccessLayer
    {
        Task<IEnumerable<Patient>> GetAllPatients(int pageNo, int pageSize);
        Task<Patient> AddPatient(Patient patient);
    }
}
