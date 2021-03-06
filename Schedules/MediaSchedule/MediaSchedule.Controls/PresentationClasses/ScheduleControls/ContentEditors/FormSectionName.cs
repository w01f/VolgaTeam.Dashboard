﻿using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public partial class FormSectionName : MetroForm
	{
		public string SectionName
		{
			get
			{
				return textEditName.EditValue?.ToString();
			}
			set
			{
				textEditName.EditValue = value;
			}
		}

		public FormSectionName()
		{
			InitializeComponent();

			pictureEditLogo.Image = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleNewPopupLogo ?? pictureEditLogo.Image;

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormSnapshotName_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (!String.IsNullOrEmpty(textEditName.EditValue as String)) return;
			e.Cancel = true;
			PopupMessageHelper.Instance.ShowWarning("You should set Snapshot name before continue");
		}
	}
}