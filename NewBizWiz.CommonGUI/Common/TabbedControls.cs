using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NewBizWiz.CommonGUI.Common
{
	public class TabbedCombobox : ComboBoxEdit
	{
		protected override bool IsInputKey(Keys keyData)
		{
			return keyData == Keys.Tab || base.IsInputKey(keyData);
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
