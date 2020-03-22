using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeleHealthDemo.Models
{
    /// <summary>
    /// Patient Details
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Gets or sets the the Patient Id
        /// </summary>
        /// <value>
        /// Unique Patient Id
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the the Patient First Name
        /// </summary>
        /// /// <value>
        /// Patient First Name
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the the Patient Last Name
        /// </summary>
        /// /// <value>
        /// Patient Last Name
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the the Patient Date of Birth
        /// </summary>
        /// /// <value>
        /// Patient Date of Birth
        /// </value>
        public string DOB { get; set; }

        /// <summary>
        /// Gets or sets the the Patient Address
        /// </summary>
        /// /// <value>
        /// Patient Address
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the the Patient Phone Number
        /// </summary>
        /// /// <value>
        /// Patient Phone Number
        /// </value>
        public string PhoneNumber { get; set; }
    }
}
