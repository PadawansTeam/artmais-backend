using System.IO;
using System.Text.RegularExpressions;

namespace ArtmaisBackend.Util.File
{
    public static class MediaTypeUtil
    {
        public static MediaTypeEnum GetMediaTypeValue(string fileExtension)
        {
            var imageRegex = new Regex(@"(.jpeg|.jpg|.png)$", RegexOptions.IgnoreCase);
            var videoRegex = new Regex(@"(.MOV|.MPEG-1|.MPEG-2|.MPEG4|.MP4|.MPG|.AVI|.WMV|.MPEGPS|.FLV)$", RegexOptions.IgnoreCase);
            var audioRegex = new Regex(@"(.MP3|.AAC|.WMA|.WAV)$", RegexOptions.IgnoreCase);

            if (imageRegex.IsMatch(fileExtension))
                return MediaTypeEnum.IMAGE;
            }

            if (videoRegex.IsMatch(fileExtension))
                return MediaTypeEnum.VIDEO;
            }

            if (audioRegex.IsMatch(fileExtension))
                return MediaTypeEnum.AUDIO;

            throw new InvalidDataException();
        }
    }
}
