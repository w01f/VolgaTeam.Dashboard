﻿using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;

namespace Asa.Common.Core.Objects.VideoInfo
{
	public class FFMpegData
	{
		public bool IsCorrupted { get; private set; }
		public string Codec { get; }
		public int Bitrate { get; }
		public int Width { get; }
		public int Height { get; }
		public double Duration { get; }

		public bool IsH264Encoded => String.Equals(Codec, "h264", StringComparison.OrdinalIgnoreCase);
		public bool IsBitrateNormal => Bitrate < 3000000;

		public FFMpegData() { }

		public FFMpegData(dynamic source)
		{
			var codecValue = source.streams[0].codec_name;
			if (codecValue != null)
				Codec = codecValue.ToString();

			var bitrateValue = source.format?.bit_rate;
			if (bitrateValue != null)
				Bitrate = Int32.Parse(bitrateValue.ToString());
			else
			{
				bitrateValue = source.streams[0].bit_rate;
				if (bitrateValue != null)
					Bitrate = Int32.Parse(bitrateValue.ToString());
			}

			var widthValue = source.streams[0].width;
			if (widthValue != null)
				Width = Int32.Parse(widthValue.ToString());

			var heightValue = source.streams[0].height;
			if (heightValue != null)
				Height = Int32.Parse(heightValue.ToString());

			var durationValue = source.format?.duration;
			if (durationValue != null)
				Duration = Math.Floor(Double.Parse(durationValue.ToString(), new CultureInfo("en-us")));
			else
			{
				durationValue = source.streams[0].duration;
				if (durationValue != null)
					Duration = Math.Floor(Double.Parse(durationValue.ToString(), new CultureInfo("en-us")));
			}
		}

		public static FFMpegData LoadFromFile(string infoFilePath)
		{
			try
			{
				return new FFMpegData(JsonConvert.DeserializeObject(File.ReadAllText(infoFilePath)));
			}
			catch (Exception)
			{
				return new FFMpegData { IsCorrupted = true };
			}
		}
	}
}
