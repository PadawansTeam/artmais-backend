﻿using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Interface;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Publications.Service
{
    public class PublicationService : IPublicationService
    {
        public PublicationService(ICommentRepository commentRepository, IOptions<SocialMediaConfiguration> options)
        {
            _commentRepository = commentRepository;
            _socialMediaConfiguration = options.Value;
        }

        private readonly ICommentRepository _commentRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;

        public bool InsertComment(CommentRequest? commentRequest, long userId)
        {
            if (commentRequest.PublicationID is null || commentRequest.Description is null)
                throw new ArgumentNullException();

            _commentRepository.Create(commentRequest, userId);

            return true;
        }

        public async Task<PublicationCommentsDto?> GetAllCommentsByPublicationId(int? publicationId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            var comments = await _commentRepository.GetAllCommentsByPublicationId(publicationId);
            var commentsAmount = comments.Count();

            var publicationCommentsDto = new PublicationCommentsDto(comments, commentsAmount);

            return publicationCommentsDto;
        }

        public PublicationShareLinkDto? GetPublicationShareLinkByPublicationIdAndUserId(long? userId, int? publicationId)
        {
            if (publicationId is null || userId is null)
                throw new ArgumentNullException();

            var publicationShareLinkDto = new PublicationShareLinkDto
            {
                Facebook = $"{_socialMediaConfiguration.Facebook}{_socialMediaConfiguration.ArtMais}{userId}/publicacao/{publicationId}{ShareLinkMessages.MessageShareComment}",
                Twitter = $"{_socialMediaConfiguration.Twitter}{_socialMediaConfiguration.ArtMais}{userId}/publicacao/{publicationId}{ShareLinkMessages.MessageShareComment}",
                Whatsapp = $"{_socialMediaConfiguration.Whatsapp}?text={_socialMediaConfiguration.ArtMais}{userId}/publicacao/{publicationId}{ShareLinkMessages.MessageShareComment}"
            };
            return publicationShareLinkDto;
        }
    }
}