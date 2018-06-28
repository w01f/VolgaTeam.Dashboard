using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.GUI.Common;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Asa.Solutions.Common.PresentationClasses
{
	public class SolutionImageToggle : PictureEdit, ISolutionToggle
	{
		public BaseSolutionInfo SolutionInfo { get; }
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
					CheckedChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public Color? SelectedColor { get; set; }
		public Color? HoverColor { get; set; }

		public SolutionImageToggle(BaseSolutionInfo solutionInfo, int buttonWidth)
		{
			SolutionInfo = solutionInfo;

			Int32 imageHeight;
			if (String.Equals(Path.GetExtension(SolutionInfo.ToggleImagePath), ".svg", StringComparison.OrdinalIgnoreCase))
			{
				var svgBitmap = SvgBitmap.FromFile(SolutionInfo.ToggleImagePath);
				imageHeight = (Int32)(svgBitmap.SvgImage.Height * (buttonWidth / svgBitmap.SvgImage.Width));
				Image = svgBitmap.Render(null, 1.0D);
			}
			else
			{
				var pngImage = Image.FromFile(SolutionInfo.ToggleImagePath);
				imageHeight = (Int32)(pngImage.Height * (buttonWidth / pngImage.Width));
				Image = pngImage;
			}

			Height = imageHeight;

			ToolTip = SolutionInfo.ToggleTitle;
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			Properties.AllowFocused = false;
			Properties.BorderStyle = BorderStyles.Default;
			Properties.NullText = " ";
			Properties.PictureAlignment = ContentAlignment.MiddleCenter;
			Properties.SizeMode = PictureSizeMode.Squeeze;
			Properties.ReadOnly = true;
			Properties.ShowMenu = false;
			Cursor = Cursors.Hand;

			Enabled = solutionInfo.Enabled;

			MouseHover += OnMouseHover;
			MouseMove += OnMouseHover;
			MouseLeave += OnMouseLeave;

			this.Buttonize();
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
