using System;
using System.ComponentModel;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class SectionDigitalControl : UserControl
	public partial class SectionDigitalControl : XtraTabPage, ISectionEditorControl, ISectionOutputControl, ISectionItemCollectionControl
	{
		public SectionEditorType EditorType=>SectionEditorType.DigitalSection;

		public SectionDigitalControl()
		{
			InitializeComponent();
			Text = "Digital";
		}

		public void InitControls()
		{
			if ((CreateGraphics()).DpiX > 96)
			{
			}
		}

		public void Release()
		{
		}

		#region Data Methods
		public void LoadData()
		{
		}

		public void SaveData()
		{
		}

		public void AddItem()
		{
		}

		public void DeleteItem()
		{
		}

		public void UpdateGridView()
		{
		}
		#endregion

			#region Output Stuff
		public Boolean ReadyForOutput => false;
		public void GenerateOutput()
		{
			throw new NotImplementedException();
		}

		public PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
