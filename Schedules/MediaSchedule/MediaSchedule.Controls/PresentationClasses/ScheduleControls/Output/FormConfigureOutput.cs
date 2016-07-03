using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public partial class FormConfigureOutput : MetroForm
	{
		private readonly Dictionary<ScheduleSectionOutputType, CheckedListBoxItem> _outputOptionItems =
			new Dictionary<ScheduleSectionOutputType, CheckedListBoxItem>();

		public FormConfigureOutput(string scheduleName, IEnumerable<ScheduleSectionOutputType> outputOptionItems)
		{
			InitializeComponent();

			foreach (var optionItem in outputOptionItems.OrderBy(v => v))
			{
				var itemName = String.Empty;
				switch (optionItem)
				{
					case ScheduleSectionOutputType.Program:
						itemName = String.Format("{0} ({1})", scheduleName, MediaMetaData.Instance.DataTypeString);
						break;
					case ScheduleSectionOutputType.DigitalOneSheet:
						itemName = String.Format("{0} (Digital)", scheduleName);
						break;
					case ScheduleSectionOutputType.ProgramAndDigital:
						itemName = String.Format("{0} ({1} + Digital)", scheduleName, MediaMetaData.Instance.DataTypeString);
						break;
					case ScheduleSectionOutputType.Summary:
						itemName = String.Format("{0} (Summary)", scheduleName);
						break;
					case ScheduleSectionOutputType.DigitalStrategy:
						itemName = String.Format("{0} (Digital Strategies)", scheduleName);
						break;
				}
				var checkItem = new CheckedListBoxItem(optionItem, itemName, CheckState.Checked);
				checkedListBoxControlOutputOptionItems.Items.Add(checkItem);
				_outputOptionItems.Add(optionItem, checkItem);
			}
		}

		public IEnumerable<ScheduleSectionOutputType> GetSelectedOutputTypes()
		{
			return checkedListBoxControlOutputOptionItems.CheckedItems
				.OfType<CheckedListBoxItem>()
				.Select(selectedOption => (ScheduleSectionOutputType)selectedOption.Value)
				.OrderBy(selectedOption => selectedOption)
				.ToList();
		}

		private void checkedListBoxControlProducts_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			buttonXContinue.Enabled = checkedListBoxControlOutputOptionItems.CheckedItemsCount > 0;
		}
	}
}