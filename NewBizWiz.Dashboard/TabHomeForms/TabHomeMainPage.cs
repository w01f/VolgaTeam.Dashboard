using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace NewBizWiz.Dashboard.TabHomeForms
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

		public void UpdatePageAccordingToggledButton(SlideType selectedSlide)
		{
			_selectedSlide = selectedSlide;
			switch (_selectedSlide)
			{
				case SlideType.Cleanslate:
					SlideCleanslate.SelectSlideType(selectedSlide);
					SlideCleanslate.UpdateOutputState();
					FormMain.Instance.OutputClick = SlideCleanslate.Output;
					FormMain.Instance.PreviewClick = SlideCleanslate.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideCleanslate.Tooltip);
					if (!Controls.Contains(SlideCleanslate))
						Controls.Add(SlideCleanslate);
					SlideCleanslate.BringToFront();
					break;
				case SlideType.Cover:
					SlideCover.SelectSlideType(selectedSlide);
					SlideCover.UpdateSavedFilesState();
					SlideCover.UpdateOutputState();
					FormMain.Instance.OutputClick = SlideCover.Output;
					FormMain.Instance.PreviewClick = SlideCover.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideCover.Tooltip);
					if (!Controls.Contains(SlideCover))
						Controls.Add(SlideCover);
					SlideCover.BringToFront();
					break;
				case SlideType.LeadoffStatement:
					SlideLeadoff.SelectSlideType(selectedSlide);
					SlideLeadoff.UpdateSavedFilesState();
					SlideLeadoff.UpdateOutputState();
					FormMain.Instance.OutputClick = SlideLeadoff.Output;
					FormMain.Instance.PreviewClick = SlideLeadoff.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideLeadoff.Tooltip);
					if (!Controls.Contains(SlideLeadoff))
						Controls.Add(SlideLeadoff);
					SlideLeadoff.BringToFront();
					break;
				case SlideType.ClientGoals:
					SlideClientGoals.SelectSlideType(selectedSlide);
					SlideClientGoals.UpdateSavedFilesState();
					SlideClientGoals.UpdateOutputState();
					FormMain.Instance.OutputClick = SlideClientGoals.Output;
					FormMain.Instance.PreviewClick = SlideClientGoals.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideClientGoals.Tooltip);
					if (!Controls.Contains(SlideClientGoals))
						Controls.Add(SlideClientGoals);
					SlideClientGoals.BringToFront();
					break;
				case SlideType.TargetCustomers:
					SlideTargetCustomers.SelectSlideType(selectedSlide);
					SlideTargetCustomers.UpdateSavedFilesState();
					SlideTargetCustomers.UpdateOutputState();
					FormMain.Instance.OutputClick = SlideTargetCustomers.Output;
					FormMain.Instance.PreviewClick = SlideTargetCustomers.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideTargetCustomers.Tooltip);
					if (!Controls.Contains(SlideTargetCustomers))
						Controls.Add(SlideTargetCustomers);
					SlideTargetCustomers.BringToFront();
					break;
				case SlideType.SimpleSummary:
					SlideSimpleSummary.SelectSlideType(selectedSlide);
					SlideSimpleSummary.UpdateSavedFilesState();
					SlideSimpleSummary.ResetTab();
					FormMain.Instance.OutputClick = SlideSimpleSummary.Output;
					FormMain.Instance.PreviewClick = SlideSimpleSummary.Preview;
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, SlideSimpleSummary.Tooltip);
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