using ArtmaisBackend.Core.Mail.Factories;
using ArtmaisBackend.Core.Mail.Requests;
using ArtmaisBackend.Core.Mail.Services;
using ArtmaisBackend.Core.PasswordRecovery.Dtos;
using ArtmaisBackend.Core.PasswordRecovery.Exceptions;
using ArtmaisBackend.Core.PasswordRecovery.Requests;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.PasswordRecovery.Services
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRecoveryRepository _passwordRecoveryRepository;
        private readonly IMailService _mailService;

        public PasswordRecoveryService(IUserRepository userRepository, IPasswordRecoveryRepository passwordRecoveryRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _passwordRecoveryRepository = passwordRecoveryRepository;
            _mailService = mailService;
        }

        public async Task<PasswordRecoveryDto> CreateAsync(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new UserNotFound(PasswordRecoveryDefaults.USER_NOT_FOUND);
            }

            var randomCode = PasswordUtil.GenerateRandomCode();

            var passwordRecovery = await _passwordRecoveryRepository.CreateAsync(user.UserID, randomCode);

            var emailRequest = new EmailRequest
            {
                ToEmail = user.Email,
                Subject = PasswordRecoveryDefaults.VERIFICATION_CODE_SUBJECT,
                Body = BodyFactory.PasswordRecoveryBody($"{randomCode}")
            };

            await _mailService.SendEmailAsync(emailRequest);

            return new PasswordRecoveryDto { UserId = passwordRecovery.UserID };
        }

        public async Task<Entities.PasswordRecovery> ValidateCodeAsync(long userId, int code)
        {
            var passwordRecovery = await _passwordRecoveryRepository.GetByUserIdAndCodeAsync(userId, code);

            if (passwordRecovery == null || passwordRecovery.ExpirationDate <= DateTime.UtcNow)
            {
                throw new InvalidVerificationCode(PasswordRecoveryDefaults.INVALID_VERIFICATION_CODE);
            }

            return passwordRecovery;
        }

        public void UpdatePassword(PasswordRecoveryRequest passwordRecoveryRequest)
        {
            if(passwordRecoveryRequest.NewPassword == "" || passwordRecoveryRequest.NewPassword != passwordRecoveryRequest.NewPasswordConfirmation)
            {
                throw new InvalidPassword(PasswordRecoveryDefaults.INVALID_PASSWORD);
            }

            var user = _userRepository.GetUserById(passwordRecoveryRequest.UserId);

            user.Password = PasswordUtil.Encrypt(passwordRecoveryRequest.NewPassword);

            _userRepository.Update(user);
        }
    }
}
