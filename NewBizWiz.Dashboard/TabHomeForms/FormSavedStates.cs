using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	public partial class FormSavedStates : Form
	{
		protected string _rootFolderPath = string.Empty;
		protected List<StateFile> _stateFiles = new List<StateFile>();

		public FormSavedStates()
		{
			InitializeComponent();
			InitForm();
			LoadFiles();
		}

		public string SelectedFile
		{
			get
			{
				var file = gridViewFiles.GetFocusedRow() as StateFile;
				return file == null ? null : file.FilePath;
			}
		}

		public virtual void InitForm() { }

		protected void LoadFiles()
		{
			gridControlFiles.DataSource = null;
			_stateFiles.Clear();
			if (Directory.Exists(_rootFolderPath))
			{
				foreach (string filePath in Directory.GetFiles(_rootFolderPath, "*.xml"))
				{
					var file = new StateFile();
					file.FilePath = filePath;
					_stateFiles.Add(file);
				}
			}
			_stateFiles.Sort((x, y) => y.LastModified.CompareTo(x.LastModified));
			gridControlFiles.DataSource = _stateFiles;
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var file = gridViewFiles.GetFocusedRow() as StateFile;
			if (file == null) return;
			if (Utilities.Instance.ShowWarningQuestion("Are you sure want to delete this file?") != DialogResult.Yes) return;
			if (File.Exists(file.FilePath))
			{
				try { File.Delete(file.FilePath); }
				catch { }
			}
			gridViewFiles.DeleteSelectedRows();
		}

		private void gridViewFiles_RowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
			{
				gridViewFiles.FocusedRowHandle = e.RowHandle;
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}

	public class FormSavedClentGoals : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Needs Analysis Slides";
			laTitle.Text = "Select the Needs Analysis Slide you want to open..";
			_rootFolderPath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals");
		}
	}

	public class FormSavedCover : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Cover Slides";
			laTitle.Text = "Select the Cover Slide you want to open...";
			_rootFolderPath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover");
		}
	}

	public class FormSavedLeadoffStatement : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Intro Slides";
			laTitle.Text = "Select the Intro Slide you want to open...";
			_rootFolderPath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro");
		}
	}

	public class FormSavedSimpleSummary : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Closing Summary Slide";
			laTitle.Text = "Select the Closing Summary Slide you want to open...";
			_rootFolderPath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary");
		}
	}

	public class FormSavedTargetCustomers : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Target Customer Slide";
			laTitle.Text = "Select the Target Customer Slide you want to open...";
			_rootFolderPath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target");
		}
	}

	public class StateFile
	{
		public string FilePath { get; set; }

		public string Name
		{
			get { return Path.GetFileNameWithoutExtension(FilePath); }
		}

		public DateTime LastModified
		{
			get { return File.GetLastWriteTime(FilePath); }
		}
	}
}