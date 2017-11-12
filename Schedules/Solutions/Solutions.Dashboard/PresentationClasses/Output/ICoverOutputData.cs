using System;

namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public interface ICoverOutputData : IDashboardOutputData
	{
		string PresentationDate { get; }
		string Title { get; }
		string DecisionMaker { get; }
		string Advertiser { get; }
		string Quote { get; }
		string SalesRep { get; }
		bool AddAsPageOne { get; }
	}
}
