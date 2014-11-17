using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NewBizWiz.CommonGUI.Common
{
	public class TabbedCombobox : ComboBoxEdit
	{
		[Browsable(true), DefaultValue(true), Category("Appearance")]
		public bool OverrideTab { get; set; }

		protected override bool IsInputKey(Keys keyData)
		{
			return (keyData == Keys.Tab && OverrideTab) || base.IsInputKey(keyData);
		}

		public TabbedCombobox()
		{
			OverrideTab = true;
		}
	}

	public class TabbedDateEdit : DateEdit
	{
		protected override bool IsInputKey(Keys keyData)
		{
			return keyData == Keys.Tab || base.IsInputKey(keyData);
		}
	}
}
