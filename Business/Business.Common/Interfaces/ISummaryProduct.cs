using System;
using Asa.Business.Common.Entities.NonPersistent.Summary;

namespace Asa.Business.Common.Interfaces
{
	public interface ISummaryProduct
	{
		Guid UniqueID { get; }
		decimal SummaryOrder { get; }
		string SummaryTitle { get; }
		string SummaryInfo { get; }
		CustomSummaryItem SummaryItem { get; }
	}
}
