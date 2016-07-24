using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Dashboard.TabHomeForms
{
	public partial class FormSavedStates : MetroForm
	{
		protected string SavedFilesPath = string.Empty;
		protected string SavedTemplatesPath = string.Empty;

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
				StateFile file = null;
				if (xtraTabControl.SelectedTabPage == xtraTabPageFiles)
					file = gridViewFiles.GetFocusedRow() as StateFile;
				else if (xtraTabControl.SelectedTabPage == xtraTabPageTemplates)
					file = gridViewTemplates.GetFocusedRow() as StateFile;
				return file == null ? null : file.FilePath;
			}
		}

		public virtual void InitForm()
		{
			SavedTemplatesPath = Path.Combine(SavedFilesPath, "templates");
		}

		private void LoadFiles()
		{
			gridControlFiles.DataSource = null;
			var files = new List<StateFile>();
			if (Directory.Exists(SavedFilesPath))
			{
				foreach (var filePath in Directory.GetFiles(SavedFilesPath, "*.xml"))
				{
					var file = new StateFile();
					file.FilePath = filePath;
					files.Add(file);
				}
			}
			files.Sort((x, y) => y.LastModified.CompareTo(x.LastModified));
			gridControlFiles.DataSource = files;

			gridControlTemplates.DataSource = null;
			var templates = new List<StateFile>();
			if (Directory.Exists(SavedTemplatesPath))
			{
				foreach (var filePath in Directory.GetFiles(SavedTemplatesPath, "*.xml"))
				{
					var file = new StateFile();
					file.FilePath = filePath;
					templates.Add(file);
				}
			}
			templates.Sort((x, y) => y.LastModified.CompareTo(x.LastModified));
			gridControlTemplates.DataSource = templates;
		}

		private void repositoryItemButtonEditFiles_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var file = gridViewFiles.GetFocusedRow() as StateFile;
			if (file == null) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete this file?") != DialogResult.Yes) return;
			if (File.Exists(file.FilePath))
			{
				try { File.Delete(file.FilePath); }
				catch { }
			}
			gridViewFiles.DeleteSelectedRows();
		}

		private void repositoryItemButtonEditTemplates_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var file = gridViewTemplates.GetFocusedRow() as StateFile;
			if (file == null) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete this file?") != DialogResult.Yes) return;
			if (File.Exists(file.FilePath))
			{
				try { File.Delete(file.FilePath); }
				catch { }
			}
			gridViewTemplates.DeleteSelectedRows();
		}

		private void gridView_RowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks != 2) return;
			var view = sender as GridView;
			if (view == null) return;
			view.FocusedRowHandle = e.RowHandle;
			DialogResult = DialogResult.OK;
			Close();
		}
	}

	public class FormSavedClentGoals : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Needs Analysis Slides";
			laTitle.Text = "Select the Needs Analysis Slide you want to open..";
			SavedFilesPath = ViewSettingsManager.Instance.ClientGoalsState.SaveFolder.LocalPath;
			base.InitForm();
		}
	}

	public class FormSavedCover : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Cover Slides";
			laTitle.Text = "Select the Cover Slide you want to open...";
			SavedFilesPath = ViewSettingsManager.Instance.CoverState.SaveFolder.LocalPath;
			base.InitForm();
		}
	}

	public class FormSavedLeadoffStatement : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Intro Slides";
			laTitle.Text = "Select the Intro Slide you want to open...";
			SavedFilesPath = ViewSettingsManager.Instance.LeadoffStatementState.SaveFolder.LocalPath;
			base.InitForm();
		}
	}

	public class FormSavedSimpleSummary : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Closing Summary Slide";
			laTitle.Text = "Select the Closing Summary Slide you want to open...";
			SavedFilesPath = ViewSettingsManager.Instance.SimpleSummaryState.SaveFolder.LocalPath;
			base.InitForm();
		}
	}

	public class FormSavedTargetCustomers : FormSavedStates
	{
		public override void InitForm()
		{
			Text = "Target Customer Slide";
			laTitle.Text = "Select the Target Customer Slide you want to open...";
			SavedFilesPath = ViewSettingsManager.Instance.TargetCustomersState.SaveFolder.LocalPath;
			base.InitForm();
		}
	}

	public class StateFile
	{
		public string FilePath { get; set; }

		public string Name => Path.GetFileNameWithoutExtension(FilePath);

		public DateTime LastModified => File.GetLastWriteTime(FilePath);
	}
}