﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using DevExpress.XtraTab;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	//public partial class DashboardSlideControl : UserControl
	public partial class DashboardSlideControl : XtraTabPage
	{
		protected BaseDashboardContainer SlideContainer { get; }

		public virtual SlideType SlideType { get; }
		public virtual string SlideName { get; }
		public virtual bool ReadyForOutput { get; }

		public DashboardSlideControl()
		{
			InitializeComponent();
		}

		protected DashboardSlideControl(BaseDashboardContainer slideContainer)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SlideContainer = slideContainer;
			comboBoxEditSlideHeader.EnableSelectAll();
			if (CreateGraphics().DpiX > 96)
			{
			}
			OnSplashResize(this, EventArgs.Empty);
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}

		public virtual void GenerateOutput()
		{
			throw new NotImplementedException();
		}

		public virtual PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
		}

		private void OnSplashResize(object sender, EventArgs e)
		{
			var splashWidth = pnSplash.Width;
			pbSplash.Visible = splashWidth >= 411;
		}
	}
}
