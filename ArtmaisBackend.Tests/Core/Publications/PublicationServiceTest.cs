using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.Publications.Service;
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
    }
}
