using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms
{
	public partial class FormEmail : Form
	{
		private readonly List<Image> _previewImages = new List<Image>();

		public FormEmail()
		{
			InitializeComponent();
		}

		#region Form GUI Event Habdlers
		private void FormQuickView_Shown(object sender, EventArgs e)
		{
			barOperations.ItemLinks[2].Visible = !OnlineSchedulePowerPointHelper.Instance.Is2003;
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
		private void barButtonItemRegularEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				using (var form = new FormEmailFileName())
				{
					RegistryHelper.MainFormHandle = form.Handle;
					if (form.ShowDialog() == DialogResult.OK)
					{
						string emailFile = Path.Combine(Path.GetFullPath(PresentationFile).Replace(Path.GetFileName(PresentationFile), string.Empty), form.FileName + ".ppt");
						try
						{
							File.Copy(PresentationFile, emailFile, true);
							if (OutlookHelper.Instance.Open())
							{
								OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
								OutlookHelper.Instance.Close();
							}
							else
								Utilities.Instance.ShowWarning("Cannot open Outlook");
							File.Delete(emailFile);
						}
						catch {}
					}
					RegistryHelper.MainFormHandle = Handle;
				}
			}
		}

		private void barLargeButtonItemLockedEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				using (var form = new FormEmailFileName())
				{
					RegistryHelper.MainFormHandle = form.Handle;
					if (form.ShowDialog() == DialogResult.OK)
					{
						string emailFile = Path.Combine(Path.GetFullPath(PresentationFile).Replace(Path.GetFileName(PresentationFile), string.Empty), form.FileName + ".ppt");
						try
						{
							OnlineSchedulePowerPointHelper.Instance.CreateLockedPresentation(PresentationFile.Replace(Path.GetExtension(PresentationFile), string.Empty), emailFile);
							if (File.Exists(emailFile))
							{
								if (OutlookHelper.Instance.Open())
								{
									OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
									OutlookHelper.Instance.Close();
								}
								else
									Utilities.Instance.ShowWarning("Cannot open Outlook");
								File.Delete(emailFile);
							}
						}
						catch {}
					}
					RegistryHelper.MainFormHandle = Handle;
				}
			}
		}

		private void barLargeButtonItemPDFEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!string.IsNullOrEmpty(PresentationFile))
			{
				using (var form = new FormEmailFileName())
				{
					RegistryHelper.MainFormHandle = form.Handle;
					if (form.ShowDialog() == DialogResult.OK)
					{
						string emailFile = Path.Combine(Path.GetFullPath(PresentationFile).Replace(Path.GetFileName(PresentationFile), string.Empty), form.FileName + ".pdf");
						try
						{
							OnlineSchedulePowerPointHelper.Instance.ConvertToPDF(PresentationFile, emailFile);
							if (File.Exists(emailFile))
							{
								if (OutlookHelper.Instance.Open())
								{
									OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
									OutlookHelper.Instance.Close();
								}
								else
									Utilities.Instance.ShowWarning("Cannot open Outlook");
								File.Delete(emailFile);
							}
						}
						catch {}
					}
					RegistryHelper.MainFormHandle = Handle;
				}
			}
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("email");
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
			catch {}
		}
		#endregion

		public string PresentationFile { get; set; }
	}
}