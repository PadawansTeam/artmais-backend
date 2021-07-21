using ArtmaisBackend.Util;
using Xunit;

namespace ArtmaisBackend.Tests.Util
{
    public class PasswordUtilTest
    {
        [Fact(DisplayName = "Shoud be validated hashed password using the encryption method")]
        public void EncryptReturnsHash()
        {
            var result = PasswordUtil.Encrypt("123456789", "05ZqadUMOvuD8CAL+jffYg==");

            Assert.Equal("05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==", result);
        }
    }
}
