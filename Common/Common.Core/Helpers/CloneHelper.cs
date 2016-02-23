using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Json;
using Newtonsoft.Json;

namespace Asa.Common.Core.Helpers
{
	public static class CloneHelper
	{
		public static TClonable Clone<TClonable, TCloneSource>(this TCloneSource original, bool fullClone = true)
			where TClonable : class, IJsonCloneable<TCloneSource>, TCloneSource 
			where TCloneSource : IJsonCloneSource
		{
			if (original == null) return null;
			var originalEncoded = original.Serialize();
			var copy = Deserialize<TClonable, TCloneSource>(original, originalEncoded);
			return copy;
		}

		public static string Serialize<TCloneSource>(this TCloneSource original)
			where TCloneSource : IJsonCloneSource
		{
			var serializerSettings = new DefaultSerializeSettings();
			return JsonConvert.SerializeObject(original, serializerSettings);
		}

		public static TClonable Deserialize<TClonable, TCloneSource>(string encodedContent)
			where TClonable : IJsonCloneable<TCloneSource>, TCloneSource
			where TCloneSource : IJsonCloneSource
		{
			var serializerSettings = new DefaultSerializeSettings();
			var copy = JsonConvert.DeserializeObject<TClonable>(encodedContent, serializerSettings);
			return copy;
		}

		public static TClonable Deserialize<TClonable, TCloneSource>(TCloneSource original, string encodedContent)
			where TClonable : IJsonCloneable<TCloneSource>, TCloneSource
			where TCloneSource : IJsonCloneSource
		{
			var serializerSettings = new DefaultSerializeSettings();
			var copy = JsonConvert.DeserializeObject<TClonable>(encodedContent, serializerSettings);
			copy.AfterClone(original);
			return copy;
		}
	}
}
