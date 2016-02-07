using System.Collections.Generic;
using Asa.Business.Online.Common;
using Asa.Business.Online.Entities.NonPersistent;

namespace Asa.Business.Online.Interfaces
{
	public interface IDigitalProductsContent
	{
		List<DigitalProduct> DigitalProducts { get; }
		DigitalProductSummary DigitalProductSummary { get; }
		void AddDigital(string categoryName);
		void UpDigital(int position);
		void DownDigital(int position);
		void ChangeDigitalProductPosition(int position, int newPosition);
		void RebuildDigitalProductIndexes();
		string GetDigitalInfo(RequestDigitalInfoEventArgs args);
	}
}
