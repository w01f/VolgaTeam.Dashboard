using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class DigitalListEditorControl : UserControl
	public partial class DigitalListEditorControl : XtraTabPage, IDigitalEditor, IDigitalItemCollectionEditor
	{
		private bool _needToReload;
		private readonly DigitalEditorsContainer _container;
		public DigitalSectionType SectionType => DigitalSectionType.List;
		public string HelpTag => "homedg";
		public bool HasItems => _container.EditedContent.DigitalProducts.Any();
		public event EventHandler<DataChangedEventArgs> DataChanged;

		public DigitalListEditorControl(DigitalEditorsContainer container)
		{
			InitializeComponent();
			Text = "Digital Startegy";
			_container = container;
		}

		public void LoadData()
		{
			if (!_needToReload) return;

			digitalProductListControl.UpdateData(
					_container.EditedContent,
					_container.EditedContent.ScheduleSettings,
					() =>
					{
						UpdateProductsCount();
						DataChanged?.Invoke(this, new DataChangedEventArgs { ChangedSectionType = SectionType });
					}
				);
			UpdateProductsCount();
			digitalProductListControl.UpdateView();

			_needToReload = false;
		}

		public void RequestReload()
		{
			_needToReload = true;
		}

		public void SaveData()
		{
			digitalProductListControl.ApplyChanges();
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs e)
		{
			if (e.ChangedSettingsType != DigitalSettingsType.ProductList) return;
			digitalProductListControl.UpdateView();
		}

		private void UpdateProductsCount()
		{
			var title = Business.Online.Dictionaries.ListManager.Instance.DefaultControlsConfiguration.SectionsListTitle ?? "Digital Strategy";
			Text = String.Format("{0}  ({1})", title, _container.EditedContent.DigitalProducts.Count);
		}

		public void AddItem(object sender)
		{
			var category = (Category)((ButtonItem)sender).Tag;
			digitalProductListControl.AddProduct(category);
		}

		public void CloneItem()
		{
			digitalProductListControl.CloneProduct();
		}

		public void DeleteItem()
		{
			digitalProductListControl.DeleteProduct();
		}
	}
}
