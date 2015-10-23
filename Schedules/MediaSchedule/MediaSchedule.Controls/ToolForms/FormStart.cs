using System;
using DevComponents.DotNetBar.Metro;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.Properties;

namespace Asa.MediaSchedule.Controls.ToolForms
{
	public partial class FormStart : MetroForm
	{
		public FormStart()
		{
			InitializeComponent();
			Text = String.Format(Text, MediaMetaData.Instance.DataTypeString);
			pbLogo.Image = MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? Resources.TVRibbonLogo : Resources.RadioRibbonLogo;
		}
	}
}