using ArtmaisBackend.Core.Signatures.Dto;
using ArtmaisBackend.Core.Signatures.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Signatures.Service
{
    public class SignatureService : ISignatureService
    {
        public SignatureService(ISignatureRepository signatureRepository, IUserRepository userRepository)
        {
            _signatureRepository = signatureRepository;
            _userRepository = userRepository;
        }

        private readonly ISignatureRepository _signatureRepository;
        private readonly IUserRepository _userRepository;

        public async Task CreateSignature(long userId)
        {
            await _signatureRepository.Create(userId);
        }

        public async Task UpdateSignature(long userId)
        {
            var signature = await _signatureRepository.GetSignatureByUserId(userId);

            if (signature is null)
                throw new ArgumentNullException();

            var date = DateTime.UtcNow;
            signature.StartDate = date;
            signature.EndDate = date.AddYears(1);

            await _signatureRepository.Update(signature);
        }

        public async Task<bool> GetSignatureByUserId(long userId)
        {
            var signature = await _signatureRepository.GetSignatureByUserId(userId);

            if (signature is null)
                return false;

            else if (signature.EndDate >= DateTime.UtcNow)
                return true;

            else
                return false;
        }


        public async Task<SignatureDto> GetSignatureUserDto(long userId)
        {
            var signature = await _signatureRepository.GetSignatureByUserId(userId);
            var isPremium = true;
            if (signature is null || signature.EndDate < DateTime.UtcNow)
                isPremium = false;

            var userInfo = _userRepository.GetUserById(userId);
            if (userInfo is null)
                throw new ArgumentNullException();

            var signatureDto = new SignatureDto
            {
                UserId = userId,
                Name = userInfo.Name,
                EndDate = signature?.EndDate ?? DateTime.MinValue,
                IsPremium = isPremium
            };

            return signatureDto;
        }
    }
}
