using System;
using System.Linq;
using Asa.Business.Media.Interfaces;
using Asa.Common.Core.Objects.Output;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Controls.BusinessClasses.Output.DigitalInfo
{
	public class StandaloneDigitalInfoOutputModel : BaseDigitalInfoOutputModel
	{
		private readonly IDigitalInfoContainer _container;
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public string FlightDates { get; set; }
		public string Color { get; set; }

		public ContractSettings ContractSettings => _container.ContractSettings;

		public string TemplateFilePath => BusinessObjects.Instance.OutputManager.GetDigitalOneSheetFile(Color, Records.Count);

		public StandaloneDigitalInfoOutputModel(IDigitalInfoContainer container) : base(container.DigitalInfo)
		{
			_container = container;
		}

		public override void PopulateReplacementsList()
		{
			base.PopulateReplacementsList();

			var key = "Flightdates";
			var value = FlightDates;
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
			key = "Advertiser  -  Decisionmaker";
			value = String.Format("{0}  -  {1}", Advertiser, DecisionMaker);
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
			key = "Investment:";
			value = SummaryInfo;
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
		}
	}
}
