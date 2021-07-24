﻿namespace ArtmaisBackend.Core.Adresses.Request
{
    public class AddressRequest
    {
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? ZipCode { get; set; }
    }
}