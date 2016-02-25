using System.ComponentModel;
using System.IO;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.RateCard
{
	[ToolboxItem(false)]
	public partial class DefaultViewer : XtraTabPage, IRateCardViewer
	{
		#region Properties
		public bool Loaded { get; private set; }
		public FileInfo File { get; private set; }
		#endregion

		public DefaultViewer(FileInfo file)
		{
			InitializeComponent();
			File = file;
			Text = Path.GetFileNameWithoutExtension(File.FullName).Replace("&", "&&");
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void LoadViewer()
		{
			Loaded = true;
		}
		#endregion
	}
}