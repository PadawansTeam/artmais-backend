﻿using ArtmaisBackend.Core.OAuth.Google.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Services.Interface;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.OAuth.Google.Mediator
{
    public class GoogleMediator : IGoogleMediator
    {
        public GoogleMediator(ICategorySubcategoryRepository categorySubcategoryRepository, IGoogleService googleService, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _categorySubcategoryRepository = categorySubcategoryRepository;
            _googleService = googleService;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IGoogleService _googleService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public async Task<string?> SignIn(string token)
        {
            var googleUser = await _googleService.ValidateToken(token);
            var user = _userRepository.GetUserById(long.Parse(googleUser.Subject));

            if (user == null)
                throw new UserNotFound("Usuário não encontrado");

            return _jwtTokenService.GenerateToken(user);
        }

        public string SignUp(OAuthSignUpRequest request)
        {

            var existentUsername = this._userRepository.GetUserByUsername(request.Username);

            if (existentUsername != null)
                throw new UsernameAlreadyInUse("Username já utilizado.");

            var existentSubcategory = this._categorySubcategoryRepository
                .GetSubcategoryBySubcategory(request.Subcategory);

            if (existentSubcategory == null)
                existentSubcategory = this._categorySubcategoryRepository.Create(request.Category, request.Subcategory);

            request.SubcategoryID = existentSubcategory.SubcategoryID;

            var user = _userRepository.CreateOAuthUser(request, "Google");
            
            return _jwtTokenService.GenerateToken(user);
        }
    }
}