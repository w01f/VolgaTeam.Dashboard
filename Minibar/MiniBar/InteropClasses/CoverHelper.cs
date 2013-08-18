﻿using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MiniBar.ToolForms;
using Application = System.Windows.Forms.Application;

namespace NewBizWiz.MiniBar.InteropClasses
{
	public partial class MinibarPowerPointHelper
	{
		public void AppendGenericCover(bool firstSlide)
		{
			if (File.Exists(MasterWizardManager.Instance.SelectedWizard.GenericCoverFile))
			{
				string presentationTemplatePath = MasterWizardManager.Instance.SelectedWizard.GenericCoverFile;
				try
				{
					using (var form = new FormProgress())
					{
						form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
						form.TopMost = true;
						var thread = new Thread(delegate()
						{
							MessageFilter.Register();
							Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: MsoTriState.msoFalse);
							AppendSlide(presentation, -1, null, firstSlide);
							presentation.Close();
						});
						thread.Start();

						form.Show();

						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
				}
				catch {}
				finally
				{
					MessageFilter.Revoke();
				}
			}
			else
				AppManager.Instance.ShowWarning("No Cover Available");
		}
	}
}