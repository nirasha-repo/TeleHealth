using TeleHealthDemo.Validators.Interfaces;
using TeleHealthDemo.Models;
using System.Text.RegularExpressions;

namespace TeleHealthDemo.Validators
{
    public class RequestValidator : IRequestValidator
    {
        public bool ValidateRequest(Patient patient)
        {
            var isValid = true;

            // first name validation
            var firstName = patient.FirstName;
            if(string.IsNullOrEmpty(firstName) || !Regex.Match(firstName, "^[a-zA-Z ]+$").Success)
            {
                isValid = false;
            }

            // rest of the fields to be validated

            return isValid;
        }
    }
}
