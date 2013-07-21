using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.MiniBar.BusinessClasses;
using vbAccelerator.Components.Shell;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.MiniBar.SettingsForms
{
	public partial class FormShortcuts : Form
	{
		public FormShortcuts()
		{
			InitializeComponent();
			laDashboard.Text = SettingsManager.Instance.DashboardName;
			if (File.Exists(BusinessClasses.SettingsManager.Instance.DashboardLogoPath) && File.Exists(BusinessClasses.SettingsManager.Instance.DashboardIconPath))
				pbDashboard.Image = new Bitmap(BusinessClasses.SettingsManager.Instance.DashboardLogoPath);
			if (BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ShowLocalButton || !BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ShowLocalButton && !BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ShowWebButton)
			{
				pnLocalSalesDepot.Visible = true;
				laLocalSalesDepot.Text = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalAppName;
				if (File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					pbLocalSalesDepot.Image = new Bitmap(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalLogoPath);
			}
			else
				pnLocalSalesDepot.Visible = false;
			if (BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ShowWebButton)
			{
				pnWebSalesDepot.Visible = true;
				laWebSalesDepot.Text = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebAppName;
				if (File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					pbWebSalesDepot.Image = new Bitmap(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebLogoPath);
			}
			else
				pnWebSalesDepot.Visible = false;
			foreach (NBWApplication application in NBWApplicationsManager.Instance.NBWApplications)
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
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion

		private void pbDashboard_Click(object sender, EventArgs e)
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.Target = BusinessClasses.SettingsManager.Instance.DashboardPath;
				shortcut.WorkingDirectory = Path.GetDirectoryName(BusinessClasses.SettingsManager.Instance.DashboardPath);
				shortcut.Description = SettingsManager.Instance.DashboardName;
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(BusinessClasses.SettingsManager.Instance.DashboardLogoPath) && File.Exists(BusinessClasses.SettingsManager.Instance.DashboardIconPath))
					shortcut.IconPath = BusinessClasses.SettingsManager.Instance.DashboardIconPath;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SettingsManager.Instance.DashboardName + ".lnk"));
			}
		}

		private void pbLocalSalesDepot_Click(object sender, EventArgs e)
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.Target = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ExecutablePath;
				shortcut.WorkingDirectory = Path.GetDirectoryName(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ExecutablePath);
				shortcut.Description = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalAppName;
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalLogoPath) && File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalIconPath))
					shortcut.IconPath = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalIconPath;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), BusinessClasses.SettingsManager.Instance.SalesDepotSettings.LocalAppName + ".lnk"));
			}
		}

		private void pbWebSalesDepot_Click(object sender, EventArgs e)
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.WorkingDirectory = Path.GetDirectoryName(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.ExecutablePath);
				shortcut.Target = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.Url;
				shortcut.Description = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebAppName;
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebLogoPath) && File.Exists(BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebIconPath))
					shortcut.IconPath = BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebIconPath;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), BusinessClasses.SettingsManager.Instance.SalesDepotSettings.WebAppName + ".lnk"));
			}
		}
	}
}