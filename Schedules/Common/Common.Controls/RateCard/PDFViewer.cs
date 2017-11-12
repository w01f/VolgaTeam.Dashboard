using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Asa.Common.GUI.RateCard;
using DevExpress.XtraTab;

namespace Asa.Schedules.Common.Controls.RateCard
{
	[ToolboxItem(false)]
	//public partial class PDFViewer : UserControl, IRateCardViewer
	public partial class PDFViewer : XtraTabPage, IRateCardViewer
	{
		#region Properties
		public FileInfo File { get; private set; }
		public bool Loaded { get; private set; }
		#endregion

		public PDFViewer(FileInfo file)
		{
			InitializeComponent();
			File = file;
			Text = Path.GetFileNameWithoutExtension(File.FullName);
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void LoadViewer()
		{
			if (Loaded) return;
			pdfViewerControl.LoadDocument(File.FullName);
			Loaded = true;
		}
		#endregion

		private void pdfViewerControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			pdfViewerControl.Focus();
		}

		private void pdfViewerControl_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				Process.Start(File.FullName);
			}
			catch {}
		}
	}
}