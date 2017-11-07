using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.Preview
{
	//public partial class PreviewGroupControl : UserControl
	public partial class PreviewGroupControl : XtraTabPage
	{
		private readonly List<Image> _previewImages = new List<Image>();
		public PreviewGroup PreviewGroup { get; }

		public PreviewGroupControl(PreviewGroup previewGroup)
		{
			InitializeComponent();
			PreviewGroup = previewGroup;

			Text = PreviewGroup.Name;
			simpleLabelItemSlideSize.Text = String.Format("<size=+2>{0} {1:#.##} x {2:#.##}</size>", PowerPointManager.Instance.SlideSettings.SlideSize.Orientation, PowerPointManager.Instance.SlideSettings.SlideSize.Width, PowerPointManager.Instance.SlideSettings.SlideSize.Height);
			GetPreviewImages();
			if (_previewImages.Any())
			{
				comboBoxEditSlides.SelectedIndexChanged -= comboBoxEditSlides_SelectedIndexChanged;
				comboBoxEditSlides.Properties.Items.Clear();
				for (var i = 1; i <= _previewImages.Count; i++)
					comboBoxEditSlides.Properties.Items.Add(i.ToString());
			}
			layoutControlGroupNavigation.Visibility = _previewImages.Count > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		public void Load()
		{
			if (!_previewImages.Any()) return;
			comboBoxEditSlides.SelectedIndex = 0;
			comboBoxEditSlides_SelectedIndexChanged(null, null);
			comboBoxEditSlides.SelectedIndexChanged += comboBoxEditSlides_SelectedIndexChanged;
		}

		#region Other Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			pictureEditPreview.BackColor = Color.WhiteSmoke;
			pictureEditPreview.Image = _previewImages[comboBoxEditSlides.SelectedIndex];
			simpleLabelItemSlideNumber.Text = String.Format("<size=+2>Slide {0} of {1}<>", (comboBoxEditSlides.SelectedIndex + 1), _previewImages.Count);
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
				pictureEditPreview.Image = null;
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
