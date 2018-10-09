using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Slides;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.PresentationClasses.Slides
{
	[ToolboxItem(false)]
	public partial class MediaSlidesControl : UserControl, IContentControl, IOutputControl
	{
		private SlidesContainerControl _slideContainer;
		public bool IsActive { get; set; }
		public string Identifier => ContentIdentifiers.Slides;
		public bool RequreScheduleInfo => false;
		public bool ShowScheduleInfo => false;
		public bool RibbonAlwaysCollapsed => false;
		public RibbonTabItem TabPage => Controller.Instance.TabSlides;

		public MediaSlidesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		private void LoadSlides()
		{
			if (_slideContainer != null)
			{
				pnMain.Controls.Remove(_slideContainer);
				_slideContainer.Dispose();
			}

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(BusinessObjects.Instance.SlideManager, new Size());
			_slideContainer.SlideOutput += (o, e) => OutputPowerPoint(e.SlideMaster);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		public void InitMetaData()
		{
			TabPage.Tag = Identifier;
		}

		public void InitControl()
		{
			Controller.Instance.SlidesLogoLabel.Image = BusinessObjects.Instance.ImageResourcesManager.MainAppRibbonLogo ?? Controller.Instance.SlidesLogoLabel.Image;
			Controller.Instance.SlidesLogoBar.RecalcLayout();
			Controller.Instance.SlidesPanel.PerformLayout();

			LoadSlides();
			SlideSettingsManager.Instance.SettingsChanged += (o, e) => LoadSlides();
		}

		public void ShowControl(ContentOpenEventArgs args = null)
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = true;
			IsActive = true;
			ContentStatusBarManager.Instance.FillStatusBarMainCommonInfo();
			ContentStatusBarManager.Instance.FillStatusBarAdditionalCommonInfo();
		}

		public void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("Slides");
		}

		private IList<OutputItem> GetOutputItems(SlideMaster slideMaster = null)
		{
			var selectedSlideMaster = slideMaster ?? _slideContainer.SelectedSlide;
			var defaultOutputGroup = new OutputGroup
			{
				Name = "Preview",
				IsCurrent = true,
				Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = "Preview",
						IsCurrent = true,
						SlidesCount = 1,
						PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							var templatePath = selectedSlideMaster.GetMasterPath();
							processor.AppendSlideMaster(templatePath, destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							var templatePath = selectedSlideMaster.GetMasterPath();
							processor.PreparePresentation(presentationSourcePath,
								presentation => processor.AppendSlideMaster(templatePath, presentation));
						}
					}
				})
			};

			var selectedOutputItems = new List<OutputItem>();
			using (var form = new FormPreview(
				Controller.Instance.FormMain,
				BusinessObjects.Instance.PowerPointManager.Processor))
			{
				form.LoadGroups(new[] { defaultOutputGroup });
				if (form.ShowDialog() == DialogResult.OK)
					selectedOutputItems.AddRange(form.GetSelectedItems());
			}

			return selectedOutputItems;
		}

		public void OutputPowerPoint(SlideMaster slideMaster = null)
		{
			var outputItems = GetOutputItems(slideMaster);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				foreach (var outputItem in outputItems)
					outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, null);
				FormProgress.CloseProgress();
			});
		}

		public void OutputPdf(SlideMaster slideMaster = null)
		{
			var outputItems = GetOutputItems(slideMaster);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", (slideMaster ?? _slideContainer.SelectedSlide).Name, DateTime.Now));
				BusinessObjects.Instance.PowerPointManager.Processor.BuildPdf(pdfFileName, presentation =>
				{
					foreach (var outputItem in outputItems)
						outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
				});
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}

		public void Email(SlideMaster slideMaster = null)
		{
			var outputItems = GetOutputItems(slideMaster);
			if (!outputItems.Any()) return;

			using (var form = new FormEmailFileName())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Email...");
					FormProgress.ShowProgress();
					Controller.Instance.ShowFloater(() =>
					{
						var emailFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", (slideMaster ?? _slideContainer.SelectedSlide).Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
						var defaultItem = outputItems.First();
						BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(emailFileName, presentation =>
						{
							foreach (var outputItem in outputItems)
								outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
						});

						var emailFile = Path.Combine(
							Path.GetFullPath(defaultItem.PresentationSourcePath)
								.Replace(Path.GetFileName(defaultItem.PresentationSourcePath), string.Empty),
							form.FileName + ".pptx");
						File.Copy(emailFileName, emailFile, true);

						FormProgress.CloseProgress();

						try
						{
							if (OutlookHelper.Instance.Open())
							{
								OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
								OutlookHelper.Instance.Close();
							}
							else
								PopupMessageHelper.Instance.ShowWarning("Cannot open Outlook");
							File.Delete(emailFile);
						}
						catch { }
					});
				}
			}
		}

		public void EditSettings()
		{
			throw new NotImplementedException();
		}

		public void OutputPowerPoint()
		{
			OutputPowerPoint(null);
		}

		public void OutputPdf()
		{
			OutputPdf(null);
		}

		public void Email()
		{
			Email(null);
		}
	}
}