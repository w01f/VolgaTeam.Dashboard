using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.RateCard
{
	[ToolboxItem(false)]
	public partial class WordViewer : XtraTabPage, IRateCardViewer
	{
		#region Properties
		public FileInfo File { get; private set; }
		public bool Loaded { get; private set; }
		#endregion

		public WordViewer(FileInfo file)
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
			if (!Loaded)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Rate Card...";
					form.TopMost = true;
					var thread = new Thread(delegate()
												{
													Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
																								{
																									var word = new WordHelper();
																									if (word.Connect())
																									{
																										Guid g = Guid.NewGuid();
																										string newFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, g.ToString() + ".html");
																										word.ConvertToHtml(File.FullName, newFileName);
																										word.Disconnect();
																										webBrowser.Url = new Uri(newFileName);
																									}
																									Loaded = true;
																								});
												});
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
		}

		public void Email() { }
		#endregion
	}
}