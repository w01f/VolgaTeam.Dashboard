using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.Preview
{
	public partial class FormPreview : MetroForm
	{
		private bool _allowHandleButtonEvents;
		private readonly PowerPointProcessor _mainPowerPointProcessor;
		private readonly Form _parentForm;

		private PreviewGroupControl SelectedGroupControl => xtraTabControlGroups.SelectedTabPage as PreviewGroupControl;

		public FormPreview(
			Form parentForm,
			PowerPointProcessor mainPowerPointProcessor)
		{
			InitializeComponent();
			_parentForm = parentForm;
			_mainPowerPointProcessor = mainPowerPointProcessor;

			Width = (Int32)(_parentForm.Width * 0.8);
			Height = (Int32)(_parentForm.Height * 0.8);

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemEnableOutput.MaxSize = RectangleHelper.ScaleSize(layoutControlItemEnableOutput.MaxSize, scaleFactor);
			layoutControlItemEnableOutput.MinSize = RectangleHelper.ScaleSize(layoutControlItemEnableOutput.MinSize, scaleFactor);
			layoutControlItemDisableOutput.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDisableOutput.MaxSize, scaleFactor);
			layoutControlItemDisableOutput.MinSize = RectangleHelper.ScaleSize(layoutControlItemDisableOutput.MinSize, scaleFactor);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		public void LoadGroups(IList<OutputGroup> outputGroups)
		{
			xtraTabControlGroups.TabPages.Clear();

			var tabPages = outputGroups
				.Where(outputGroup => outputGroup.Items.Any())
				.Select(outputGroup => new PreviewGroupControl(outputGroup, _mainPowerPointProcessor, _parentForm, this)).ToList();
			foreach (var tabPage in tabPages)
			{
				tabPage.PreviewItemChanged += OnPreviewItemChanged;
				xtraTabControlGroups.TabPages.Add(tabPage);
			}
			xtraTabControlGroups.SelectedTabPage = xtraTabControlGroups.TabPages
				.OfType<PreviewGroupControl>()
				.FirstOrDefault(previewGroupControl => previewGroupControl.OutputGroup.IsCurrent) ?? xtraTabControlGroups.SelectedTabPage;
			xtraTabControlGroups.ShowTabHeader = DefaultBoolean.True;
			xtraTabControlGroups.SelectedPageChanged += OnSelectedPreviewItemChanged;

			CalculateSlides();

			layoutControlItemDisableOutput.Visibility =
				layoutControlItemEnableOutput.Visibility =
					outputGroups.SelectMany(outputGroup => outputGroup.Items).Count(item => item.Enabled) > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		public IList<OutputItem> GetSelectedItems()
		{
			return xtraTabControlGroups.TabPages
				.OfType<PreviewGroupControl>()
				.SelectMany(groupControl => groupControl.OutputItems)
				.ToList();
		}

		public void CalculateSlides()
		{
			simpleLabelItemSlideCount.Text = String.Format("<color=gray>Slide Output Count: {0}</color>",
				xtraTabControlGroups.TabPages
				.OfType<PreviewGroupControl>()
				.SelectMany(groupControl => groupControl.OutputItems)
				.Sum(outputItem => outputItem.SlidesCount));
		}

		private void LoadPreviewGroup(PreviewGroupControl previewControl)
		{
			previewControl.LoadPreviewControl();
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			xtraTabControlGroups.TabPages
				.Where(tabPage => tabPage != xtraTabControlGroups.SelectedTabPage)
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = false);
			Application.DoEvents();

			LoadPreviewGroup((PreviewGroupControl)xtraTabControlGroups.SelectedTabPage);

			xtraTabControlGroups.TabPages
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = true);
			Application.DoEvents();
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (var groupControl in xtraTabControlGroups.TabPages.OfType<PreviewGroupControl>().ToList())
				groupControl.ClearPreviewImages();
		}

		private void OnSelectedPreviewItemChanged(object sender, TabPageChangedEventArgs e)
		{
			if (!(e.Page is PreviewGroupControl previewControl)) return;

			xtraTabControlGroups.TabPages
				.Where(tabPage => tabPage != e.Page)
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = false);
			Application.DoEvents();

			previewControl.LoadPreviewControl();

			xtraTabControlGroups.TabPages
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = true);
			Application.DoEvents();
		}

		private void OnPreviewItemChanged(Object sender, PreviewItemChangedEventArgs e)
		{
			_allowHandleButtonEvents = false;
			buttonXEnableOutput.Checked = e.OutputItem.Enabled;
			buttonXDisableOutput.Checked = !e.OutputItem.Enabled;
			_allowHandleButtonEvents = true;
		}

		private void OnOutputButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXEnableOutput.Checked = false;
			buttonXDisableOutput.Checked = false;
			button.Checked = true;
		}

		private void OnOutputButtonCheckedChanged(object sender, EventArgs e)
		{
			if (!_allowHandleButtonEvents) return;
			var button = (ButtonX)sender;
			if (!button.Checked) return;
			SelectedGroupControl.SelectedPreviewControl.IsEnabled = buttonXEnableOutput.Checked;
			CalculateSlides();
		}
	}
}