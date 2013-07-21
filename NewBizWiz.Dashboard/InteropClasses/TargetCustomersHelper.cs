﻿using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Dashboard.TabHomeForms;
using NewBizWiz.Dashboard.ToolForms;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.Dashboard.InteropClasses
{
	public partial class DashboardPowerPointHelper
	{
		public void AppendTargetCustomers()
		{
			if (Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TargetCustomersFolder))
			{
				string presentationTemplatePath = Path.Combine(MasterWizardManager.Instance.SelectedWizard.TargetCustomersFolder, string.Format(MasterWizardManager.TargetCustomersSlideTemplate, 1));
				if (File.Exists(presentationTemplatePath))
				{
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
								foreach (Slide slide in presentation.Slides)
								{
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "HEADER":
													shape.TextFrame.TextRange.Text = TargetCustomersControl.Instance.Title;
													break;
												case "TEXTBOX0":
													shape.TextFrame.TextRange.Text = TargetCustomersControl.Instance.TargetDemo;
													break;
												case "TEXTBOX1":
													shape.TextFrame.TextRange.Text = TargetCustomersControl.Instance.HHI;
													break;
												case "TEXTBOX2":
													shape.TextFrame.TextRange.Text = TargetCustomersControl.Instance.Geography;
													break;
											}
										}
									}
								}
								AppendSlide(presentation, -1);
								presentation.Close();
							});
							thread.Start();

							form.Show();

							while (thread.IsAlive)
								Application.DoEvents();
							form.Close();
						}
						using (var form = new FormSlideOutput())
						{
							if (form.ShowDialog() != DialogResult.OK)
								AppManager.Instance.ActivateMainForm();
						}
					}
					catch {}
					finally
					{
						MessageFilter.Revoke();
					}
				}
			}
		}
	}
}