using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Asa.Common.Core.Json
{
	public class DefaultSerializeSettings : JsonSerializerSettings
	{
		public DefaultSerializeSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			PreserveReferencesHandling = PreserveReferencesHandling.Objects;
			TypeNameHandling = TypeNameHandling.All;
			NullValueHandling = NullValueHandling.Ignore;
			MissingMemberHandling = MissingMemberHandling.Ignore;
			ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
			Formatting = Formatting.None;
			//Formatting = Formatting.Indented;
			Converters.Add(new JsonImageConverter());
			ContractResolver = new ContentResolver();
			Error += OnError;
		}

		private void OnError(object sender, ErrorEventArgs e)
		{
			e.ErrorContext.Handled = true;
		}
	}
}
