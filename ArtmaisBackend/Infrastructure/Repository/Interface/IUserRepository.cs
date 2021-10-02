﻿using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.OAuth.Google;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Core.Users.Dto;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        User Create(SignUpRequest signUpRequest, int userTypeId = 1);
        User CreateOAuthUser(OAuthSignUpRequest signUpRequest, string provider, int userTypeId = 1);
        User GetUserByEmail(string email);
        IEnumerable<RecomendationDto> GetUsersByInterest(long userId);
        User GetUserByUsername(string username);
        User GetUserById(long? id);
        User Update(User user);
        UserCategoryDto GetSubcategoryByUserId(long userId);
        IEnumerable<RecomendationDto> GetUsers();
    }
}
