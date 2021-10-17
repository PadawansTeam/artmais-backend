﻿using System;

namespace ArtmaisBackend.Core.Publications.Dto
{
    public class CommentDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}