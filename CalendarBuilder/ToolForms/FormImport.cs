using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
    public partial class FormImport : Form
    {
        private AdScheduleBuilder.BusinessClasses.ShortSchedule[] _scheduleList = null;

        public AdScheduleBuilder.BusinessClasses.Schedule SelectedSchedule
        {
            get
            {
                return new AdScheduleBuilder.BusinessClasses.Schedule(_scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName);
            }
        }

        public bool BuildAdvanced
        { 
            get
            {
                return checkEditAdvanced.Checked;
            }
        }

        public bool BuildGraphic
        {
            get
            {
                return checkEditGraphic.Checked;
            }
        }

        public bool BuildSimple
        {
            get
            {
                return checkEditSimple.Checked;
            }
        }

        public FormImport()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new System.Drawing.Font(laTitle.Font.Name, laTitle.Font.Size - 5, laTitle.Font.Style);
                laCalendarType.Font = new System.Drawing.Font(laCalendarType.Font.Name, laCalendarType.Font.Size - 3, laCalendarType.Font.Style);
            }
        }

        private void OutsideClick()
        {
            gridViewSchedules.FocusedRowHandle = -1;
            gridViewSchedules.FocusedColumn = gridColumnBusinessName;
            gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = false;
            buttonXImport.Enabled = false;
        }

        private void FormImport_Load(object sender, EventArgs e)
        {
            gridViewSchedules.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewSchedules_FocusedRowChanged);
            OutsideClick();
            _scheduleList = BusinessClasses.ScheduleManager.Instance.AdSchedules.ToArray();
            gridControlSchedules.DataSource = new BindingList<AdScheduleBuilder.BusinessClasses.ShortSchedule>(_scheduleList);
            gridViewSchedules.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewSchedules_FocusedRowChanged);
        }

        private void gridViewSchedules_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = true;
            buttonXImport.Enabled = gridViewSchedules.SelectedRowsCount > 0;
        }

        private void gridViewSchedules_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            gridViewSchedules_FocusedRowChanged(null, null);
            if (e.Clicks == 2)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void Outside_Click(object sender, EventArgs e)
        {
            OutsideClick();
        }
    }
}
