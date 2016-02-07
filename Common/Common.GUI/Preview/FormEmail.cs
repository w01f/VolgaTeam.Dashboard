using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraBars;
using Asa.Common.GUI.ToolForms;

namespace Asa.Common.GUI.Preview
{
	public partial class FormEmail : MetroForm
	{
		private readonly IPowerPointHelper _powerPointHelper;
		private readonly HelpManager _helpManager;

		public List<PreviewGroupControl> GroupControls { get; private set; }

		private PreviewGroup _mergedGroup;

		public FormEmail(IPowerPointHelper powerPointHelper, HelpManager helpManager)
		{
			InitializeComponent();
			_powerPointHelper = powerPointHelper;
			_helpManager = helpManager;
			GroupControls = new List<PreviewGroupControl>();
		}

		public void LoadGroups(IEnumerable<PreviewGroup> previewGroups)
		{
			GroupControls.Clear();
			xtraTabControlGroups.TabPages.Clear();
			foreach (var groupControl in previewGroups.Select(previewGroup => new PreviewGroupControl(previewGroup)))
				GroupControls.Add(groupControl);
			xtraTabControlGroups.TabPages.AddRange(GroupControls.ToArray());
			if (GroupControls.Count > 1)
			{
				xtraTabControlGroups.ShowTabHeader = DefaultBoolean.True;
				_mergedGroup = new PreviewGroup
				{
					Name = "Merged Slides",
					PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};

				FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading slides...");
				FormProgress.ShowProgress();
				var thread = new Thread(() => _powerPointHelper.MergeFiles(_mergedGroup.PresentationSourcePath, previewGroups.Select(pg => pg.PresentationSourcePath).ToArray()));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				FormProgress.CloseProgress();
			}
			else
			{
				xtraTabControlGroups.ShowTabHeader = DefaultBoolean.False;
				_mergedGroup = previewGroups.FirstOrDefault();
			}
		}

		#region Form GUI Event Habdlers
		private void FormEmail_Shown(object sender, System.EventArgs e)
		{
			GroupControls.ForEach(gc => gc.Load());
		}

		private void FormQuickView_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (var groupControl in GroupControls)
				groupControl.ClearPreviewImages();
			_mergedGroup.ClearAssets();
		}
		#endregion

		#region Button Clicks
		private void barButtonItemRegularEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormEmailFileName())
			{
				RegistryHelper.MainFormHandle = form.Handle;
				if (form.ShowDialog() == DialogResult.OK)
				{
					var emailFile = Path.Combine(Path.GetFullPath(_mergedGroup.PresentationSourcePath).Replace(Path.GetFileName(_mergedGroup.PresentationSourcePath), string.Empty), form.FileName + ".pptx");
					try
					{
						File.Copy(_mergedGroup.PresentationSourcePath, emailFile, true);
						if (OutlookHelper.Instance.Open())
						{
							OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
							OutlookHelper.Instance.Close();
						}
						else
							PopupMessageHelper.Instance.ShowWarning("Cannot open Outlook");
						File.Delete(emailFile);
					}
					catch { }
				}
				RegistryHelper.MainFormHandle = Handle;
			}
		}

		private void barLargeButtonItemPDFEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			using (var form = new FormEmailFileName())
			{
				RegistryHelper.MainFormHandle = form.Handle;
				if (form.ShowDialog() == DialogResult.OK)
				{
					var emailFile = Path.Combine(Path.GetFullPath(_mergedGroup.PresentationSourcePath).Replace(Path.GetFileName(_mergedGroup.PresentationSourcePath), string.Empty), form.FileName + ".pdf");
					try
					{
						_powerPointHelper.ConvertToPDF(_mergedGroup.PresentationSourcePath, emailFile);
						if (File.Exists(emailFile))
						{
							if (OutlookHelper.Instance.Open())
							{
								OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
								OutlookHelper.Instance.Close();
							}
							else
								PopupMessageHelper.Instance.ShowWarning("Cannot open Outlook");
							File.Delete(emailFile);
						}
					}
					catch { }
				}
				RegistryHelper.MainFormHandle = Handle;
			}
		}

		private void barLargeButtonItemHelp_ItemClick(object sender, ItemClickEventArgs e)
		{
			_helpManager.OpenHelpLink("email");
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}
		#endregion
	}
}