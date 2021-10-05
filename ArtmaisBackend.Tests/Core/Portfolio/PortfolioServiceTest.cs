using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Core.Portfolio.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);
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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);
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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(expectedList);

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);

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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);
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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(firstList);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);
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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetAllPublicationsByUserId(It.IsAny<long>())).Returns(expectedList);

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);

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
            var mockMapper = new Mock<IMapper>();
            mockMediaTypeRepository.Setup(x => x.GetMediaTypeById(It.IsAny<int>())).Returns(mediaType);
            mockMediaRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<MediaType>())).Returns(media);
            mockPuclicationRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<Media>())).Returns(publication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);
            var result = portfolioService.InsertPortfolioContent(portfolioRequest, userId, mediaTypeId);

            result.Should().BeEquivalentTo(expectedPortfolioContentDto);
        }

        [Fact(DisplayName = "InsertPortfolioContent should be returns throw when portfolio request is null")]
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
            var mockMapper = new Mock<IMapper>();
            mockMediaTypeRepository.Setup(x => x.GetMediaTypeById(It.IsAny<int>())).Returns(mediaType);
            mockMediaRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<MediaType>())).Returns(media);
            mockPuclicationRepository.Setup(x => x.Create(It.IsAny<PortfolioRequest>(), It.IsAny<long>(), It.IsAny<Media>())).Returns(publication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);

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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetPublicationByIdAndUserId(It.IsAny<long>(), It.IsAny<int>())).Returns(publication);
            mockMapper.Setup(m => m.Map(portfolioDescriptionRequest, publication)).Returns(updatedPublication);
            #endregion

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);
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
            var mockMapper = new Mock<IMapper>();
            mockPuclicationRepository.Setup(x => x.GetPublicationByIdAndUserId(It.IsAny<long>(), It.IsAny<int>())).Returns(expectedPublication);

            var portfolioService = new PortfolioService(mockMediaRepository.Object, mockMediaTypeRepository.Object, mockPuclicationRepository.Object, mockMapper.Object);

            Action act = () => portfolioService.UpdateDescription(portfolioDescriptionRequest, userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }
    }
}
