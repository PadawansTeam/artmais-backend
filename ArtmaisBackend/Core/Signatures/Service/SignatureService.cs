using ArtmaisBackend.Core.Signatures.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Signatures.Service
{
    public class SignatureService : ISignatureService
    {
        public SignatureService(ISignatureRepository signatureRepository)
        {
            _signatureRepository = signatureRepository;
        }

        private readonly ISignatureRepository _signatureRepository;

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
    }
}
