using Chess.DTOs;

namespace Chess.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationResult> RegisterAsync(RegistrationDto registrationDto);
    }

}