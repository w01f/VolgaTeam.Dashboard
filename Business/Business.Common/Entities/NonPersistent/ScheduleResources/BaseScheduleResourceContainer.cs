using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.NonPersistent.ScheduleResources
{
	public abstract class BaseScheduleResourceContainer : SettingsContainer, IBaseScheduleResourceContainer, IJsonCloneable<BaseScheduleResourceContainer>
	{
		public List<BaseResourceItem> Items { get; }

		[JsonIgnore]
		public abstract string ResourceFolderPath { get; }

		protected BaseScheduleResourceContainer()
		{
			Items = new List<BaseResourceItem>();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			foreach (var resourceItem in Items)
				resourceItem.Parent = this;
		}

		public void AfterClone(BaseScheduleResourceContainer source, Boolean fullClone = true)
		{
			AfterCreate();
			foreach (var resourceItem in Items)
			{
				resourceItem.AfterClone(source.Items.First(sourceProgram => sourceProgram.Id == resourceItem.Id));
				resourceItem.Parent = this;
			}
		}

		public virtual void Dispose()
		{
			Items.ForEach(p => p.Dispose());
			Parent = null;
		}

		public TResource AddResource<TResource>() where TResource : BaseResourceItem
		{
			var resource = (TResource)Activator.CreateInstance(typeof(TResource), this);
			Items.Add(resource);
			Parent.MarkAsModified();
			return resource;
		}

		public void RemoveResource(BaseResourceItem resource)
		{
			resource.Release();
			Items.Remove(resource);
			Parent.MarkAsModified();
		}
	}
}
