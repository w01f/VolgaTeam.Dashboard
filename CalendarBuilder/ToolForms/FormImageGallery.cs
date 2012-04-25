using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
    public partial class FormImageGallery : Form
    {
        private List<BusinessClasses.ImageSource> _imageSources = new List<BusinessClasses.ImageSource>();

        public BusinessClasses.ImageSource SelectedSource
        {
            get
            {
                if(gridViewImageGallery.FocusedRowHandle>=0)
                    return _imageSources[gridViewImageGallery.GetFocusedDataSourceRowIndex()];
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
            _imageSources.AddRange(BusinessClasses.ListManager.Instance.Images);
            gridControlImageGallery.DataSource = _imageSources;
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