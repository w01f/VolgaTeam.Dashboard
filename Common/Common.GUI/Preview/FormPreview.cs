using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraBars;
using Asa.Common.GUI.ToolForms;

namespace Asa.Common.GUI.Preview
{
	public partial class FormPreview : MetroForm
	{
		private readonly PowerPointProcessor _powerPointProcessor;
		private readonly Form _parentForm;
		private readonly Action<Action, Action> _showFloater;
		private readonly Func<Action, bool> _checkPowerPoint;

		public List<PreviewGroupControl> GroupControls { get; }

		public FormPreview(
			Form parentForm,
			PowerPointProcessor powerPointProcessor,
			Action<Action, Action> showFloater,
			Func<Action, bool> checkPowerPoint)
		{
			InitializeComponent();
			_parentForm = parentForm;
			_powerPointProcessor = powerPointProcessor;
			_showFloater = showFloater;
			_checkPowerPoint = checkPowerPoint;
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

			if (_powerPointProcessor.IsLinkedWithApplication)
			{
				FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
				FormProgress.ShowOutputProgress();
				_showFloater(() =>
				{
					foreach (var previewGroup in GroupControls.Select(gc => gc.PreviewGroup))
						_powerPointProcessor.AppendSlidesFromFile(previewGroup.PresentationSourcePath, previewGroup.InsertOnTop);
					FormProgress.CloseProgress();
				}, null);
			}
			else
				_checkPowerPoint(null);
			DialogResult = DialogResult.OK;
		}
		#endregion
	}
}