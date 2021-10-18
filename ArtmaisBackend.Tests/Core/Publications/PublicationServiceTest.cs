using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.Publications.Service;
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
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockCommentRepository.Setup(x => x.Create(commentRequest, userId));
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
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
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

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
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.InsertComment(commentRequest, It.IsAny<long>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get All Comments By Publication Id should be returns publication comments dto")]
        public async Task GetAllCommentsByPublicationIdShouldBeReturnsPublicationCommentsDto()
        {
            #region Mocks
            var publicationId = 2;
            var commentDto = new List<CommentDto>
            {
               new CommentDto
               {
                   Name = "Name1",
                   Username = "UserName1",
                   Description = "Description1",
                   CommentDate = DateTime.Now
               },
               new CommentDto
               {
                   Name = "Name2",
                   Username = "UserName2",
                   Description = "Description2",
                   CommentDate = DateTime.Now
               },
               new CommentDto
               {
                   Name = "Name3",
                   Username = "UserName3",
                   Description = "Description3",
                   CommentDate = DateTime.Now
               }

            };
            var expectResult = new PublicationCommentsDto
            {
                Comments = commentDto,
                CommentsAmount = commentDto.Count
            };

            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockCommentRepository.Setup(x => x.GetAllCommentsByPublicationId(publicationId)).ReturnsAsync(commentDto);
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = await publicationService.GetAllCommentsByPublicationId(publicationId);

            result.Should().BeEquivalentTo(expectResult);
        }

        [Fact(DisplayName = "Get All Comments By Publication Id should be returns empty object")]
        public async Task GetAllCommentsByPublicationIdShouldBeReturnsEmptyObject()
        {
            #region Mocks
            var publicationId = 2;
            var commentDto = new List<CommentDto> { };
            var expectResult = new PublicationCommentsDto
            {
                Comments = commentDto,
                CommentsAmount = commentDto.Count
            };
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockCommentRepository.Setup(x => x.GetAllCommentsByPublicationId(publicationId)).ReturnsAsync(commentDto);
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = await publicationService.GetAllCommentsByPublicationId(publicationId);

            result.Should().BeEquivalentTo(expectResult);
        }

        [Fact(DisplayName = "Get All Comments By Publication Id should be returns throw when publication id is null")]
        public async Task GetAllCommentsByPublicationIdShouldBeThrowWhenPublicationIdIsNull()
        {
            #region Mocks
            int? publicationId = null;
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockCommentRepository.Setup(x => x.GetAllCommentsByPublicationId(It.IsAny<int>())).Throws<ArgumentNullException>();
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Func<Task> result = async () =>
            {
                await publicationService.GetAllCommentsByPublicationId(publicationId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Publication Share Link By Publication Id And User Id should be returns PublicationShareLinkDto")]
        public void GetPublicationShareLinkByPublicationIdAndUserIdShouldBeReturnsShareLinkDto()
        {
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/artista/"
            }
            );

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = publicationService.GetPublicationShareLinkByPublicationIdAndUserId(3, 12);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/intent/tweet?text=https://artmais-frontend.herokuapp.com/artista/3/publicacao/12%20Olhá%20só%20que%20publicação%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/artista/3/publicacao/12%20Olhá%20só%20que%20publicação%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.Whatsapp.Should().BeEquivalentTo("https://wa.me/?text=https://artmais-frontend.herokuapp.com/artista/3/publicacao/12%20Olhá%20só%20que%20publicação%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
        }

        [Fact(DisplayName = "Get Publication Share Link By Publication Id And User Id should be null when user id is null")]
        public void GetPublicationShareLinkByPublicationIdAndUserIdShouldBeReturnsNullWhenUserIdIsNull()
        {
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockOptions.Setup(x => x.Value).Returns(It.IsAny<SocialMediaConfiguration>());

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.GetPublicationShareLinkByPublicationIdAndUserId(null, It.IsAny<int>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Publication Share Link By Publication Id And User Id should be null when publication id is null")]
        public void GetPublicationShareLinkByPublicationIdAndUserIdShouldBeReturnsNullWhenPublicationIdIsNull()
        {
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockOptions.Setup(x => x.Value).Returns(It.IsAny<SocialMediaConfiguration>());

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.GetPublicationShareLinkByPublicationIdAndUserId(It.IsAny<long>(), null);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Insert Like should be true")]
        public async Task InsertLikeShouldBeReturnsTrue()
        {
            #region Mocks
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockLikeRepository.Setup(x => x.Create(It.IsAny<int>(), It.IsAny<long>()));
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = await publicationService.InsertLike(It.IsAny<int>(), It.IsAny<long>());

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Insert Like should be returns throw when publication id is null")]
        public async Task InsertLikeShouldBeThrowWhenPublicationIdIsNull()
        {
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

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
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockLikeRepository.Setup(x => x.GetLikeByPublicationIdAndUserId(publicationId, userId)).Returns(expectedLike);
            mockLikeRepository.Setup(x => x.Delete(expectedLike));
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = publicationService.DeleteLike(publicationId, userId);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Delete Like should be returns throw when publication id is null")]
        public void DeleteLikeShouldBeThrowWhenPublicationIdIsNull()
        {
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.DeleteLike(null, It.IsAny<long>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Is Liked Publication should be true")]
        public void GetIsLikedPublicationShouldBeTrue()
        {
            #region Mocks
            var publicationId = 122;
            var userId = 12;
            var expectResult = new Like
            {
                LikeID = 1,
                UserID = userId,
                PublicationID = publicationId,
                LikeDate = DateTime.Now
            };
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockLikeRepository.Setup(x => x.GetLikeByPublicationIdAndUserId(publicationId, userId)).Returns(expectResult);
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = publicationService.GetIsLikedPublication(publicationId, userId);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Get Is Liked Publication should be false")]
        public void GetIsLikedPublicationShouldBeFalse()
        {
            #region Mocks
            var publicationId = 122;
            var userId = 12;
            Like expectResult = null;
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockLikeRepository.Setup(x => x.GetLikeByPublicationIdAndUserId(publicationId, userId)).Returns(expectResult);
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = publicationService.GetIsLikedPublication(publicationId, userId);

            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Get Is Liked Publication should be returns throw when publication id is null")]
        public void GetIsLikedPublicationShouldBeThrowWhenPublicationIdIsNull()
        {
            #region Mocks
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockLikeRepository.Setup(x => x.GetLikeByPublicationIdAndUserId(It.IsAny<int>(), It.IsAny<long>())).Throws<ArgumentNullException>();
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Action act = () => publicationService.GetIsLikedPublication(null, It.IsAny<long>());
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Is Liked Publication should be likes amount")]
        public async Task GetAllLikesByPublicationIdShouldBeReturnsLikesAmount()
        {
            #region Mocks
            var publicationId = 122;
            var expectResult = 3;
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockLikeRepository.Setup(x => x.GetAllLikesByPublicationId(publicationId)).ReturnsAsync(expectResult);
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = await publicationService.GetAllLikesByPublicationId(publicationId);

            result.Should().Be(expectResult);
        }

        [Fact(DisplayName = "Get Is Liked Publication should be zero")]
        public async Task GetAllLikesByPublicationIdShouldBeReturnsLikesAmountEqualsZero()
        {
            #region Mocks
            var publicationId = 122;
            var expectResult = 0;
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockLikeRepository.Setup(x => x.GetAllLikesByPublicationId(publicationId)).ReturnsAsync(expectResult);
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);
            var result = await publicationService.GetAllLikesByPublicationId(publicationId);

            result.Should().Be(expectResult);
        }

        [Fact(DisplayName = "Get Is Liked Publication should be returns throw when publication id is null")]
        public async Task GetAllLikesByPublicationIdShouldBeThrowWhenPublicationIdIsNull()
        {
            #region Mocks
            int? publicationId = null;
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            mockLikeRepository.Setup(x => x.GetLikeByPublicationIdAndUserId(It.IsAny<int>(), It.IsAny<long>())).Throws<ArgumentNullException>();
            #endregion

            var publicationService = new PublicationService(mockCommentRepository.Object, mockLikeRepository.Object, mockOptions.Object);

            Func<Task> result = async () =>
            {
                await publicationService.GetAllLikesByPublicationId(publicationId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }
    }
}
