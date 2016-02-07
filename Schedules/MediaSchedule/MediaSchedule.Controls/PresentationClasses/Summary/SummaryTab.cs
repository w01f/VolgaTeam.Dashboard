using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	//public partial class SummaryTab : UserControl
	public partial class SummaryTab : XtraTabPage
	{
		public ScheduleSection Section { get; private set; }
		public ISectionSummaryControl Content { get; private set; }
		public event EventHandler<EventArgs> SummaryTypeChanged;
		public event EventHandler<EventArgs> DataChanged;

		public SummaryTab()
		{
			InitializeComponent();
		}

		public void LoadData(ScheduleSection section, bool quickLoad)
		{
			Section = section;
			Text = Section.Name.Replace("&", "&&");
			if (!quickLoad || Content == null)
				ResetContent();
			else
				Content.LoadData(Section.Summary, true);
		}

		public void SaveData()
		{
			Content.SaveData();
		}

		public void Release()
		{
			SummaryTypeChanged = null;
			DataChanged = null;
			Content.Release();
			Content = null;
			Section = null;
		}

		public void ResetContent()
		{
			Controls.Clear();
			if (Content != null)
			{
				Content.Release();
				((Control)Content).Dispose();
			}
			switch (Section.Summary.SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
					Content = new ProductSummaryControl();
					break;
				case SectionSummaryTypeEnum.Custom:
					Content = new CustomSummaryControl();
					break;
				case SectionSummaryTypeEnum.Strategy:
					Content = new StrategySummaryControl();
					break;
				default:
					throw new ArgumentOutOfRangeException("Summary type is not recognized");
			}
			Content.LoadData(Section.Summary, false);
			Content.DataChanged += OnDataChanged;
			Controls.Add((Control)Content);
			if (SummaryTypeChanged != null)
				SummaryTypeChanged(this, EventArgs.Empty);
		}

		private void RaiseDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void OnDataChanged(object sender, EventArgs e)
		{
			RaiseDataChanged();
		}
	}
}
