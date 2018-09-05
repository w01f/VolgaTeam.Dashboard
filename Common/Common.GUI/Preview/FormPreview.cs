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

			Left = _parentForm.Left + (_parentForm.Width - Width) / 2;
			Top = _parentForm.Top + (_parentForm.Height - Height) / 2;

			Opacity = 0;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
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

			layoutControlItemOutputToggle.Visibility =
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
			if (xtraTabControlGroups.SelectedTabPage != null)
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
			toggleSwitchOutput.IsOn = e.OutputItem.Enabled;
			_allowHandleButtonEvents = true;
		}

		private void OnOutputToggled(object sender, EventArgs e)
		{
			if (!_allowHandleButtonEvents) return;
			SelectedGroupControl.SelectedPreviewControl.IsEnabled = toggleSwitchOutput.IsOn;
			CalculateSlides();
		}
	}
}