using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.DayProperties
{
    public partial class DigitalPropertiesControl : UserControl
    {
        private bool _alowToSave = false;
        private BusinessClasses.CalendarDay _day = null;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesChanged;

        public DigitalPropertiesControl()
        {
            InitializeComponent();

            memoEditCustomNote.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditCustomNote.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditCustomNote.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditProduct.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditProduct.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditProduct.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            _day = day;

            _alowToSave = false;
            buttonXCustomNote.Checked = !string.IsNullOrEmpty(_day.Digital.CustomNote);
            memoEditCustomNote.EditValue = !string.IsNullOrEmpty(_day.Digital.CustomNote) ? _day.Digital.CustomNote : null;

            comboBoxEditCategory.Properties.Items.Clear();
            comboBoxEditCategory.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineCategories.Select(x => ("Web: " + x.Name)).ToArray());
            comboBoxEditCategory.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.MobileCategories.Select(x => ("Mobile: " + x.Name)).ToArray());
            comboBoxEditCategory.EditValue = !string.IsNullOrEmpty(_day.Digital.Category) ? _day.Digital.Category : null;

            UpdateSubCategory(comboBoxEditCategory.EditValue != null ? comboBoxEditCategory.EditValue.ToString() : string.Empty);
            comboBoxEditSubCategory.EditValue = !string.IsNullOrEmpty(_day.Digital.SubCategory) ? _day.Digital.SubCategory : null;

            UpdateProduct(comboBoxEditCategory.EditValue != null ? comboBoxEditCategory.EditValue.ToString() : string.Empty, comboBoxEditSubCategory.EditValue != null ? comboBoxEditSubCategory.EditValue.ToString() : string.Empty);
            comboBoxEditProduct.EditValue = !string.IsNullOrEmpty(_day.Digital.ProductName) ? _day.Digital.ProductName : null;

            buttonXInventory.Checked = !string.IsNullOrEmpty(_day.Digital.Category) || !string.IsNullOrEmpty(_day.Digital.SubCategory) || !string.IsNullOrEmpty(_day.Digital.ProductName);

            checkEditShowCategory.Checked = _day.Digital.ShowCategory;
            checkEditShowSubCategory.Checked = _day.Digital.ShowSubCategory;
            checkEditShowProduct.Checked = _day.Digital.ShowProduct;
            _alowToSave = true;
        }

        public void SaveData()
        {
            _day.Digital.CustomNote = buttonXCustomNote.Checked && memoEditCustomNote.EditValue != null ? memoEditCustomNote.EditValue.ToString() : null;
            _day.Digital.Category = buttonXInventory.Checked && comboBoxEditCategory.EditValue != null ? comboBoxEditCategory.EditValue.ToString() : null;
            _day.Digital.SubCategory = buttonXInventory.Checked && comboBoxEditSubCategory.EditValue != null ? comboBoxEditSubCategory.EditValue.ToString() : null;
            _day.Digital.ProductName = buttonXInventory.Checked && comboBoxEditProduct.EditValue != null ? comboBoxEditProduct.EditValue.ToString() : null;
            _day.Digital.ShowCategory = checkEditShowCategory.Checked;
            _day.Digital.ShowSubCategory = checkEditShowSubCategory.Checked;
            _day.Digital.ShowProduct = checkEditShowProduct.Checked;
        }

        private void UpdateSubCategory(string category)
        {
            comboBoxEditSubCategory.Properties.Items.Clear();
            comboBoxEditSubCategory.EditValue = null;
            if (category.Contains("Web: "))
            {
                category = category.Replace("Web: ", string.Empty);
                comboBoxEditSubCategory.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
            }
            else if (category.Contains("Mobile: "))
            {
                category = category.Replace("Mobile: ", string.Empty);
                comboBoxEditSubCategory.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && !string.IsNullOrEmpty(x.SubCategory)).Select(x => x.SubCategory).Distinct().ToArray());
            }
            comboBoxEditSubCategory.Enabled = comboBoxEditSubCategory.Properties.Items.Count > 0;
            checkEditShowSubCategory.Enabled = comboBoxEditSubCategory.Properties.Items.Count > 0;
        }

        private void UpdateProduct(string category, string subCategory)
        {
            comboBoxEditProduct.Properties.Items.Clear();
            comboBoxEditProduct.EditValue = null;
            if (category.Contains("Web: "))
            {
                category = category.Replace("Web: ", string.Empty);
                comboBoxEditProduct.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OnlineSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
            }
            else if (category.Contains("Mobile: "))
            {
                category = category.Replace("Mobile: ", string.Empty);
                comboBoxEditProduct.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.MobileSources.Where(x => x.Category.Name.Equals(category) && (x.SubCategory.Equals(subCategory) || string.IsNullOrEmpty(subCategory))).Select(x => x.Name).Distinct().ToArray());
            }
            comboBoxEditProduct.Enabled = comboBoxEditProduct.Properties.Items.Count > 0;
            checkEditShowProduct.Enabled = comboBoxEditProduct.Properties.Items.Count > 0;
        }

        private void checkEditUseCustomNote_CheckedChanged(object sender, System.EventArgs e)
        {
            memoEditCustomNote.Enabled = buttonXCustomNote.Checked;
            memoEditCustomNote.EditValue = buttonXCustomNote.Checked ? memoEditCustomNote.EditValue : null;
        }

        private void editor_EditValueChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
                if (this.PropertiesChanged != null)
                    this.PropertiesChanged(sender, e);
        }

        private void checkEditQuickList_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditQuickList.Enabled = buttonXQuickList.Checked;
            comboBoxEditQuickList.EditValue = buttonXQuickList.Checked ? comboBoxEditQuickList.EditValue : null;
        }

        private void checkEditInventory_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditCategory.Enabled = buttonXInventory.Checked;
            checkEditShowCategory.Enabled = buttonXInventory.Checked;
            comboBoxEditSubCategory.Enabled = buttonXInventory.Checked && comboBoxEditSubCategory.Properties.Items.Count > 0;
            checkEditShowSubCategory.Enabled = buttonXInventory.Checked && comboBoxEditSubCategory.Properties.Items.Count > 0;
            comboBoxEditProduct.Enabled = buttonXInventory.Checked && comboBoxEditProduct.Properties.Items.Count > 0;
            checkEditShowProduct.Enabled = buttonXInventory.Checked && comboBoxEditProduct.Properties.Items.Count > 0;
            comboBoxEditCategory.EditValue = buttonXInventory.Checked ? comboBoxEditCategory.EditValue : null;
            comboBoxEditSubCategory.EditValue = buttonXInventory.Checked ? comboBoxEditSubCategory.EditValue : null;
            comboBoxEditProduct.EditValue = buttonXInventory.Checked ? comboBoxEditProduct.EditValue : null;
        }

        private void comboBoxEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
            {
                UpdateSubCategory(comboBoxEditCategory.EditValue != null ? comboBoxEditCategory.EditValue.ToString() : string.Empty);
                UpdateProduct(comboBoxEditCategory.EditValue != null ? comboBoxEditCategory.EditValue.ToString() : string.Empty, comboBoxEditSubCategory.EditValue != null ? comboBoxEditSubCategory.EditValue.ToString() : string.Empty);
                editor_EditValueChanged(sender, e);
            }
        }

        private void comboBoxEditSubCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (_alowToSave)
            {
                UpdateProduct(comboBoxEditCategory.EditValue != null ? comboBoxEditCategory.EditValue.ToString() : string.Empty, comboBoxEditSubCategory.EditValue != null ? comboBoxEditSubCategory.EditValue.ToString() : string.Empty);
                editor_EditValueChanged(sender, e);
            }
        }
    }
}
