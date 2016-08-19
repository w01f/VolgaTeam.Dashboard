using System;
using Asa.Business.Solutions.Dashboard.Dictionaries;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class CoverState
	{
		public bool ShowPresentationDate { get; set; }

		public string SlideHeader { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime PresentationDate { get; set; }
		public Quote Quote { get; set; }
		public bool AddAsPageOne { get; set; }

		public CoverState()
		{
			Quote = new Quote();
			ShowPresentationDate = true;
			AddAsPageOne = true;
		}
	}
}
