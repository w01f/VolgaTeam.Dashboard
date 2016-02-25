using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.OfficeInterops;
using DevExpress.XtraTab;
using Asa.Common.GUI.ToolForms;

namespace Asa.Common.GUI.RateCard
{
	[ToolboxItem(false)]
	public partial class ExcelViewer : XtraTabPage, IRateCardViewer
	{
		#region Properties
		public FileInfo File { get; }
		public bool Loaded { get; private set; }
		#endregion

		public ExcelViewer(FileInfo file)
		{
			InitializeComponent();
			File = file;
			Text = Path.GetFileNameWithoutExtension(File.FullName).Replace("&", "&&");
		}

		#region IFileViewer Methods
		public void ReleaseResources()
		{
			webBrowser.Navigate("about:blank");
		}

		public void LoadViewer()
		{
			if (Loaded) return;
			var thread = new Thread(() => Invoke((MethodInvoker)delegate()
			{
				var excelHelper = new ExcelHelper();
				if (!excelHelper.Connect()) return;
				var g = Guid.NewGuid();
				var newFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, g + ".html");
				excelHelper.ConvertToHtml(File.FullName, newFileName);
				excelHelper.Disconnect();
				webBrowser.Url = new Uri(newFileName);
				Loaded = true;
			}));
			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Page...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			FormProgress.CloseProgress();
		}
		#endregion
	}
}