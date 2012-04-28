using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
    public partial class LogoControl : UserControl
    {
        private bool _alowToSave = false;
        private BusinessClasses.CalendarDay _day = null;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesChanged;

        public LogoControl()
        {
            InitializeComponent();
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            _day = day;
            _alowToSave = false;

            buttonXLogos.Checked = _day.Logo.SmallImage != null;
            pbSelectedLogo.Image = _day.Logo.SmallImage;
            pbSelectedLogo.Tag = _day.Logo;
            gridControlImageGallery.DataSource = BusinessClasses.ListManager.Instance.Images;
            _alowToSave = true;
        }

        public void SaveData()
        {
            if (_day != null)
            {
                if (pbSelectedLogo.Tag != null)
                    _day.Logo = (pbSelectedLogo.Tag as BusinessClasses.ImageSource).Clone(_day);
                else
                    _day.Logo = new BusinessClasses.ImageSource(_day);
            }
        }

        private void checkEditLogos_CheckedChanged(object sender, System.EventArgs e)
        {
            laSelectedLogo.Enabled = buttonXLogos.Checked;
            pbSelectedLogo.Enabled = buttonXLogos.Checked;
            pbSelectedLogo.Tag = buttonXLogos.Checked ? pbSelectedLogo.Tag : null;
            pbSelectedLogo.Image = buttonXLogos.Checked ? pbSelectedLogo.Image : null;
            laAvailableLogos.Enabled = buttonXLogos.Checked;
            gridControlImageGallery.Enabled = buttonXLogos.Checked;
            gridViewImageGallery.OptionsSelection.EnableAppearanceFocusedRow = buttonXLogos.Checked ? gridViewImageGallery.OptionsSelection.EnableAppearanceFocusedRow : false;
        }

        private void gridViewImageGallery_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewImageGallery.FocusedRowHandle >= 0 && _alowToSave)
            {
                gridViewImageGallery.OptionsSelection.EnableAppearanceFocusedRow = true;
                BusinessClasses.ImageSource selectedImageSource = BusinessClasses.ListManager.Instance.Images[gridViewImageGallery.GetDataSourceRowIndex(gridViewImageGallery.FocusedRowHandle)];
                pbSelectedLogo.Image = selectedImageSource.SmallImage;
                pbSelectedLogo.Tag = selectedImageSource;
                if (this.PropertiesChanged != null)
                    this.PropertiesChanged(sender, e);
            }
        }

        private void gridViewImageGallery_Click(object sender, EventArgs e)
        {
            Point pt = gridControlImageGallery.PointToClient(Control.MousePosition);

            if (gridViewImageGallery.CalcHitInfo(pt).RowHandle == gridViewImageGallery.FocusedRowHandle)
                gridViewImageGallery_FocusedRowChanged(null, null);
        }
    }
}
