using System;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using DevComponents.DotNetBar.Metro;
using Asa.Media.Controls.Properties;

namespace Asa.Media.Controls.ToolForms
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