using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace Asa.Common.GUI.Common
{
	public static class TextEditorsHelper
	{
		private static bool _enter;
		private static bool _needToPerformSelectionMode;

		public static BaseEdit EnableSelectAll(this BaseEdit editor)
		{
			editor.MouseUp += OnMouseUpForSelect;
			editor.MouseDown += OnMouseDown;
			editor.Enter += OnEnterForSelect;
			return editor;
		}

		public static RepositoryItem EnableSelectAll(this RepositoryItem editor)
		{
			editor.MouseUp += OnMouseUpForSelect;
			editor.MouseDown += OnMouseDown;
			editor.Enter += OnEnterForSelect;
			return editor;
		}

		public static TextEdit DisableSelectAll(this TextEdit editor)
		{
			editor.Enter += OnEnterForReset;
			return editor;
		}

		public static BaseEdit RaiseNullValueIfEditorEmpty(this BaseEdit editor)
		{
			editor.EditValueChanged += OnEmptyEditorEditValueChanged;
			return editor;
		}
		
		public static BaseEdit RaiseChangePlaceholderColor(this BaseEdit editor)
		{
			editor.Properties.Appearance.ForeColor = Color.Gray;
			editor.EditValueChanged += OnPlaceholderColorEditValueChanged;
			return editor;
		}

		private static void OnEnterForSelect(object sender, EventArgs e)
		{
			_enter = true;
		}

		private static void OnEnterForReset(object sender, EventArgs e)
		{
			var editor = sender as TextEdit;
			if (!String.IsNullOrEmpty(editor?.Text))
				editor.Select(editor.Text.Length, 0);
		}

		private static void OnMouseUpForSelect(object sender, MouseEventArgs e)
		{
			if (_needToPerformSelectionMode)
				((BaseEdit)sender).SelectAll();
			ResetEnterFlag();
		}

		private static void OnMouseDown(object sender, MouseEventArgs e)
		{
			_needToPerformSelectionMode = _enter;
		}

		private static void ResetEnterFlag()
		{
			_enter = false;
		}

		private static void OnEmptyEditorEditValueChanged(object sender, EventArgs e)
		{
			if (!(sender is BaseEdit editor)) return;
			if (editor.EditValue is String value && String.IsNullOrEmpty(value))
				editor.EditValue = null;
		}

		private static void OnPlaceholderColorEditValueChanged(object sender, EventArgs e)
		{
			if (!(sender is BaseEdit editor)) return;
			if (editor.EditValue == null || editor.EditValue is String value && String.IsNullOrEmpty(value))
				editor.Properties.Appearance.ForeColor = Color.Gray;
			else
				editor.Properties.Appearance.Reset();
		}
	}
}
