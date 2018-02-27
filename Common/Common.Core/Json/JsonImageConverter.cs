using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace Asa.Common.Core.Json
{
	public class JsonImageConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Image) || objectType.IsSubclassOf(typeof(Image));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return !String.IsNullOrEmpty(reader.Value as String) ?
				Image.FromStream(new MemoryStream(Convert.FromBase64String((String)reader.Value))) : null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!(value is Bitmap bmp)) return;
			var converter = new ImageConverter();
			writer.WriteValue(Convert.ToBase64String((byte[]) converter.ConvertTo(bmp, typeof(byte[]))));
		}
	}
}
