using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.CommonGUI.Preview
{
	public partial class FormPreview : Form
	{
		private readonly IPowerPointHelper _powerPointHelper;
		private readonly HelpManager _helpManager;
		private readonly Form _parentForm;
		private readonly Action<Action> _showFloater;
		private readonly Action _trackOutput;

		public List<PreviewGroupControl> GroupControls { get; private set; }

		public FormPreview(Form parentForm, IPowerPointHelper powerPointHelper, HelpManager helpManager, Action<Action> showFloater, Action trackOutput = null)
		{
			InitializeComponent();
			_parentForm = parentForm;
			_powerPointHelper = powerPointHelper;
			_helpManager = helpManager;
			_showFloater = showFloater;
			_trackOutput = trackOutput;
			GroupControls = new List<PreviewGroupControl>();
		}

		public void LoadGroups(IEnumerable<PreviewGroup> previewGroups)
		{
			GroupControls.Clear();
			xtraTabControlGroups.TabPages.Clear();
			foreach (var groupControl in previewGroups.Select(previewGroup => new PreviewGroupControl(previewGroup)))
				GroupControls.Add(groupControl);
			xtraTabControlGroups.TabPages.AddRange(GroupControls.ToArray());
			xtraTabControlGroups.ShowTabHeader = GroupControls.Count > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		#region Form GUI Event Habdlers
		private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (var groupControl in GroupControls)
				groupControl.ClearPreviewImages();
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOutput_ItemClick(object sender, ItemClickEventArgs e)
		{
			FormBorderStyle = FormBorderStyle.None;
			Size = new Size(0, 0);

			RegistryHelper.MaximizeMainForm = _parentForm.WindowState == FormWindowState.Maximized;
			RegistryHelper.MainFormHandle = _parentForm.Handle;
			if (_trackOutput != null)
				_trackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				_showFloater(() =>
				{
					foreach (var previewGroup in GroupControls.Select(gc => gc.PreviewGroup))
						_powerPointHelper.AppendSlidesFromFile(previewGroup.PresentationSourcePath);
					formProgress.Close();
				});
			}
			DialogResult = DialogResult.OK;
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			_helpManager.OpenHelpLink("preview");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}
		#endregion
	}
}