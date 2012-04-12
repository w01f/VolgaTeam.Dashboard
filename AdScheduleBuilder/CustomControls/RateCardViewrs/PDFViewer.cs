using System.IO;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls.RateCardViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PDFViewer : DevExpress.XtraTab.XtraTabPage, BusinessClasses.IRateCardViewer
    {
        #region Properties
        public FileInfo File { get; private set; }
        public bool Loaded { get; private set; }
        #endregion

        public PDFViewer(FileInfo file)
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
            if (!this.Loaded)
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Rate Card...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        FormMain.Instance.Invoke((MethodInvoker)delegate
                        {
                            InteropClasses.ExcelHelper excelHelper = new InteropClasses.ExcelHelper();
                            if (excelHelper.Connect())
                            {
                                axAcroPDF.LoadFile(this.File.FullName);
                                axAcroPDF.setView("FitW");
                                this.Loaded = true;
                            }
                        });
                    }));
                    form.Show();
                    System.Windows.Forms.Application.DoEvents();
                    thread.Start();
                    while (thread.IsAlive)
                        System.Windows.Forms.Application.DoEvents();
                    form.Close();
                }
            }
        }

        public void Email()
        {
        }
        #endregion
    }
}
