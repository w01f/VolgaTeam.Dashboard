using System;
using System.IO;
using Asa.Common.Core.Helpers;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.NonPersistent.ScheduleResources
{
	public abstract class BaseResourceItem
	{
		public Guid Id { get; set; }
		public BaseScheduleResourceContainer Parent { get; set; }

		[JsonIgnore]
		public string ResourceFolderName => Id.ToString();

		[JsonIgnore]
		public string ResourceFolderPath => Path.Combine(Parent.ResourceFolderPath, ResourceFolderName);

		[JsonConstructor]
		private BaseResourceItem() { }

		protected BaseResourceItem(BaseScheduleResourceContainer parent)
		{
			Parent = parent;
			Id = Guid.NewGuid();
		}

		public void Dispose()
		{
			Parent = null;
		}

		public void Release()
		{
			if(Directory.Exists(ResourceFolderPath))
				Utilities.DeleteFolder(ResourceFolderPath);
		}

		public virtual void AfterClone(BaseResourceItem source)
		{
			Parent = source.Parent;
		}
	}
}
