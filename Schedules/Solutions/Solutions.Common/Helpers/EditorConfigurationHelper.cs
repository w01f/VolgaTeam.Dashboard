using System;
using System.Drawing;
using Asa.Business.Solutions.Common.Configuration;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Common.Helpers
{
	public class EditorConfigurationHelper
	{
		private readonly BaseEdit _editor;
		private readonly TextEditorConfiguration _configuration;

		public EditorConfigurationHelper(BaseEdit editor, TextEditorConfiguration configuration)
		{
			_editor = editor;
			_configuration = configuration;

			InitEditorStyle();

			_editor.EditValueChanged += OnPlaceholderColorEditValueChanged;
		}

		private void InitEditorStyle()
		{
			if (_configuration.FontSize.HasValue)
			{
				var fontSizeDelta = (Int32)(_configuration.FontSize.Value - Math.Floor(_editor.Properties.Appearance.Font.Size));
				_editor.Properties.Appearance.FontSizeDelta = fontSizeDelta;
				_editor.Properties.AppearanceFocused.FontSizeDelta = fontSizeDelta;
				_editor.Properties.AppearanceDisabled.FontSizeDelta = fontSizeDelta;
				_editor.Properties.AppearanceReadOnly.FontSizeDelta = fontSizeDelta;
				if (_editor is ComboBoxEdit comboEditor)
					comboEditor.Properties.AppearanceDropDown.FontSizeDelta = fontSizeDelta;
			}
			if (!_configuration.BackColor.IsEmpty)
			{
				_editor.Properties.Appearance.BackColor = _configuration.BackColor;
				_editor.Properties.AppearanceFocused.BackColor = _configuration.BackColor;
				_editor.Properties.AppearanceDisabled.BackColor = CommonSkins.GetSkin(UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledControl); ;
			}
			if (!_configuration.ForeColor.IsEmpty)
			{
				_editor.Properties.Appearance.ForeColor = _configuration.ForeColor;
				_editor.Properties.AppearanceFocused.ForeColor = _configuration.ForeColor;
				_editor.Properties.AppearanceDisabled.ForeColor = CommonSkins.GetSkin(UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledText);
			}
			if (!_configuration.DropdownForeColor.IsEmpty)
			{
				if (_editor is ComboBoxEdit comboEditor)
					comboEditor.Properties.AppearanceDropDown.ForeColor = _configuration.DropdownForeColor;
			}

			OnPlaceholderColorEditValueChanged(_editor, EventArgs.Empty);
		}

		private void OnPlaceholderColorEditValueChanged(object sender, EventArgs e)
		{
			if (!(sender is BaseEdit editor)) return;
			if (String.IsNullOrEmpty(editor.EditValue?.ToString()))
				editor.Properties.Appearance.ForeColor = Color.Gray;
			else if (!_configuration.ForeColor.IsEmpty)
				editor.Properties.Appearance.ForeColor = _configuration.ForeColor;
			else
				editor.Properties.Appearance.ForeColor = editor.Properties.AppearanceFocused.ForeColor;
		}
	}

	public static class EditorConfigurationExtensions
	{
		public static void AssignConfiguration(this BaseEdit editor, TextEditorConfiguration configuration)
		{
			new EditorConfigurationHelper(editor, configuration);
		}
	}
}
