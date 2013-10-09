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
			Links = new List<NBWLink>();
			var nbwApplicationsRoot = new DirectoryInfo(SettingsManager.Instance.NBWApplicationsRootPath);
			if (nbwApplicationsRoot.Exists)
			{
				foreach (var nbwApplicationRoot in nbwApplicationsRoot.GetDirectories())
				{
					var nbwLink = NBWLink.CreateLink(nbwApplicationRoot);
					if (nbwLink == null || !nbwLink.IsConfigured) continue;
					nbwLink.OnRun += (o, e) =>
					{
						var allowAccess = true;
						if (!string.IsNullOrEmpty(nbwLink.AccessCode))
						{
							allowAccess = false;
							using (var form = new FormAppCode())
							{
								var result = DialogResult.OK;
								while (result == DialogResult.OK)
								{
									result = form.ShowDialog();

									if (result != DialogResult.OK) continue;
									if (form.Code.Equals(nbwLink.AccessCode))
									{
										allowAccess = true;
										break;
									}
									AppManager.Instance.ShowWarning("Incorrect Access Code.\nTry again");
								}
							}
						}
						if (allowAccess)
							nbwLink.Run();
						ServiceDataManager.Instance.WriteActivity();
					};
					nbwLink.OnCreateShorcut += (o, e) =>
					{
						var allowAccess = true;
						if (!string.IsNullOrEmpty(nbwLink.AccessCode))
						{
							allowAccess = false;
							using (var form = new FormAppCode())
							{
								var result = DialogResult.OK;
								while (result == DialogResult.OK)
								{
									result = form.ShowDialog();
									if (result != DialogResult.OK) continue;
									if (form.Code.Equals(nbwLink.AccessCode))
									{
										allowAccess = true;
										break;
									}
									AppManager.Instance.ShowWarning("Incorrect Access Code.\nTry again");
								}
							}
						}
						if (!allowAccess) return;
						nbwLink.CreateShorcut();
					};
					Links.Add(nbwLink);
				}
				Links.Sort((x, y) => x.TabOrder == y.TabOrder ? x.Order.CompareTo(y.Order) : x.TabOrder.CompareTo(y.TabOrder));
			}
		}

		public List<NBWLink> Links { get; set; }

		public static NBWApplicationsManager Instance
		{
			get { return _instance; }
		}
	}
}