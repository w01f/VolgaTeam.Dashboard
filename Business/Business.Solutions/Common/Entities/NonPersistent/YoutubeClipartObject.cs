using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Asa.Business.Solutions.Common.Enums;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public class YouTubeClipartObject : ClipartObject
	{
		public override ClipartObjectType Type => ClipartObjectType.YouTube;

		public string BaseUrl { get; set; }
		public string EmbeddedUrl { get; set; }
		public Image Thumbnail { get; set; }

		public static YouTubeClipartObject FromUrl(string url)
		{
			var regEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
			var match = regEx.Match(url);

			if (!match.Success || match.Groups.Count <= 1)
				throw new FormatException("Wrong YouTube format");

			var youTubeId = match.Groups[1].Value;
			var embeddedUrl = String.Format("https://www.youtube.com/embed/{0}", youTubeId);

			var thumbnailUrl = String.Format("https://img.youtube.com/vi/{0}/0.jpg", youTubeId);
			var tempFile = Path.GetTempFileName();
			Image thumbnailImage;
			using (var client = new WebClient())
			{
				client.DownloadFile(new Uri(thumbnailUrl), tempFile);
				thumbnailImage = Image.FromFile(tempFile);
			}

			return new YouTubeClipartObject
			{
				BaseUrl = url,
				EmbeddedUrl = embeddedUrl,
				Thumbnail = thumbnailImage
			};
		}
	}
}
