using System;
using Asa.Business.Online.Enums;
using Asa.Business.Online.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public abstract class BasePackageRecord
	{
		public IDigitalPackageSettingsContainer SettingsContainer { get; set; }

		public abstract string Category { get; set; }
		public abstract string SubCategory { get; set; }
		public abstract string Name { get; set; }
		public abstract string Info { get; set; }
		public abstract string Location { get; set; }
		public abstract decimal? Rate { get; set; }
		public abstract decimal? Investment { get; set; }
		public abstract decimal? Impressions { get; set; }
		public abstract decimal? CPM { get; set; }

		public bool UseFormula { get; set; }

		[JsonIgnore]
		public decimal? InvestmentCalculated
		{
			get
			{
				if (UseFormula &&
					(SettingsContainer.DigitalPackageSettings.ShowInvestment &&
						SettingsContainer.DigitalPackageSettings.ShowImpressions &&
						SettingsContainer.DigitalPackageSettings.ShowCPM) &&
					SettingsContainer.DigitalPackageSettings.Formula == FormulaType.Investment)
					Investment = CPMCalculated.HasValue && ImpressionsCalculated.HasValue ? Math.Round(CPMCalculated.Value * (ImpressionsCalculated.Value / 1000), 2) : (decimal?)null;
				return Investment;
			}
			set
			{
				Investment = value;
			}
		}

		[JsonIgnore]
		public decimal? ImpressionsCalculated
		{
			get
			{
				if (UseFormula &&
					(SettingsContainer.DigitalPackageSettings.ShowInvestment &&
						SettingsContainer.DigitalPackageSettings.ShowImpressions &&
						SettingsContainer.DigitalPackageSettings.ShowCPM) &&
					SettingsContainer.DigitalPackageSettings.Formula == FormulaType.Impressions)
					Impressions = InvestmentCalculated.HasValue && CPMCalculated.HasValue && CPMCalculated.Value != 0 ? Math.Round(((InvestmentCalculated.Value * 1000) / CPMCalculated.Value), 0) : (decimal?)null;
				return Impressions;
			}
			set
			{
				Impressions = value;
			}
		}

		[JsonIgnore]
		public decimal? CPMCalculated
		{
			get
			{
				if (UseFormula &&
					(SettingsContainer.DigitalPackageSettings.ShowInvestment &&
						SettingsContainer.DigitalPackageSettings.ShowImpressions &&
						SettingsContainer.DigitalPackageSettings.ShowCPM) &&
					SettingsContainer.DigitalPackageSettings.Formula == FormulaType.CPM)
					CPM = InvestmentCalculated.HasValue && ImpressionsCalculated.HasValue && ImpressionsCalculated.Value != 0 ? Math.Round((InvestmentCalculated.Value / (ImpressionsCalculated.Value / 1000)), 2) : (decimal?)null;
				return CPM;
			}
			set
			{
				CPM = value;
			}
		}

		protected BasePackageRecord() { }

		protected BasePackageRecord(IDigitalPackageSettingsContainer settingsContainer)
		{
			SettingsContainer = settingsContainer;
		}

		public virtual void Dispose()
		{
			SettingsContainer = null;
		}
	}
}
