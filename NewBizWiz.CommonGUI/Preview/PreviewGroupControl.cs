using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.CommonGUI.Preview
{
	//public partial class PreviewGroupControl : UserControl
	public partial class PreviewGroupControl : XtraTabPage
	{
		private readonly List<Image> _previewImages = new List<Image>();
		public PreviewGroup PreviewGroup { get; private set; }

		public PreviewGroupControl(PreviewGroup previewGroup)
		{
			InitializeComponent();
			PreviewGroup = previewGroup;

			Text = PreviewGroup.Name;
			laSlideSize.Text = String.Format("{0} {1} x {2}", SettingsManager.Instance.Orientation, SettingsManager.Instance.SizeWidth.ToString("#.##"), SettingsManager.Instance.SizeHeght.ToString("#.##"));
			GetPreviewImages();
			Resize += OnResize;
			if (_previewImages.Any())
			{
				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				for (var i = 1; i <= _previewImages.Count; i++)
					comboBoxEditSlides.Properties.Items.Add(i.ToString());
			}
			pnNavigationArea.Visible = _previewImages.Count > 1;
		}

		public void Load()
		{
			if (!_previewImages.Any()) return;
			comboBoxEditSlides.SelectedIndex = 0;
			comboBoxEditSlides_SelectedIndexChanged(null, null);
			comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
		}

		#region Other Event Handlers
		private void OnResize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}

		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureBoxPreview.BackColor = Color.WhiteSmoke;
			pictureBoxPreview.Image = _previewImages[comboBoxEditSlides.SelectedIndex];
			laSlideNumber.Text = String.Format("Slide {0} of {1}", (comboBoxEditSlides.SelectedIndex + 1), _previewImages.Count);
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
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
		#endregion

		#region Common Methods
		private void GetPreviewImages()
		{
			var previewFolderPath = PreviewGroup.ImageSourcePath;
			if (!Directory.Exists(previewFolderPath)) return;
			_previewImages.Clear();
			var previewImages = Directory.GetFiles(previewFolderPath, "*.png");
			Array.Sort(previewImages, WinAPIHelper.StrCmpLogicalW);
			for (int i = 0; i < previewImages.Length; i++)
				_previewImages.Add(new Bitmap(previewImages[i], true));
		}

		public void ClearPreviewImages()
		{
			try
			{
				foreach (var image in _previewImages)
					image.Dispose();
				_previewImages.Clear();
				PreviewGroup.ClearAssets();
			}
			catch { }
		}
		#endregion
	}
}
