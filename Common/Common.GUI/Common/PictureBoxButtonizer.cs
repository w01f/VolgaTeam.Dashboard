using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Asa.Common.GUI.Common
{
	public static class PictureBoxButtonizer
	{
		public static void Buttonize(this PictureBox pictureBox)
		{
			pictureBox.MouseDown += (o, e) =>
			{
				var pic = (PictureBox)o;
				pic.Top += 1;
			};

			pictureBox.MouseUp += (o, e) =>
			{
				var pic = (PictureBox)o;
				pic.Top -= 1;
			};
		}

		public static void Buttonize(this PictureEdit pictureEdit)
		{
			pictureEdit.MouseDown += (o, e) =>
			{
				var pic = (PictureEdit)o;
				pic.Top += 1;
			};

			pictureEdit.MouseUp += (o, e) =>
			{
				var pic = (PictureEdit)o;
				pic.Top -= 1;
			};
		}
	}
}
