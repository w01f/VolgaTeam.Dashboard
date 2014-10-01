using System;
using DevComponents.DotNetBar.Metro;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.QuickShare.Single
{
	public partial class FormStart : MetroForm
	{
		public FormStart()
		{
			InitializeComponent();
			Text = String.Format(Text, MediaMetaData.Instance.DataTypeString);
			laTitle.Text = String.Format(laTitle.Text, MediaMetaData.Instance.DataTypeString);
		}
	}
}