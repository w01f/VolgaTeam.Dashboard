using System;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Json;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.NonPersistent.Common
{
	public abstract class SettingsContainer
	{
		protected IChangeTracked Parent { get; set; }

		public static TSettings CreateInstance<TSettings>(IChangeTracked parent, string encodedSource = "") where TSettings : SettingsContainer
		{
			var createNew = String.IsNullOrEmpty(encodedSource);
			var serializerSettings = new DefaultSerializeSettings();
			var settings = !createNew ?
				JsonConvert.DeserializeObject<TSettings>(encodedSource, serializerSettings) :
				Activator.CreateInstance<TSettings>();
			settings.Parent = parent;
			if (createNew)
				settings.AfterConstruction();
			settings.AfterCreate();
			return settings;
		}

		protected virtual void AfterConstruction() { }

		protected virtual void AfterCreate() { }

		protected void OnSettingsChanged()
		{
			Parent?.MarkAsModified();
		}

		public string Serialize()
		{
			var serializerSettings = new DefaultSerializeSettings();
			return JsonConvert.SerializeObject(this, serializerSettings);
		}
	}
}
