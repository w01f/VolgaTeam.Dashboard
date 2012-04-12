using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
    public partial class FormImageGallery : Form
    {
        private List<BusinessClasses.PrintSource> _publicationSources = new List<BusinessClasses.PrintSource>();

        public BusinessClasses.PrintSource SelectedSource
        {
            get
            {
                if(gridViewImageGallery.FocusedRowHandle>=0)
                    return _publicationSources[gridViewImageGallery.GetFocusedDataSourceRowIndex()];
                else
                    return null;
            }
        }

        public FormImageGallery()
        {
            InitializeComponent();
        }

        private void FormImageGallery_Load(object sender, System.EventArgs e)
        {
            _publicationSources.AddRange(BusinessClasses.ListManager.Instance.PrintSources.Where(x => x.BigLogo != null && x.SmallLogo != null /*&& x.TinyLogo != null*/).GroupBy(x => x.BigLogoFileName).Select(x => x.First()));
            gridControlImageGallery.DataSource = _publicationSources;
        }

        private void gridViewImageGallery_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}