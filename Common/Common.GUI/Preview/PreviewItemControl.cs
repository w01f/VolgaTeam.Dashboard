﻿using System;
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
    //public partial class PreviewItemControl : UserControl
    public partial class PreviewItemControl : XtraTabPage
    {
        private readonly PreviewGroupControl _groupControl;
        private readonly List<Image> _previewImages = new List<Image>();
        public OutputItem OutputItem { get; }
        public bool IsLoaded { get; set; }

        public bool IsEnabled => OutputItem.Enabled;

        public PreviewItemControl(OutputItem outputItem, PreviewGroupControl groupControl)
        {
            _groupControl = groupControl;
            InitializeComponent();
            OutputItem = outputItem;

            Text = OutputItem.Name;
            simpleLabelItemSlideSize.Text = String.Format("<size=+2>{0} {1:#.##} x {2:#.##}</size>", SlideSettingsManager.Instance.SlideSettings.SlideSize.Orientation, SlideSettingsManager.Instance.SlideSettings.SlideSize.Width, SlideSettingsManager.Instance.SlideSettings.SlideSize.Height);
            layoutControl.Enabled = outputItem.Enabled;
        }

        public void Load()
        {
            if (!IsEnabled) return;
            if (IsLoaded) return;

            GetPreviewImages();

            if (_previewImages.Any())
            {
                comboBoxEditSlides.SelectedIndexChanged -= OnSelectedSlideIndexChanged;
                comboBoxEditSlides.Properties.Items.Clear();
                for (var i = 1; i <= _previewImages.Count; i++)
                    comboBoxEditSlides.Properties.Items.Add(i.ToString());

                comboBoxEditSlides.SelectedIndex = 0;

                OnSelectedSlideIndexChanged(null, null);
                comboBoxEditSlides.SelectedIndexChanged += OnSelectedSlideIndexChanged;
            }

            layoutControlGroupNavigation.Visibility = _previewImages.Count > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;

            OutputItem.SlidesCount = _previewImages.Count > OutputItem.SlidesCount ?
                _previewImages.Count :
                OutputItem.SlidesCount;

            IsLoaded = true;
        }

        public void EnableSlide()
        {
            var needToLoad = !OutputItem.Enabled;
            OutputItem.Enabled = true;

            if (needToLoad)
                _groupControl.LoadPreviewControl(this);

            Load();

            layoutControl.Enabled = true;
        }

        public void DisableSlide()
        {
            OutputItem.Enabled = false;
            layoutControl.Enabled = false;
        }

        #region Other Event Handlers
        private void OnSelectedSlideIndexChanged(object sender, EventArgs e)
        {
            pictureEditPreview.BackColor = Color.WhiteSmoke;
            pictureEditPreview.Image = _previewImages[comboBoxEditSlides.SelectedIndex];
            simpleLabelItemSlideNumber.Text = String.Format("<size=+2>Slide {0} of {1}</size>", (comboBoxEditSlides.SelectedIndex + 1), _previewImages.Count);
        }

        private void OnSlidesButtonClick(object sender, ButtonPressedEventArgs e)
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
            var previewFolderPath = OutputItem.ImageSourcePath;
            if (!Directory.Exists(previewFolderPath)) return;
            _previewImages.Clear();
            var previewImageFiles = Directory.GetFiles(previewFolderPath, "*.png");
            Array.Sort(previewImageFiles, WinAPIHelper.StrCmpLogicalW);
            foreach (String previewImageFilePath in previewImageFiles)
                _previewImages.Add(new Bitmap(previewImageFilePath, true));
        }

        public void ClearPreviewImages()
        {
            try
            {
                pictureEditPreview.Image = null;
                foreach (var image in _previewImages)
                    image.Dispose();
                _previewImages.Clear();
                OutputItem.ClearAssets();
            }
            catch { }
        }
        #endregion
    }
}
