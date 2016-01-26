using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Core.Common;
using Asa.Core.Dashboard;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public sealed partial class TabHomeMainPage : UserControl
	{
		private static TabHomeMainPage _instance;

		private SlideType _selectedSlide = SlideType.None;

		public SlideCleanslateControl SlideCleanslate { get; private set; }
		public SlideCoverControl SlideCover { get; private set; }
		public SlideLeadoffStatementControl SlideLeadoff { get; private set; }
		public SlideClientGoalsControl SlideClientGoals { get; private set; }
		public SlideTargetCustomersControl SlideTargetCustomers { get; private set; }
		public SlideSimpleSummaryControl SlideSimpleSummary { get; private set; }

		public string HelpKey
		{
			get
			{
				switch (_selectedSlide)
				{
					case SlideType.Cleanslate:
						return "Home";
					case SlideType.Cover:
						return "Cover";
					case SlideType.LeadoffStatement:
						return "Intro";
					case SlideType.ClientGoals:
						return "Needs";
					case SlideType.TargetCustomers:
						return "Target";
					case SlideType.SimpleSummary:
						return "Closing";
					default:
						return String.Empty;
				}
			}
		}

		public string SelectedSlideName
		{
			get
			{
				switch (_selectedSlide)
				{
					case SlideType.Cleanslate:
						return SlideCleanslate.SlideName;
					case SlideType.Cover:
						return SlideCover.SlideName;
					case SlideType.LeadoffStatement:
						return SlideLeadoff.SlideName;
					case SlideType.ClientGoals:
						return SlideClientGoals.SlideName;
					case SlideType.TargetCustomers:
						return SlideTargetCustomers.SlideName;
					case SlideType.SimpleSummary:
						return SlideSimpleSummary.SlideName;
					default:
						return String.Empty;
				}
			}
		}

		private TabHomeMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			SlideCleanslate = new SlideCleanslateControl();
			SlideCleanslate.SlideChanged += OnSlideChanged;
			SlideCover = new SlideCoverControl();
			SlideCover.SlideChanged += OnSlideChanged;
			SlideLeadoff = new SlideLeadoffStatementControl();
			SlideLeadoff.SlideChanged += OnSlideChanged;
			SlideClientGoals = new SlideClientGoalsControl();
			SlideClientGoals.SlideChanged += OnSlideChanged;
			SlideTargetCustomers = new SlideTargetCustomersControl();
			SlideTargetCustomers.SlideChanged += OnSlideChanged;
			SlideSimpleSummary = new SlideSimpleSummaryControl();
			SlideSimpleSummary.SlideChanged += OnSlideChanged;
			Core.Dashboard.SettingsManager.Instance.ThemeManager.ThemesChanged += (o, e) => UpdatePageAccordingToggledButton();

			AppManager.Instance.SetClickEventHandler(this);
		}

		private void OnSlideChanged(object sender, SlideEventArgs args)
		{
			UpdatePageAccordingToggledButton(args.SlideType);
		}

		public static TabHomeMainPage Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TabHomeMainPage();
				return _instance;
			}
		}

		private void UpdatePageAccordingToggledButton()
		{
			UpdatePageAccordingToggledButton(_selectedSlide);
		}

		public void UpdatePageAccordingToggledButton(SlideType selectedSlide)
		{
			_selectedSlide = selectedSlide;
			switch (_selectedSlide)
			{
				case SlideType.Cleanslate:
					SlideCleanslate.SelectSlideType(selectedSlide);
					SlideCleanslate.UpdateOutputState();
					FormMain.Instance.LoadClick = SlideCleanslate.LoadClick;
					FormMain.Instance.OutputClick = SlideCleanslate.Output;
					FormMain.Instance.PreviewClick = SlideCleanslate.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeLoad, SlideCleanslate.TooltipLoad);
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideCleanslate.TooltipHelp);
					if (!Controls.Contains(SlideCleanslate))
						Controls.Add(SlideCleanslate);
					SlideCleanslate.BringToFront();
					break;
				case SlideType.Cover:
					SlideCover.SelectSlideType(selectedSlide);
					SlideCover.UpdateOutputState();
					FormMain.Instance.LoadClick = SlideCover.LoadClick;
					FormMain.Instance.OutputClick = SlideCover.Output;
					FormMain.Instance.PreviewClick = SlideCover.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeLoad, SlideCover.TooltipLoad);
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideCover.TooltipHelp);
					if (!Controls.Contains(SlideCover))
						Controls.Add(SlideCover);
					SlideCover.BringToFront();
					break;
				case SlideType.LeadoffStatement:
					SlideLeadoff.SelectSlideType(selectedSlide);
					SlideLeadoff.UpdateOutputState();
					FormMain.Instance.LoadClick = SlideLeadoff.LoadClick;
					FormMain.Instance.OutputClick = SlideLeadoff.Output;
					FormMain.Instance.PreviewClick = SlideLeadoff.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeLoad, SlideLeadoff.TooltipLoad);
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideLeadoff.TooltipHelp);
					if (!Controls.Contains(SlideLeadoff))
						Controls.Add(SlideLeadoff);
					SlideLeadoff.BringToFront();
					break;
				case SlideType.ClientGoals:
					SlideClientGoals.SelectSlideType(selectedSlide);
					SlideClientGoals.UpdateOutputState();
					FormMain.Instance.LoadClick = SlideClientGoals.LoadClick;
					FormMain.Instance.OutputClick = SlideClientGoals.Output;
					FormMain.Instance.PreviewClick = SlideClientGoals.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeLoad, SlideClientGoals.TooltipLoad);
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideClientGoals.TooltipHelp);
					if (!Controls.Contains(SlideClientGoals))
						Controls.Add(SlideClientGoals);
					SlideClientGoals.BringToFront();
					break;
				case SlideType.TargetCustomers:
					SlideTargetCustomers.SelectSlideType(selectedSlide);
					SlideTargetCustomers.UpdateOutputState();
					FormMain.Instance.LoadClick = SlideTargetCustomers.LoadClick;
					FormMain.Instance.OutputClick = SlideTargetCustomers.Output;
					FormMain.Instance.PreviewClick = SlideTargetCustomers.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeLoad, SlideTargetCustomers.TooltipLoad);
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideTargetCustomers.TooltipHelp);
					if (!Controls.Contains(SlideTargetCustomers))
						Controls.Add(SlideTargetCustomers);
					SlideTargetCustomers.BringToFront();
					break;
				case SlideType.SimpleSummary:
					SlideSimpleSummary.SelectSlideType(selectedSlide);
					SlideSimpleSummary.ResetTab();
					FormMain.Instance.LoadClick = SlideSimpleSummary.LoadClick;
					FormMain.Instance.OutputClick = SlideSimpleSummary.Output;
					FormMain.Instance.PreviewClick = SlideSimpleSummary.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeLoad, SlideSimpleSummary.TooltipLoad);
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideSimpleSummary.TooltipHelp);
					if (!Controls.Contains(SlideSimpleSummary))
						Controls.Add(SlideSimpleSummary);
					SlideSimpleSummary.BringToFront();
					break;
				default:
					pnEmpty.BringToFront();
					break;
			}
		}
	}
}