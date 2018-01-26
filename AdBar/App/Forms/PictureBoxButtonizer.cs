using System.Windows.Forms;

namespace Asa.Bar.App.Forms
{
	public static class PictureBoxButtonizer
	{
		public static void Buttonize(this PictureBox pictureBox)
		{
			pictureBox.MouseDown += (o, e) =>
			{
				var pic = (PictureBox)(o);
				pic.Top += 1;
			};

			pictureBox.MouseUp += (o, e) =>
			{
				var pic = (PictureBox)(o);
				pic.Top -= 1;
			};
		}
	}
}
