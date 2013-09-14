using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.MiniBar.ToolForms;
using vbAccelerator.Components.Shell;

namespace NewBizWiz.MiniBar.BusinessClasses
{
	public class NBWApplicationsManager
	{
		private static readonly NBWApplicationsManager _instance = new NBWApplicationsManager();

		private NBWApplicationsManager()
		{
			NBWApplications = new List<NBWApplication>();
			var nbwApplicationsRoot = new DirectoryInfo(SettingsManager.Instance.NBWApplicationsRootPath);
			if (nbwApplicationsRoot.Exists)
			{
				foreach (var nbwApplicationRoot in nbwApplicationsRoot.GetDirectories())
				{
					var nbwApplication = new NBWApplication(nbwApplicationRoot);
					if (!nbwApplication.IsConfigured) continue;
					nbwApplication.OnRun += (o, e) =>
					{
						var executablePath = nbwApplication.Executable;
						if (File.Exists(executablePath))
						{
							var allowAccess = true;
							if (!string.IsNullOrEmpty(nbwApplication.AccessCode))
							{
								allowAccess = false;
								using (var form = new FormAppCode())
								{
									var result = DialogResult.OK;
									while (result == DialogResult.OK)
									{
										result = form.ShowDialog();

										if (result != DialogResult.OK) continue;
										if (form.Code.Equals(nbwApplication.AccessCode))
										{
											allowAccess = true;
											break;
										}
										AppManager.Instance.ShowWarning("Incorrect Access Code.\nTry again");
									}
								}
							}
							if (allowAccess)
								Process.Start(executablePath);
						}
						ServiceDataManager.Instance.WriteActivity();
					};
					nbwApplication.OnCreateShorcut += (o, e) =>
					{
						var executablePath = nbwApplication.Executable;
						if (!File.Exists(executablePath)) return;
						var allowAccess = true;
						if (!string.IsNullOrEmpty(nbwApplication.AccessCode))
						{
							allowAccess = false;
							using (var form = new FormAppCode())
							{
								var result = DialogResult.OK;
								while (result == DialogResult.OK)
								{
									result = form.ShowDialog();
									if (result != DialogResult.OK) continue;
									if (form.Code.Equals(nbwApplication.AccessCode))
									{
										allowAccess = true;
										break;
									}
									AppManager.Instance.ShowWarning("Incorrect Access Code.\nTry again");
								}
							}
						}
						if (!allowAccess) return;
						using (var shortcut = new ShellLink())
						{
							shortcut.Target = nbwApplication.Executable;
							shortcut.WorkingDirectory = Path.GetDirectoryName(executablePath);
							shortcut.Description = nbwApplication.Title.Replace("\n", " ").Replace("\r", string.Empty);
							shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
							if (File.Exists(nbwApplication.Icon))
								shortcut.IconPath = nbwApplication.Icon;
							shortcut.IconIndex = 0;
							shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nbwApplication.Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
						}
					};
					NBWApplications.Add(nbwApplication);
				}
				NBWApplications.Sort((x, y) => x.Order.CompareTo(y.Order));
			}
		}

		public List<NBWApplication> NBWApplications { get; set; }

		public static NBWApplicationsManager Instance
		{
			get { return _instance; }
		}
	}
}