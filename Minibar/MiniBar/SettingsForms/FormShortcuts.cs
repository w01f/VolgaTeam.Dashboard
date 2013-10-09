using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.MiniBar.BusinessClasses;
using vbAccelerator.Components.Shell;
using SettingsManager = NewBizWiz.MiniBar.BusinessClasses.SettingsManager;

namespace NewBizWiz.MiniBar.SettingsForms
{
	public partial class FormShortcuts : Form
	{
		public FormShortcuts()
		{
			InitializeComponent();
			if (SettingsManager.Instance.SalesDepotSettings.ShowLocalButton || !SettingsManager.Instance.SalesDepotSettings.ShowLocalButton && !SettingsManager.Instance.SalesDepotSettings.ShowWebButton)
			{
				pnLocalSalesDepot.Visible = true;
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					pbLocalSalesDepot.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath);
			}
			else
				pnLocalSalesDepot.Visible = false;
			if (SettingsManager.Instance.SalesDepotSettings.ShowWebButton)
			{
				pnWebSalesDepot.Visible = true;
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					pbWebSalesDepot.Image = new Bitmap(SettingsManager.Instance.SalesDepotSettings.WebLogoPath);
			}
			else
				pnWebSalesDepot.Visible = false;
			foreach (var application in NBWApplicationsManager.Instance.Links)
			{
				var definition = new ApplicationDefinitionControl();
				definition.Application = application;
				definition.UpdateView();
				xtraScrollableControl.Controls.Add(definition);
				definition.BringToFront();
			}
		}

		#region Picture Box Clicks Habdlers

		/// <summary>
		///     Buttonize the PictureBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox) (sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox) (sender);
			pic.Top -= 1;
		}

		#endregion

		private void pbLocalSalesDepot_Click(object sender, EventArgs e)
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.Target = SettingsManager.Instance.SalesDepotSettings.ExecutablePath;
				shortcut.WorkingDirectory = Path.GetDirectoryName(SettingsManager.Instance.SalesDepotSettings.ExecutablePath);
				shortcut.Description = SettingsManager.Instance.SalesDepotSettings.LocalAppName;
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					shortcut.IconPath = SettingsManager.Instance.SalesDepotSettings.LocalIconPath;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SettingsManager.Instance.SalesDepotSettings.LocalAppName + ".lnk"));
			}
		}

		private void pbWebSalesDepot_Click(object sender, EventArgs e)
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.WorkingDirectory = Path.GetDirectoryName(SettingsManager.Instance.SalesDepotSettings.ExecutablePath);
				shortcut.Target = SettingsManager.Instance.SalesDepotSettings.Url;
				shortcut.Description = SettingsManager.Instance.SalesDepotSettings.WebAppName;
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					shortcut.IconPath = SettingsManager.Instance.SalesDepotSettings.WebIconPath;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SettingsManager.Instance.SalesDepotSettings.WebAppName + ".lnk"));
			}
		}
	}
}