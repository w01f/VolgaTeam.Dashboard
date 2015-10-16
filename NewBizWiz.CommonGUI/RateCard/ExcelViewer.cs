using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.CommonGUI.RateCard
{
	[ToolboxItem(false)]
	public partial class ExcelViewer : XtraTabPage, IRateCardViewer
	{
		#region Properties
		public FileInfo File { get; private set; }
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
				Guid g = Guid.NewGuid();
				string newFileName = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, g + ".html");
				excelHelper.ConvertToHtml(File.FullName, newFileName);
				excelHelper.Disconnect();
				webBrowser.Url = new Uri(newFileName);
				Loaded = true;
			}));
			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Rate Card...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			FormProgress.CloseProgress();
		}

		public void Email() { }
		#endregion
	}
}