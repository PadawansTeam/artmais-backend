﻿using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.Publications.Service;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Publications
{
    public class PublicationServiceTest
    {
        [Fact(DisplayName = "Insert Comment should be returns true")]
        public void InsertCommentShouldBeReturnsTrue()
        {
            #region Mocks
            var userId = 112;
            var commentRequest = new CommentRequest
            {
                PublicationID = 12,
                Description = "commnet"
            };
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockCommentRepository.Setup(x => x.Create(commentRequest, userId));
            #endregion

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = publicationService.InsertComment(commentRequest, userId);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Insert Comment should be returns throw when publication id is null")]
        public void InsertCommentShouldBeThrowWhenPublicationIdIsNull()
        {
            #region Mocks
            var commentRequest = new CommentRequest
            {
                Description = "comment"
            };
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            #endregion

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.InsertComment(commentRequest, It.IsAny<long>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Insert Comment should be returns throw when description is null")]
        public void InsertCommentShouldBeThrowWhenDescriptionIsNull()
        {
            #region Mocks
            var commentRequest = new CommentRequest
            {
                PublicationID = 12
            };
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            #endregion

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.InsertComment(commentRequest, It.IsAny<long>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Insert Like should be true")]
        public async Task InsertLikeShouldBeReturnsTrue()
        {
            #region Mocks
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockLikeRepository.Setup(x => x.Create(It.IsAny<int>(), It.IsAny<long>()));
            #endregion

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = await publicationService.InsertLike(It.IsAny<int>(), It.IsAny<long>());

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Insert Like should be returns throw when publication id is null")]
        public async Task InsertLikeShouldBeThrowWhenPublicationIdIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Func<Task> result = async () =>
            {
                await publicationService.InsertLike(null, It.IsAny<long>());
            };
            await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Delete Like should be true")]
        public void DeleteLikeShouldBeReturnsTrue()
        {
            #region Mocks
            var userId = 10;
            var publicationId = 122;
            var expectedLike = new Like
            {
                LikeID = 1,
                UserID = userId,
                PublicationID = publicationId,
                LikeDate = DateTime.Now
            };
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockLikeRepository.Setup(x => x.GetLikeByPublicationIdAndUserId(publicationId, userId)).Returns(expectedLike);
            mockLikeRepository.Setup(x => x.Delete(expectedLike));
            #endregion

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = publicationService.DeleteLike(publicationId, userId);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Delete Like should be returns throw when publication id is null")]
        public void DeleteLikeShouldBeThrowWhenPublicationIdIsNull()
        {
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.DeleteLike(null, It.IsAny<long>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Publication By Id should be returns PublicationDto by publicationId")]
        public async Task GetPublicationByIdShouldBeReturnsPublicationInfo()
        {
            #region Mocks
            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var userId = 1;
            var publicationId = 123;
            var user = new User
            {
                UserID = 1,
                Username = "Username",
                Name = "Name",
                UserPicture = "UserPicture",
                BackgroundPicture = "BackgrounPicture",
            };
            var portfolio = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    PublicationID = 123,
                    Description = "Description",
                    S3UrlMedia = "S3UrlMedia",
                    PublicationDate = new DateTime(2021, 12, 10)
                },
                new PortfolioContentDto
                {
                    PublicationID = 123
                },
                new PortfolioContentDto
                {
                    PublicationID = 125
                }
            };
            var userCategory = new UserCategoryDto
            {
                Category = "Category",
                Subcategory = "SubCategoy"
            };
            var shareProfile = new ShareProfileBaseDto
            {
                Facebook = "Facebook",
                Twitter = "Twitter",
                Instagram = "Instagram"
            };
            var comment = new List<CommentDto>
            {
                new CommentDto
                {
                Description = "description1"
                },
                new CommentDto
                {
                Description = "description2"
                }
            };
            var expectedResult = new PublicationDto
            {
                BackgroundPicture = "BackgrounPicture",
                Category = "Category",
                CommentsAmount = 2,
                Description = "Description",
                PublicationDate = new DateTime(2021, 12, 10),
                IsLiked = false,
                LikesAmount = 0,
                Name = "Name",
                PublicationFacebook = "https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/artista/1/publicacao/123%20Olhá%20só%20que%20publicação%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.",
                PublicationTwitter = "https://twitter.com/intent/tweet?text=https://artmais-frontend.herokuapp.com/artista/1/publicacao/123%20Olhá%20só%20que%20publicação%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.",
                PublicationWhatsapp = "https://wa.me/?text=https://artmais-frontend.herokuapp.com/artista/1/publicacao/123%20Olhá%20só%20que%20publicação%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.",
                S3UrlMedia = "S3UrlMedia",
                Subcategory = "SubCategoy",
                UserFacebook = "Facebook",
                UserInstagram = "Instagram",
                Username = "Username",
                UserPicture = "UserPicture",
                UserTwitter = "Twitter",
                Comments = comment
            };
            mockUserRepository.Setup(m => m.GetUserById(userId)).Returns(user);
            mockPublicationRepository.Setup(m => m.GetAllPublicationsByUserId(userId)).Returns(portfolio);
            mockUserRepository.Setup(m => m.GetSubcategoryByUserId(userId)).Returns(userCategory);
            mockUserService.Setup(m => m.GetShareProfile(userId)).Returns(shareProfile);
            mockOptions.Setup(m => m.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/artista/"
            });
            mockCommentRepository.Setup(m => m.GetAllCommentsByPublicationId(publicationId)).ReturnsAsync(comment);
            #endregion

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            var result = await publicationService.GetPublicationById(publicationId, userId);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "Get Publication By Id should be throw when publication id or user id is null")]
        public async Task GetPublicationByIdShouldBeThrow()
        {
            int? publicationId = null;
            long userId = 1;

            var mockUserService = new Mock<IUserService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPublicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var publicationService = new PublicationService(mockUserService.Object, mockUserRepository.Object, mockPublicationRepository.Object, mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object); ;

            Func<Task> result = async () =>
            {
                await publicationService.GetPublicationById(publicationId, userId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }
    }
}
