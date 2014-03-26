using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTab;
using NewBizWiz.CommonGUI.ToolForms;

namespace NewBizWiz.CommonGUI.RateCard
{
	[ToolboxItem(false)]
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
			Text = Path.GetFileNameWithoutExtension(File.FullName).Replace("&", "&&");
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void LoadViewer()
		{
			if (Loaded) return;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Rate Card...";
				form.TopMost = true;
				var thread = new Thread(() => Invoke((MethodInvoker)delegate()
				{
					axAcroPDF.LoadFile(File.FullName);
					axAcroPDF.setView("FitW");
					Loaded = true;
				}));
				form.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}

		public void Email() { }
		#endregion
	}
}