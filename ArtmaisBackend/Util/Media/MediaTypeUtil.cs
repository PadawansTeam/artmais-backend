using System.IO;
using System.Text.RegularExpressions;

namespace ArtmaisBackend.Util.File
{
    public static class FileExtensionUtil
    {
        public static MediaType GetMediaTypeValue(string fileExtension)
        {
            var imageRegex = new Regex(@"(.jpeg|.jpg|.png)$", RegexOptions.IgnoreCase);
            var videoRegex = new Regex(@"(.MOV|.MPEG-1|.MPEG-2|.MPEG4|.MP4|.MPG|.AVI|.WMV|.MPEGPS|.FLV)$", RegexOptions.IgnoreCase);
            var audioRegex = new Regex(@"(.MP3|.AAC|.WMA|.WAV)$", RegexOptions.IgnoreCase);

            if (imageRegex.IsMatch(fileExtension))
                return MediaType.IMAGE;

            if (videoRegex.IsMatch(fileExtension))
                return MediaType.VIDEO;

            if (audioRegex.IsMatch(fileExtension))
                return MediaType.AUDIO;

            throw new InvalidDataException();
        }
    }
}
