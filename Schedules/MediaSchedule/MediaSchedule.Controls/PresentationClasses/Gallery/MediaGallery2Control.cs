﻿using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Gallery;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Media.Controls.BusinessClasses;

namespace Asa.Media.Controls.PresentationClasses.Gallery
{
	public class MediaGallery2Control : GalleryControl
	{
		public override GalleryManager Manager
		{
			get { return BusinessObjects.Instance.Gallery2Manager; }
		}

		public override RibbonPanel Panel
		{
			get { return Controller.Instance.Gallery2Panel; }
		}

		public override RibbonBar BrowseBar
		{
			get { return Controller.Instance.Gallery2BrowseBar; }
		}

		public override RibbonBar ImageBar
		{
			get { return Controller.Instance.Gallery2ImageBar; }
		}

		public override RibbonBar ZoomBar
		{
			get { return Controller.Instance.Gallery2ZoomBar; }
		}

		public override RibbonBar CopyBar
		{
			get { return Controller.Instance.Gallery2CopyBar; }
		}

		public override ItemContainer BrowseModeContainer
		{
			get { return Controller.Instance.Gallery2BrowseModeContainer; }
		}

		public override ButtonItem ViewMode
		{
			get { return Controller.Instance.Gallery2View; }
		}

		public override ButtonItem EditMode
		{
			get { return Controller.Instance.Gallery2Edit; }
		}

		public override ButtonItem ImageSelect
		{
			get { return Controller.Instance.Gallery2ImageSelect; }
		}

		public override ButtonItem ImageCrop
		{
			get { return Controller.Instance.Gallery2ImageCrop; }
		}

		public override ButtonItem ZoomIn
		{
			get { return Controller.Instance.Gallery2ZoomIn; }
		}

		public override ButtonItem ZoomOut
		{
			get { return Controller.Instance.Gallery2ZoomOut; }
		}

		public override ButtonItem Copy
		{
			get { return Controller.Instance.Gallery2Copy; }
		}

		public override ComboBoxEdit SectionsList
		{
			get { return Controller.Instance.Gallery2Sections; }
		}

		public override ComboBoxEdit GroupsList
		{
			get { return Controller.Instance.Gallery2Groups; }
		}

		public override string Identifier
		{
			get { return ContentIdentifiers.Gallery2; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabGallery2; }
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("gallery2");
		}
	}
}
