using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Asa.Common.GUI.Common
{
	public class ImageToggleButton : PictureEdit
	{
		public event EventHandler CheckedChanged;

		private bool _checked;
		public bool Checked
		{
			get => _checked;
			set
			{
				var valueChanged = value != _checked;
				_checked = value;
				BackColor = _checked ?
					SelectedColor ?? ColorTranslator.FromHtml("#C6E2FF") :
					Color.White;
				if (valueChanged)
					OnCheckedChanged();
			}
		}

		public Color? SelectedColor { get; set; }
		public Color? HoverColor { get; set; }

		public ImageToggleButton()
		{
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Properties.AllowFocused = false;
			Properties.BorderStyle = BorderStyles.Default;
			Properties.NullText = " ";
			Properties.PictureAlignment = ContentAlignment.MiddleCenter;
			Properties.SizeMode = PictureSizeMode.Squeeze;
			Properties.ReadOnly = true;
			Properties.ShowMenu = false;
			Cursor = Cursors.Hand;

			MouseHover += OnMouseHover;
			MouseMove += OnMouseHover;
			MouseLeave += OnMouseLeave;

			this.Buttonize();
		}

		protected virtual void OnCheckedChanged()
		{
			CheckedChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnMouseHover(object sender, EventArgs e)
		{
			if (Checked) return;
			BackColor = HoverColor ?? BackColor;
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			if (Checked) return;
			BackColor = Color.White;
		}
	}
}
