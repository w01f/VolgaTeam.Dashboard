using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.PresentationClasses.Digital.Output;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using Asa.Online.Controls.PresentationClasses.Products;
using Asa.Online.Controls.ToolForms;
using DevExpress.Skins;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
    [ToolboxItem(false)]
    //public partial class DigitalProductEditorControl : UserControl
    public partial class DigitalProductEditorControl : XtraTabPage, IDigitalEditor, IDigitalProductsContainer, IDigitalOutputContainer
    {
        private bool _allowApplyValues;
        private bool _needToReload;
        private readonly DigitalEditorsContainer _container;
        private readonly List<DigitalProductControl> _tabPages = new List<DigitalProductControl>();

        public DigitalSectionType SectionType => DigitalSectionType.Products;
        public SlideType SlideType => SlideType.DigitalProducts;
        public string HelpTag => "digitalsl";
        public IDigitalProductsContent DigitalProductsContent => _container.EditedContent;
        public PowerPointProcessor PowerPointProcessor => BusinessObjects.Instance.PowerPointManager.Processor;
        public event EventHandler<DataChangedEventArgs> DataChanged;

        public DigitalProductEditorControl(DigitalEditorsContainer container)
        {
            InitializeComponent();
            Text = ListManager.Instance.DefaultControlsConfiguration.SectionsProductTitle ?? "Digital Onesheets";
            _container = container;

            spinEditDuration.EnableSelectAll();

            var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
            emptySpaceItemTop.MaxSize = RectangleHelper.ScaleSize(emptySpaceItemTop.MaxSize, scaleFactor);
            emptySpaceItemTop.MinSize = RectangleHelper.ScaleSize(emptySpaceItemTop.MinSize, scaleFactor);
            layoutControlItemFlightDatesToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesToggle.MaxSize, scaleFactor);
            layoutControlItemFlightDatesToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemFlightDatesToggle.MinSize, scaleFactor);
            layoutControlItemDurationToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDurationToggle.MaxSize, scaleFactor);
            layoutControlItemDurationToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDurationToggle.MinSize, scaleFactor);
            layoutControlItemDurationEditor.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDurationEditor.MaxSize, scaleFactor);
            layoutControlItemDurationEditor.MinSize = RectangleHelper.ScaleSize(layoutControlItemDurationEditor.MinSize, scaleFactor);
            layoutControlItemWeeksToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWeeksToggle.MaxSize, scaleFactor);
            layoutControlItemWeeksToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemWeeksToggle.MinSize, scaleFactor);
            layoutControlItemMonthsToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthsToggle.MaxSize, scaleFactor);
            layoutControlItemMonthsToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthsToggle.MinSize, scaleFactor);
        }

        public void LoadData()
        {
            if (!_needToReload) return;

            _allowApplyValues = false;

            checkEditShowFlightDates.Text = String.Format("{0}", _container.EditedContent.ScheduleSettings.FlightDates);

            xtraTabControlProducts.SuspendLayout();
            Application.DoEvents();
            xtraTabControlProducts.SelectedPageChanged -= OnProductsTabControlSelectedPageChanged;
            xtraTabControlProducts.TabPages.OfType<IDigitalProductControl>().ToList().ForEach(c => c.Release());
            xtraTabControlProducts.TabPages.Clear();
            _tabPages.Clear();
            foreach (var product in _container.EditedContent.DigitalProducts.Where(p => !String.IsNullOrEmpty(p.Name)))
            {
                var productTab = new DigitalProductControl(this);
                _tabPages.Add(productTab);
                Application.DoEvents();
                productTab.Product = product;
                productTab.LoadValues();
                Application.DoEvents();
            }
            _tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
            xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());

            Application.DoEvents();
            xtraTabControlProducts.ResumeLayout();
            xtraTabControlProducts.SelectedPageChanged += OnProductsTabControlSelectedPageChanged;

            _allowApplyValues = true;

            LoadProduct(_tabPages.FirstOrDefault());
            Application.DoEvents();

            _needToReload = false;
        }

        public void RequestReload()
        {
            _needToReload = true;
        }

        public void SaveData()
        {
            if (xtraTabControlProducts.SelectedTabPage is IDigitalProductControl)
                SaveProduct((IDigitalProductControl)xtraTabControlProducts.SelectedTabPage);
            foreach (var tabPage in _tabPages)
                tabPage.SaveValues();
        }

        public void UpdateAccordingSettings(SettingsChangedEventArgs e) { }

        public void RaiseDataChanged()
        {
            DataChanged?.Invoke(this, new DataChangedEventArgs { ChangedSectionType = DigitalSectionType.Products });
        }

        #region Records Management
        public void LoadProduct(IDigitalProductControl productControl)
        {
            if (productControl == null) return;

            _allowApplyValues = false;

            var product = productControl.Product;

            simpleLabelItemCategory.Text = product.Category;
            checkEditShowFlightDates.Checked = product.ShowFlightDates;

            checkEditDuration.Checked = product.ShowDuration;
            switch (product.DurationType)
            {
                case "Months":
                    checkEditMonths.Checked = true;
                    checkEditWeeks.Checked = false;
                    break;
                case "Weeks":
                    checkEditWeeks.Checked = true;
                    checkEditMonths.Checked = false;
                    break;
            }
            if (product.DurationValue.HasValue)
            {
                spinEditDuration.EditValue = product.DurationValue;
            }
            else
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = product.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = product.WeeksDuration;
            }
            _allowApplyValues = true;
        }

        private void SaveProduct(IDigitalProductControl productControl)
        {
            if (productControl == null) return;

            productControl.Product.ShowFlightDates = checkEditShowFlightDates.Checked;

            SaveDurationCheckboxValues(productControl);
            productControl.Product.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
        }
        #endregion

        #region Event Handlers
        private void OnProductsTabControlSelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            SaveProduct((IDigitalProductControl)e.PrevPage);
            LoadProduct((DigitalProductControl)e.Page);
        }

        private void OnProductsTabControlMouseDown(object sender, MouseEventArgs e)
        {
            var tabControl = (XtraTabControl)sender;
            var hitInfo = tabControl.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.HitTest != XtraTabHitTest.PageHeader || e.Button != MouseButtons.Right) return;
            var productControl = (DigitalProductControl)hitInfo.Page;
            using (var form = new FormCloneProduct())
            {
                if (form.ShowDialog() != DialogResult.Yes) return;
                var selectedPage = (DigitalProductControl)xtraTabControlProducts.SelectedTabPage;
                var newPrintProduct = productControl.Product.Clone<DigitalProduct, DigitalProduct>();
                xtraTabControlProducts.SelectedPageChanged -= OnProductsTabControlSelectedPageChanged;
                xtraTabControlProducts.TabPages.Clear();
                var newPublicationTab = new DigitalProductControl(this);
                newPublicationTab.Product = newPrintProduct;
                newPublicationTab.Text = newPrintProduct.Name;
                newPublicationTab.LoadValues();
                _tabPages.Add(newPublicationTab);
                _tabPages.Sort((x, y) => x.Product.Index.CompareTo(y.Product.Index));
                xtraTabControlProducts.TabPages.AddRange(_tabPages.ToArray());
                xtraTabControlProducts.SelectedPageChanged += OnProductsTabControlSelectedPageChanged;
                xtraTabControlProducts.SelectedTabPage = selectedPage;
                RaiseDataChanged();
            }
        }

        private void OnDurationCheckedChanged(object sender, EventArgs e)
        {
            spinEditDuration.Enabled = checkEditDuration.Checked;
            checkEditMonths.Enabled = checkEditDuration.Checked;
            checkEditWeeks.Enabled = checkEditDuration.Checked;
            if (!_allowApplyValues) return;
            SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
        }

        private void SaveDurationCheckboxValues(IDigitalProductControl productControl)
        {
            productControl.Product.ShowDuration = checkEditDuration.Checked;
            if (checkEditMonths.Checked)
                productControl.Product.DurationType = "Months";
            else if (checkEditWeeks.Checked)
                productControl.Product.DurationType = "Weeks";
            RaiseDataChanged();
        }

        private void OnMonthsCheckedChanged(object sender, EventArgs e)
        {
            var productControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
            if (productControl == null) return;
            checkEditWeeks.CheckedChanged -= OnWeeksCheckedChanged;
            checkEditWeeks.Checked = !checkEditMonths.Checked;
            if (checkEditMonths.Checked)
                spinEditDuration.EditValue = productControl.Product.MonthDuraton;
            else if (checkEditWeeks.Checked)
                spinEditDuration.EditValue = productControl.Product.WeeksDuration;
            checkEditWeeks.CheckedChanged += OnWeeksCheckedChanged;

            if (!_allowApplyValues) return;
            SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
        }

        private void OnWeeksCheckedChanged(object sender, EventArgs e)
        {
            var productControl = xtraTabControlProducts.SelectedTabPage as DigitalProductControl;
            if (productControl == null) return;
            checkEditMonths.CheckedChanged -= OnMonthsCheckedChanged;
            checkEditMonths.Checked = !checkEditWeeks.Checked;
            if (checkEditMonths.Checked)
                spinEditDuration.EditValue = productControl.Product.MonthDuraton;
            else if (checkEditWeeks.Checked)
                spinEditDuration.EditValue = productControl.Product.WeeksDuration;
            checkEditMonths.CheckedChanged += OnMonthsCheckedChanged;

            if (!_allowApplyValues) return;
            SaveDurationCheckboxValues(xtraTabControlProducts.SelectedTabPage as DigitalProductControl);
        }

        private void OnProductTogleCheckedChanged(object sender, EventArgs e)
        {
            if (!_allowApplyValues) return;
            RaiseDataChanged();
        }

        private void OnDigitalProductContainerResize(object sender, EventArgs e)
        {
            checkEditShowFlightDates.Left = (Width - checkEditShowFlightDates.Width) / 2;
        }
        #endregion

        #region Output
        public Theme SelectedTheme
        {
            get
            {
                var selectedTheme = MediaMetaData.Instance.SettingsManager.GetSelectedThemeName(SlideType);
                return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType).FirstOrDefault(t => t.Name.Equals(selectedTheme) || String.IsNullOrEmpty(selectedTheme));
            }
        }

        public OutputGroup GetOutputGroup()
        {
            LoadData();

            var outputItems = xtraTabControlProducts.TabPages.OfType<IDigitalOutputItem>().Select(productControl => productControl.GeneratePreviewData()).ToList();

            outputItems.ForEach(item => { item.Enabled = item.IsCurrent || BusinessObjects.Instance.OutputManager.DigitalSlideOutputConfiguration.EnablePlanners; });

            return new OutputGroup
            {
                Name = "Planners",
                IsCurrent = TabControl != null && TabControl.SelectedTabPage == this,
                Items = outputItems
            };
        }
        #endregion
    }
}
