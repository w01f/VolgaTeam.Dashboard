using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.ToolForms;
using DevExpress.Utils;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.Preview
{
	//public partial class PreviewGroupControl : UserControl
	public partial class PreviewGroupControl : XtraTabPage
	{
		private readonly PowerPointProcessor _mainPowerPointProcessor;
		private readonly Form _parentForm;
		private readonly Form _previewForm;

		public OutputGroup OutputGroup { get; }

		public event EventHandler<PreviewItemChangedEventArgs> PreviewItemChanged;

		public PreviewItemControl SelectedPreviewControl => xtraTabControlItems.SelectedTabPage as PreviewItemControl;

		public IList<OutputItem> OutputItems => xtraTabControlItems.TabPages
			.OfType<PreviewItemControl>()
			.Select(previewItemControl => previewItemControl.OutputItem)
			.Where(previewItem => previewItem.Enabled)
			.ToList();

		public PreviewGroupControl(
			OutputGroup outputGroup,
			PowerPointProcessor mainPowerPointProcessor,
			Form parentForm,
			Form previewForm)
		{
			OutputGroup = outputGroup;
			_mainPowerPointProcessor = mainPowerPointProcessor;
			_parentForm = parentForm;
			_previewForm = previewForm;

			InitializeComponent();

			Text = OutputGroup.Name;

			FormProgress.Init(_parentForm);

			xtraTabControlItems.TabPages.AddRange(outputGroup.Items.Select(previewItem => new PreviewItemControl(previewItem)).ToArray());
			xtraTabControlItems.ShowTabHeader = xtraTabControlItems.TabPages.Count > 1 ? DefaultBoolean.True : DefaultBoolean.False;
			xtraTabControlItems.SelectedTabPage = xtraTabControlItems.TabPages
				.OfType<PreviewItemControl>()
				.FirstOrDefault(previewControl => previewControl.OutputItem.IsCurrent);
			xtraTabControlItems.SelectedPageChanged += OnSelectedPreviewItemChanged;
		}

		public void LoadPreviewControl()
		{
			LoadPreviewControl(SelectedPreviewControl);
		}

		public void LoadPreviewControl(PreviewItemControl previewControl)
		{
			if (!(previewControl.IsLoaded || previewControl.OutputItem.PreviewGeneratingAction == null))
			{
				Utilities.ActivateForm(_parentForm.Handle, _parentForm.WindowState == FormWindowState.Maximized, true);
				Utilities.ActivateForm(_previewForm.Handle, _previewForm.WindowState == FormWindowState.Maximized, true);
				FormProgress.ShowProgress("Loading Slide Preview...", () =>
				{
					previewControl.OutputItem.PreviewGeneratingAction(_mainPowerPointProcessor,
						previewControl.OutputItem.PresentationSourcePath);
					if (_previewForm.InvokeRequired)
						_previewForm.BeginInvoke(new MethodInvoker(previewControl.Load));
					else
						previewControl.Load();
				});
				Utilities.ActivateForm(_parentForm.Handle, _parentForm.WindowState == FormWindowState.Maximized, false);
				Utilities.ActivateForm(_previewForm.Handle, _previewForm.WindowState == FormWindowState.Maximized, false);
			}

			PreviewItemChanged?.Invoke(
				this,
				new PreviewItemChangedEventArgs
				{
					OutputItem = previewControl.OutputItem
				});
		}

		public void ClearPreviewImages()
		{
			foreach (var previewItemControl in xtraTabControlItems.TabPages.OfType<PreviewItemControl>().ToList())
				previewItemControl.ClearPreviewImages();
		}

		private void OnSelectedPreviewItemChanged(object sender, TabPageChangedEventArgs e)
		{
			if (!(e.Page is PreviewItemControl previewControl)) return;

			xtraTabControlItems.TabPages
				.Where(tabPage => tabPage != e.Page)
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = false);
			Application.DoEvents();

			LoadPreviewControl(previewControl);

			xtraTabControlItems.TabPages
				.ToList()
				.ForEach(tabPage => tabPage.PageEnabled = true);
			Application.DoEvents();
		}
	}
}
