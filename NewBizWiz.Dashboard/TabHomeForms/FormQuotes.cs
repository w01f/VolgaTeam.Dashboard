using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.Core.Dashboard;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace Asa.Dashboard.TabHomeForms
{
	public partial class FormQuotes : MetroForm
	{
		public FormQuotes()
		{
			InitializeComponent();
		}

		public Quote SelectedQuote
		{
			get
			{
				if (checkedListBoxControlQuotes.CheckedItems.Count > 0)
					return (Quote)checkedListBoxControlQuotes.CheckedItems[0];
				else
					return null;
			}
		}

		private void FormQuotes_Load(object sender, EventArgs e)
		{
			checkedListBoxControlQuotes.Items.Clear();
			foreach (Quote quote in ListManager.Instance.CoverLists.Quotes)
			{
				checkedListBoxControlQuotes.Items.Add(quote, "\"" + quote.Text + "\"", CheckState.Unchecked, true);
				checkedListBoxControlQuotes.Items.Add(quote, quote.Author, CheckState.Unchecked, false);
				checkedListBoxControlQuotes.Items.Add(quote, "", CheckState.Unchecked, false);
			}
			laTitle.Focus();
		}

		private void checkedListBoxControlQuotes_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.State == CheckState.Checked)
			{
				foreach (CheckedListBoxItem item in checkedListBoxControlQuotes.Items)
				{
					if (item.Value != checkedListBoxControlQuotes.Items[e.Index].Value && item.CheckState == CheckState.Checked)
					{
						item.CheckState = CheckState.Unchecked;
					}
				}
			}
		}

		private void checkedListBoxControlQuotes_DrawItem(object sender, ListBoxDrawItemEventArgs e)
		{
			var control = (CheckedListBoxControl)sender;
			if ((e.Index + 2) % 3 == 0)
			{
				using (Brush backBrush = new SolidBrush(control.BackColor))
				using (Brush foreBrush = new SolidBrush(Color.Black))
				using (var font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Regular))
				{
					e.Cache.FillRectangle(backBrush, e.Bounds);
					e.Cache.DrawString("      " + ((Quote)e.Item).Author + "\"", font, foreBrush, e.Bounds, e.Appearance.TextOptions.GetStringFormat());
				}
				e.Handled = true;
			}
			else if ((e.Index + 1) % 3 == 0)
			{
				using (Brush backBrush = new SolidBrush(control.BackColor))
				using (Brush foreBrush = new SolidBrush(Color.Black))
				using (var font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Regular))
				{
					e.Cache.FillRectangle(backBrush, e.Bounds);
					e.Cache.DrawString(" ", font, foreBrush, e.Bounds, e.Appearance.TextOptions.GetStringFormat());
				}
				e.Handled = true;
			}
			else if (checkedListBoxControlQuotes.GetItemChecked(e.Index))
			{
				e.Appearance.BackColor = control.BackColor;
				e.Appearance.ForeColor = Color.Red;
			}
			else
			{
				e.Appearance.BackColor = control.BackColor;
				e.Appearance.ForeColor = control.ForeColor;
			}
		}

		private void checkedListBoxControlQuotes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (checkedListBoxControlQuotes.SelectedIndex % 3 == 0)
			{
				checkedListBoxControlQuotes.SelectedIndex += 1;
			}
		}
	}
}