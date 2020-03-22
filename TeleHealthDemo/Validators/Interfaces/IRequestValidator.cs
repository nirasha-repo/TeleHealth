using TeleHealthDemo.Models;

namespace TeleHealthDemo.Validators.Interfaces
{
    public interface IRequestValidator
    {
        bool ValidateRequest(Patient patient);
    }
}
