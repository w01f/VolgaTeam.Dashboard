using System;
using System.Diagnostics;
using System.IO;
using DevComponents.DotNetBar.Metro;

namespace AdSalesBrowser
{
	public partial class FormVideoDownloadComplete : MetroForm
	{
		private readonly string _filePath;

		public FormVideoDownloadComplete(string filePath)
		{
			InitializeComponent();
			_filePath = filePath;
			laTitle.Text = String.Format(laTitle.Text, Path.GetFileName(_filePath));
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
