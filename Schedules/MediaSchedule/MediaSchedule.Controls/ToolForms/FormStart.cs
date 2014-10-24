using System;
using DevComponents.DotNetBar.Metro;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.Properties;

namespace NewBizWiz.MediaSchedule.Controls.ToolForms
{
	public partial class FormStart : MetroForm
	{
		public FormStart()
		{
			InitializeComponent();
			Text = String.Format(Text, MediaMetaData.Instance.DataTypeString);
			pbLogo.Image = MediaMetaData.Instance.DataType == MediaDataType.TV ? Resources.TVRibbonLogo : Resources.RadioRibbonLogo;
		}
	}
}