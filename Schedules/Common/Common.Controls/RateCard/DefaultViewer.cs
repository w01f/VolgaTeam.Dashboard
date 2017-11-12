using System.ComponentModel;
using System.IO;
using Asa.Common.GUI.RateCard;
using DevExpress.XtraTab;

namespace Asa.Schedules.Common.Controls.RateCard
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
			Text = Path.GetFileNameWithoutExtension(File.FullName);
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