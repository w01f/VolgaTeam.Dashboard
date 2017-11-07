using System;
using System.Diagnostics;
using System.IO;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormFileDownloadComplete : MetroForm
	{
		private readonly string _filePath;

		public FormFileDownloadComplete(string filePath)
		{
			InitializeComponent();
			_filePath = filePath;
			simpleLabelItemTitle.Text = String.Format(simpleLabelItemTitle.Text, Path.GetFileName(_filePath));

			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenFile.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenFile.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenFile.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenFile.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenFolder.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenFolder.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenFolder.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenFolder.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void buttonXOpenFile_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_filePath);
			}
			catch{}
		}

		private void buttonXOpenFolder_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(Path.GetDirectoryName(_filePath));
			}
			catch { }
		}
	}
}
