using System;
using Asa.Business.Solutions.Dashboard.Dictionaries;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class CoverState : DasboardDataState
	{
		public bool AddAsPageOne { get; set; }
		public bool UseGenericCover { get; set; }
		public bool ShowPresentationDate { get; set; }

		public string SlideHeader { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime PresentationDate { get; set; }
		public Quote Quote { get; set; }
		public string SalesRep { get; set; }

		public CoverState()
		{
			AddAsPageOne = true;
			Quote = new Quote();
		}
	}
}
