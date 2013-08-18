using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormPreview : Form
	{
		private readonly List<Image> _previewImages = new List<Image>();

		public FormPreview()
		{
			InitializeComponent();
		}

		#region Form GUI Event Habdlers
		private void FormQuickView_Shown(object sender, EventArgs e)
		{
			barOperations.ItemLinks[2].Visible = !CalendarPowerPointHelper.Instance.Is2003;
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				laSlideSize.Text = string.Format("{0} {1} x {2}", new object[] { SettingsManager.Instance.Orientation, SettingsManager.Instance.SizeWidth.ToString("#.##"), SettingsManager.Instance.SizeHeght.ToString("#.##") });
				GetPreviewImages();
				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				for (int i = 1; i <= _previewImages.Count; i++)
					comboBoxEditSlides.Properties.Items.Add(i.ToString());
				if (_previewImages.Count > 0)
					comboBoxEditSlides.SelectedIndex = 0;
				comboBoxEditSlides_SelectedIndexChanged(null, null);
				comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
			}
		}

		private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClearPreviewImages();
		}

		private void FormQuickView_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOutput_ItemClick(object sender, ItemClickEventArgs e)
		{
			var result = DialogResult.Cancel;
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				using (var formProgress = new FormProgress())
				{
					formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
					formProgress.TopMost = true;
					formProgress.Show();
					CalendarPowerPointHelper.Instance.AppendSlidesFromFile(PresentationFile);
					formProgress.Close();
					using (var formOutput = new FormSlideOutput())
					{
						result = formOutput.ShowDialog();
					}
				}
			}
			DialogResult = result;
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("preview");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}
		#endregion

		#region Other Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				pictureBoxPreview.Image = _previewImages[comboBoxEditSlides.SelectedIndex];
				laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (comboBoxEditSlides.SelectedIndex + 1).ToString(), _previewImages.Count.ToString() });
			}
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				if (e.Button.Index == 1)
				{
					int selectedIndex = comboBoxEditSlides.SelectedIndex + 1;
					if (selectedIndex >= _previewImages.Count)
						selectedIndex = 0;
					comboBoxEditSlides.SelectedIndex = selectedIndex;
				}
				else if (e.Button.Index == 2)
				{
					int selectedIndex = comboBoxEditSlides.SelectedIndex - 1;
					if (selectedIndex < 0)
						selectedIndex = _previewImages.Count - 1;
					comboBoxEditSlides.SelectedIndex = selectedIndex;
				}
			}
		}
		#endregion

		#region Common Methods
		private void GetPreviewImages()
		{
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				string previewFolderPath = PresentationFile.Replace(Path.GetExtension(PresentationFile), string.Empty);
				if (Directory.Exists(previewFolderPath))
				{
					_previewImages.Clear();
					string[] previewImages = Directory.GetFiles(previewFolderPath, "*.png");
					Array.Sort(previewImages, (x, y) => WinAPIHelper.StrCmpLogicalW(x, y));
					for (int i = 0; i < previewImages.Length; i++)
						_previewImages.Add(new Bitmap(previewImages[i], true));
				}
			}
		}

		private void ClearPreviewImages()
		{
			try
			{
				foreach (Image image in _previewImages)
					image.Dispose();
				_previewImages.Clear();
				File.Delete(PresentationFile);
				Directory.Delete(PresentationFile.Replace(Path.GetExtension(PresentationFile), string.Empty), true);
			}
			catch { }
		}
		#endregion

		public string PresentationFile { get; set; }
	}
}