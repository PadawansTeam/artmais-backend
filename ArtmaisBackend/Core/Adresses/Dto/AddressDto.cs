namespace ArtmaisBackend.Core.Adresses.Dto
{
    public class AddressDto
    {
        public long UserId { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
