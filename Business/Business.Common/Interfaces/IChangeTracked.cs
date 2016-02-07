using System;

namespace Asa.Business.Common.Interfaces
{
	public interface IChangeTracked
	{
		DateTime LastModified { get; set; }
		bool IsModified(IChangeTracked latest);
		void MarkAsModified();
	}
}
