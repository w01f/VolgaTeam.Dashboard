﻿using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;

namespace Asa.Dashboard.ToolForms
{
	public partial class FormSaveTemplate : MetroForm
	{
		public FormSaveTemplate()
		{
			InitializeComponent();
		}

		public string TemplateName
		{
			get
			{
				if (textEditTemplateName.EditValue != null)
					return textEditTemplateName.EditValue.ToString();
				return null;
			}
		}

		private void textEditTemplateName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormSaveTemplate_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (!String.IsNullOrEmpty(TemplateName)) return;
			PopupMessageHelper.Instance.ShowWarning("Template Name can't be empty");
			e.Cancel = true;
		}
	}
}