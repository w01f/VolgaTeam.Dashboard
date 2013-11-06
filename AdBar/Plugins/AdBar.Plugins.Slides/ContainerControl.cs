using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AdBar.Plugins.Common.BusinessClasses;
using AdBar.Plugins.Core;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Slides;
using NewBizWiz.Core.Common;
using NewBizWiz.MiniBar.InteropClasses;

namespace AdBar.Plugins.Slides
{
	public partial class ContainerControl : UserControl, IAdBarControl
	{
		private readonly PluginController _controller = new PluginController();

		private readonly SlideManager _slideManager = new SlideManager(NewBizWiz.Core.Common.SettingsManager.Instance.SlideMastersPath);

		public ContainerControl()
		{
			InitializeComponent();
			UpdateSlideMasters();
		}

		private void UpdateSlideMasters()
		{
			IEnumerable<SlideMaster> availableSlide = _slideManager.Slides.Where(s => s.SizeWidth == NewBizWiz.Core.Common.SettingsManager.Instance.SizeWidth && s.SizeHeght == NewBizWiz.Core.Common.SettingsManager.Instance.SizeHeght);
			if (availableSlide.Any())
			{
				SlideMaster selectedSlide = availableSlide.FirstOrDefault(s => s.Group.Equals(SettingsManager.Instance.SelectedSlideGroup) && s.Name.Equals(SettingsManager.Instance.SelectedSlideMaster));
				if (selectedSlide == null)
				{
					selectedSlide = availableSlide.FirstOrDefault();
					SettingsManager.Instance.SelectedSlideGroup = selectedSlide.Group;
					SettingsManager.Instance.SelectedSlideMaster = selectedSlide.Name;
					SettingsManager.Instance.SaveSettings();
				}
				buttonItemOutput.Enabled = true;
				buttonItemSlideMaster.Visible = true;
				buttonItemSlideMaster.Image = selectedSlide.AdBarLogo;
				ribbonBar.Text = String.Format("{0}", selectedSlide.Name);
				buttonItemSlideMaster.Tag = selectedSlide;
			}
			else
			{
				buttonItemOutput.Enabled = false;
				buttonItemSlideMaster.Visible = false;
				ribbonBar.Text = "No Slides";
			}
			ribbonBar.RecalcLayout();
		}

		private void AppendSlideMaster(SlideMaster slideMaster)
		{
			if (slideMaster == null) return;
			if (!AdBarPowerPointHelper.Instance.PowerPointDetected())
			{
				if (Utilities.Instance.ShowWarningQuestion("You need to first open PowerPoint\nDo you want to do that now?") == DialogResult.Yes)
					_controller.RunPowerPoint();
				else
					return;
			}
			AdBarPowerPointHelper.Instance.Connect(false);
			AdBarPowerPointHelper.Instance.AppendSlideMaster(slideMaster.MasterPath);
		}

		private void buttonItemOutput_Click(object sender, EventArgs e)
		{
			var selectedSlideMaster = buttonItemSlideMaster.Tag as SlideMaster;
			AppendSlideMaster(selectedSlideMaster);
		}

		private void buttonItemSlideMaster_Click(object sender, EventArgs e)
		{
			using (var form = new FormSlideSelector())
			{
				form.LoadSlides(_slideManager);
				form.Shown += (o, args) => { form.SetSelectedSlide(SettingsManager.Instance.SelectedSlideGroup, SettingsManager.Instance.SelectedSlideMaster); };

				form.AddSlide += (o, args) => AppendSlideMaster(args.SelectedSlide);
				if (form.ShowDialog() == DialogResult.OK)
				{
					SlideMaster selectedSlide = form.SelectedSlide;
					if (selectedSlide == null) return;
					buttonItemSlideMaster.Image = selectedSlide.AdBarLogo;
					ribbonBar.Text = String.Format("{0}", selectedSlide.Name);
					buttonItemSlideMaster.Tag = selectedSlide;
					ribbonBar.RecalcLayout();

					SettingsManager.Instance.SelectedSlideGroup = selectedSlide.Group;
					SettingsManager.Instance.SelectedSlideMaster = selectedSlide.Name;
					SettingsManager.Instance.SaveSettings();

					if (StateChanged != null)
						StateChanged(this, new AdBarControlStateEventArgs());
				}
			}
		}

		#region IAdBarControl Memebers

		public string ControlName
		{
			get { return "slidestab"; }
		}

		public IEnumerable<RibbonBar> RibbonBars
		{
			get { return new[] {ribbonBar}; }
		}

		public event EventHandler<AdBarControlStateEventArgs> StateChanged;

		public void UpdateControl(IAdBarControl raisedBy, object[] stateParameters)
		{
			if (raisedBy != null && raisedBy.ControlName.Equals("slidepack"))
				UpdateSlideMasters();
		}

		#endregion
	}
}