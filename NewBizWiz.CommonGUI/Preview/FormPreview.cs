using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraBars;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.Interop;

namespace Asa.CommonGUI.Preview
{
	public partial class FormPreview : MetroForm
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

		public bool CheckPowerPointRunning()
		{
			if (_powerPointHelper.IsLinkedWithApplication) return true;
			if (Utilities.Instance.ShowWarningQuestion(String.Format("PowerPoint must be open if you want to build a SellerPoint Schedule.{0}Do you want to open PowerPoint now?", Environment.NewLine)) == DialogResult.Yes)
				_showFloater(() => PowerPointManager.Instance.RunPowerPointLoader());
			return false;
		}

		#region Form GUI Event Habdlers
		private void FormPreview_Shown(object sender, EventArgs e)
		{
			GroupControls.ForEach(gc => gc.Load());
		}

		private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (var groupControl in GroupControls)
				groupControl.ClearPreviewImages();
		}
		#endregion

		#region Button Clicks
		private void barButtonItemOutput_ItemClick(object sender, ItemClickEventArgs e)
		{
			WindowState = FormWindowState.Normal;
			FormBorderStyle = FormBorderStyle.None;
			Size = new Size(0, 0);

			RegistryHelper.MaximizeMainForm = _parentForm.WindowState == FormWindowState.Maximized;
			RegistryHelper.MainFormHandle = _parentForm.Handle;

			if (_powerPointHelper.IsLinkedWithApplication)
			{
				if (_trackOutput != null)
					_trackOutput();

				FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
				FormProgress.ShowProgress();
				_showFloater(() =>
				{
					foreach (var previewGroup in GroupControls.Select(gc => gc.PreviewGroup))
						_powerPointHelper.AppendSlidesFromFile(previewGroup.PresentationSourcePath, previewGroup.InsertOnTop);
					FormProgress.CloseProgress();
				});
			}
			else
				CheckPowerPointRunning();
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