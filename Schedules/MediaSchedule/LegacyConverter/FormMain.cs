using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;

namespace Asa.Media.LegacyConverter
{
	public partial class FormMain : MetroForm
	{
		private readonly ConvertManager _convertManager = new ConvertManager();

		public FormMain()
		{
			InitializeComponent();
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				buttonXConvert.Font = new Font(buttonXConvert.Font.FontFamily, buttonXConvert.Font.Size - 2, buttonXConvert.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
			}
		}

		#region GUI Event handlers
		private void OnFormLoad(object sender, EventArgs e)
		{
			PopupMessageHelper.Instance.Title = Text;
			FormProgress.Init(this);
			_convertManager.Init();
		}

		private void OnSourceButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = buttonEditSource.EditValue as String;
				if (dialog.ShowDialog() != DialogResult.OK) return;
				buttonEditSource.EditValue = dialog.SelectedPath;
			}
		}

		private void OnConvertClick(object sender, EventArgs e)
		{
			var sourcePath = buttonEditSource.EditValue as String;
			if (String.IsNullOrEmpty(sourcePath) || !Directory.Exists(sourcePath))
			{
				PopupMessageHelper.Instance.ShowWarning("Soure path is not correct");
				return;
			}
			var result = false;
			Application.DoEvents();
			FormProgress.ShowProgress("Converting Data...", () =>
			{
				result = _convertManager.RunConversion(sourcePath);
			});
			if (result)
				PopupMessageHelper.Instance.ShowInformation("Conversion Successfully Complited");
		}

		private void OnCloseClick(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}
