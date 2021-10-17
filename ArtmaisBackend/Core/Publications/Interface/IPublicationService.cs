﻿using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Request;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Publications.Interface
{
    public interface IPublicationService
    {
        bool InsertComment(CommentRequest? commentRequest, long userId);

        Task<PublicationCommentsDto?> GetAllCommentsByPublicationId(int? publicationId);

        PublicationShareLinkDto? GetPublicationShareLinkByPublicationIdAndUserId(long? userId, int? publicationId);
    }
}