using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Core.Portfolio.Service;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Portfolio
{
    public class PortfolioServiceTest
    {
        [Fact(DisplayName = "Get Logged User Portfolio Content By User Id should be returns list of PortfolioContentListDto")]
        public void GetLoggedUserPortfolioByIdShouldBeReturnsPortfolioContentDto()
        {
            #region Mocks
            var userId = 113;
            var firstList = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                }
            };
            var image = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                }
            };
            var video = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                }
            };
            var audio = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                }
            };
            var expectedList = new PortfolioContentListDto
            {
                Image = image,
                Audio = audio,
                Video = video
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.GetLoggedUserPortfolioById(userId);

            result.Should().BeEquivalentTo(expectedList);
        }

        [Fact(DisplayName = "Get Logged User Portfolio Content By User Id should be returns Empty List")]
        public void GetLoggedUserPortfolioByIdShouldBeReturnsEmptyList()
        {
            #region Mocks
            var userId = 113;
            var firstList = new List<PortfolioContentDto> { };
            var image = new List<PortfolioContentDto> { };
            var video = new List<PortfolioContentDto> { };
            var audio = new List<PortfolioContentDto> { };
            var expectedList = new PortfolioContentListDto
            {
                Image = image,
                Audio = audio,
                Video = video
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.GetLoggedUserPortfolioById(userId);

            result.Should().BeEquivalentTo(expectedList);
        }

        [Fact(DisplayName = "Get Logged User Portfolio Content By User Id should be returns throw when user id is null")]
        public void GetLoggedUserPortfolioByIdShouldBeThrowWhenUserIdIsNull()
        {
            long? userId = null;

            var expectedList = new List<PortfolioContentDto> { };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(expectedList);

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.GetLoggedUserPortfolioById(userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Portfolio Content By User Id should be returns list of PortfolioContentListDto")]
        public void GetPortfolioByUserIdShouldBeReturnsPortfolioContentDto()
        {
            #region Mocks
            var userId = 113;
            var firstList = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                }
            };
            var image = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 1,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                }
            };
            var video = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                }
            };
            var audio = new List<PortfolioContentDto>
            {
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 2,
                    PublicationID = 2,
                    MediaID = 2,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia2",
                    Description = "Description2",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 3,
                    PublicationID = 3,
                    MediaID = 3,
                    MediaTypeID = 3,
                    S3UrlMedia = "S3UrlMedia3",
                    Description = "Description3",
                    PublicationDate = new DateTime()
                }
            };
            var expectedList = new PortfolioContentListDto
            {
                Image = image,
                Audio = audio,
                Video = video
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.GetPortfolioByUserId(userId);

            result.Should().BeEquivalentTo(expectedList);
        }

        [Fact(DisplayName = "Get Portfolio Content By User Id should be returns Empty List")]
        public void GetPortfolioByUserIdShouldBeReturnsEmptyList()
        {
            #region Mocks
            var userId = 113;
            var firstList = new List<PortfolioContentDto> { };
            var image = new List<PortfolioContentDto> { };
            var video = new List<PortfolioContentDto> { };
            var audio = new List<PortfolioContentDto> { };
            var expectedList = new PortfolioContentListDto
            {
                Image = image,
                Audio = audio,
                Video = video
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.GetPortfolioByUserId(userId);

            result.Should().BeEquivalentTo(expectedList);
        }

        [Fact(DisplayName = "Get Portfolio Content By User Id should be returns throw when user id is null")]
        public void GetPortfolioByUserIdShouldBeThrowWhenUserIdIsNull()
        {
            long? userId = null;
            var expectedList = new List<PortfolioContentDto> { };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(expectedList);

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.GetPortfolioByUserId(userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Insert Portfolio Content should be returns PortfolioContentDto")]
        public void InsertPortfolioContentShouldBeReturnsPortfolioContentDto()
        {
            #region Mocks
            long userId = 1;
            int mediaTypeId = 1;
            var mediaType = new MediaType
            {
                MediaTypeId = 1,
                Description = "Description"
            };
            var media = new Media
            {
                MediaID = 1,
                MediaTypeID = 1,
                MediaType = mediaType,
                UserID = 1,
                User = new User { },
                S3UrlMedia = "S3UrlMedia"
            };
            var portfolioRequest = new PortfolioRequest
            {
                PortfolioImageUrl = "PortfolioImageUrl",
                Description = "Description"
            };
            var publication = new Publication
            {
                PublicationID = 1,
                MediaID = 1,
                Media = media,
                UserID = 1,
                User = new User { },
                Description = "Description",
                PublicationDate = new DateTime(2021, 8, 15)
            };
            var expectedPortfolioContentDto = new PortfolioContentDto
            {
                UserID = 1,
                PublicationID = 1,
                MediaID = 1,
                MediaTypeID = 1,
                S3UrlMedia = "S3UrlMedia",
                Description = "Description",
                PublicationDate = new DateTime(2021, 8, 15)
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockMediaTypeRepository.Setup(x => x.GetMediaTypeById(It.IsAny<int>())).Returns(mediaType);
            mockMediaRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<MediaType>())).Returns(media);
            mockPuclicationRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<Media>())).Returns(publication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.InsertPortfolioContent(portfolioRequest, userId, mediaTypeId);

            result.Should().BeEquivalentTo(expectedPortfolioContentDto);
        }

        [Fact(DisplayName = "Insert Portfolio Content should be returns PortfolioContentDto with description null")]
        public void InsertPortfolioContentShouldBeReturnsPortfolioContentDtoWithNullDescription()
        {
            #region Mocks
            long userId = 1;
            int mediaTypeId = 1;
            var mediaType = new MediaType
            {
                MediaTypeId = 1,
                Description = "Description"
            };
            var media = new Media
            {
                MediaID = 1,
                MediaTypeID = 1,
                MediaType = mediaType,
                UserID = 1,
                User = new User { },
                S3UrlMedia = "S3UrlMedia"
            };
            var portfolioRequest = new PortfolioRequest
            {
                PortfolioImageUrl = "PortfolioImageUrl",
            };
            var publication = new Publication
            {
                PublicationID = 1,
                MediaID = 1,
                Media = media,
                UserID = 1,
                User = new User { },
                Description = "",
                PublicationDate = new DateTime(2021, 8, 15)
            };
            var expectedPortfolioContentDto = new PortfolioContentDto
            {
                UserID = 1,
                PublicationID = 1,
                MediaID = 1,
                MediaTypeID = 1,
                S3UrlMedia = "S3UrlMedia",
                Description = "",
                PublicationDate = new DateTime(2021, 8, 15)
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockMediaTypeRepository.Setup(x => x.GetMediaTypeById(It.IsAny<int>())).Returns(mediaType);
            mockMediaRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<MediaType>())).Returns(media);
            mockPuclicationRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<Media>())).Returns(publication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.InsertPortfolioContent(portfolioRequest, userId, mediaTypeId);

            result.Should().BeEquivalentTo(expectedPortfolioContentDto);
            result.Description.Should().BeEmpty();
        }

        [Fact(DisplayName = "Insert Portfolio Content should be returns throw when portfolio request is null")]
        public void InsertPortfolioContentShouldBeThrowWhenUserIdIsNull()
        {
            #region Mocks
            var portfolioRequest = new PortfolioRequest { };
            long userId = 1;
            int mediaTypeId = 1;
            var mediaType = new MediaType { };
            var media = new Media { };
            var publication = new Publication { };
            var portfolioContentDto = new PortfolioContentDto { };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockMediaTypeRepository.Setup(x => x.GetMediaTypeById(It.IsAny<int>())).Returns(mediaType);
            mockMediaRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<MediaType>())).Returns(media);
            mockPuclicationRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<Media>())).Returns(publication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.InsertPortfolioContent(portfolioRequest, userId, mediaTypeId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Update Description should be returns True")]
        public void UpdateDescriptionShouldBeReturnsPortfolioContentDto()
        {
            #region Mocks
            var userId = 1;
            var portfolioDescriptionRequest = new PortfolioDescriptionRequest
            {
                PublicationId = 1,
                PublicationDescription = "PublicationDescription"
            };
            var publication = new Publication
            {
                PublicationID = 1,
                MediaID = 1,
                Media = new Media { },
                UserID = 1,
                User = new User { },
                Description = "Description",
                PublicationDate = new DateTime()
            };
            var updatedPublication = new Publication
            {
                PublicationID = 1,
                UserID = 1,
                Description = "PublicationDescription"
            };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetPublicationByIdAndUserId(It.IsAny<long>(), It.IsAny<int>())).Returns(publication);
            mockMapper.Setup(m => m.Map(portfolioDescriptionRequest, publication)).Returns(updatedPublication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.UpdateDescription(portfolioDescriptionRequest, userId);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Update Description should be returns throw when portfolio request is null")]
        public void UpdateDescriptionShouldBeThrowWhenUserIdIsNull()
        {
            var portfolioDescriptionRequest = new PortfolioDescriptionRequest { };
            var userId = 1;

            var expectedPublication = new Publication { };

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetPublicationByIdAndUserId(It.IsAny<long>(), It.IsAny<int>())).Returns(expectedPublication);

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.UpdateDescription(portfolioDescriptionRequest, userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Publication By Id should be returns portfolio content dto")]
        public void GetPublicationByIdShouldBeReturnsPortfolioContentDto()
        {
            #region Mocks
            var userId = 113;
            var publicationId = 2;
            var portfolioContent = new List<PortfolioContentDto>
            {
               new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 2,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 3,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 4,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
            };
            var expectedPublication = new PortfolioContentDto
            {
                UserID = 1,
                PublicationID = 2,
                MediaID = 1,
                MediaTypeID = 2,
                S3UrlMedia = "S3UrlMedia1",
                Description = "Description1",
                PublicationDate = new DateTime()
            };
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(userId)).Returns(portfolioContent);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.GetPublicationById(publicationId, userId);

            result.Should().BeEquivalentTo(expectedPublication);
        }

        [Fact(DisplayName = "Get Publication By Id should be returns throw when portfolio id is null")]
        public void GetPublicationByIdShouldBeThrowWhenUserIdIsNull()
        {
            #region Mocks
            var userId = 113;
            int? publicationId = null;
            var portfolioContent = new List<PortfolioContentDto>
            {
               new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 1,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 2,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 3,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
                new PortfolioContentDto
                {
                    UserID = 1,
                    PublicationID = 4,
                    MediaID = 1,
                    MediaTypeID = 2,
                    S3UrlMedia = "S3UrlMedia1",
                    Description = "Description1",
                    PublicationDate = new DateTime()
                },
            };
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<int>())).Returns(portfolioContent);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.GetPublicationById(publicationId, userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Publication By Id should be returns throw when portfolio content is null")]
        public void GetPublicationByIdShouldBeThrowWhenPortfolioContentIsNull()
        {
            #region Mocks
            var userId = 113;
            int? publicationId = 2;
            var expectedList = new List<PortfolioContentDto> { };
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<int>())).Throws<ArgumentNullException>();
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.GetPublicationById(publicationId, userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Insert Comment should be returns PortfolioContentDto")]
        public void InsertCommentShouldBeReturnsTrue()
        {
            #region Mocks
            var userId = 112;
            var commentRequest = new CommentRequest
            {
                PublicationID = 12,
                Description = "commnet"
            };
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();

            mockCommentRepository.Setup(x => x.Create(commentRequest, userId));
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = portfolioService.InsertComment(commentRequest, userId);

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
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.InsertComment(commentRequest, It.IsAny<long>());
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
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Action act = () => portfolioService.InsertComment(commentRequest, It.IsAny<long>());
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

            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockCommentRepository.Setup(x => x.GetAllCommentsByPublicationId(publicationId)).ReturnsAsync(commentDto);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = await portfolioService.GetAllCommentsByPublicationId(publicationId);

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
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockCommentRepository.Setup(x => x.GetAllCommentsByPublicationId(publicationId)).ReturnsAsync(commentDto);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);
            var result = await portfolioService.GetAllCommentsByPublicationId(publicationId);

            result.Should().BeEquivalentTo(expectResult);
        }

        [Fact(DisplayName = "Get All Comments By Publication Id should be returns throw when publication id is null")]
        public async Task GetAllCommentsByPublicationIdShouldBeThrowWhenPublicationIdIsNull()
        {
            #region Mocks
            int? publicationId = null;
            var mockMediaRepository = new Mock<IMediaRepository>();
            var mockMediaTypeRepository = new Mock<IMediaTypeRepository>();
            var mockPuclicationRepository = new Mock<IPublicationRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockMapper = new Mock<IMapper>();
            mockCommentRepository.Setup(x => x.GetAllCommentsByPublicationId(It.IsAny<int>())).Throws<ArgumentNullException>();
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockCommentRepository.Object, mockOptions.Object, mockMapper.Object);

            Func<Task> result = async () =>
            {
                await portfolioService.GetAllCommentsByPublicationId(publicationId);
            };
            await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }
    }
}
