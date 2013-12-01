using System;
using System.Windows.Forms;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Single
{
	public partial class FormStart : Form
	{
		public FormStart()
		{
			InitializeComponent();
			Text = String.Format(Text, MediaMetaData.Instance.DataTypeString);
			laTitle.Text = String.Format(laTitle.Text, MediaMetaData.Instance.DataTypeString);
		}
	}
}