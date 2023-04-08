using Chess.Models;
using Chess.Repositories;
using Chess.DTOs;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace Chess.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<RegistrationDto> _validator;
        public RegistrationService(IUserRepository userRepository, IMapper mapper, IValidator<RegistrationDto> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<RegistrationResult> RegisterAsync(RegistrationDto registrationDto)
        {
            ValidationResult result = await _validator.ValidateAsync(registrationDto);
            if (!result.IsValid)
            {
                return new RegistrationResult(false, result.Errors.First().ToString());
            }
            if (await _userRepository.GetUserByUsernameAsync(registrationDto.Username) != null)
            {
                return new RegistrationResult(false, "Username already exists");
            }
            if (await _userRepository.GetUserByEmailAsync(registrationDto.Email) != null)
            {
                return new RegistrationResult(false, "Email address already exists");
            }
            User newUser = _mapper.Map<User>(registrationDto);
            await _userRepository.CreateUserAsync(newUser);
            return new RegistrationResult(true, "Registration successful");
        }


    }

    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public RegistrationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}