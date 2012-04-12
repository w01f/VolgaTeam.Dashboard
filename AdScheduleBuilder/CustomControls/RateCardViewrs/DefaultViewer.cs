using System.IO;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls.RateCardViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DefaultViewer : DevExpress.XtraTab.XtraTabPage, BusinessClasses.IRateCardViewer
    {
        #region Properties
        public bool Loaded { get; private set; }
        public FileInfo File { get; private set; }
        #endregion

        public DefaultViewer(FileInfo file)
        {
            InitializeComponent();
            this.File = file;
            this.Text = Path.GetFileNameWithoutExtension(this.File.FullName).Replace("&", "&&");
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
        }

        public void LoadViewer()
        {
            this.Loaded = true;
        }

        public void Email()
        {
        }
        #endregion
    }
}
