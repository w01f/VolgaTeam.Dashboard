using System.IO;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.MiniBar.InteropClasses
{
	public partial class MinibarPowerPointHelper
	{
		public void AddPageNumbers()
		{
			try
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Page Numbers for your presentation...";
					form.TopMost = true;
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						if (_activePresentation != null)
						{
							if (_activePresentation.Slides.Count > 0)
							{
								Shape pageNumberShape = null;
								Presentation pageNumberPresentation = null;
								string pageNumberPath = MasterWizardManager.Instance.SelectedWizard.PageNumbersFile;
								if (File.Exists(pageNumberPath))
								{
									pageNumberPresentation = _powerPointObject.Presentations.Open(FileName: pageNumberPath, WithWindow: MsoTriState.msoFalse);
									foreach (Slide slide in pageNumberPresentation.Slides)
									{
										bool pageNumberFound = false;
										foreach (Shape shape in slide.Shapes)
										{
											for (int i = 1; i <= shape.Tags.Count && !pageNumberFound; i++)
											{
												switch (shape.Tags.Name(i))
												{
													case "NEWPAGENUMBER":
														pageNumberShape = shape;
														pageNumberFound = true;
														break;
												}
											}
											if (pageNumberFound)
												break;
										}
									}
								}

								if (pageNumberShape != null)
								{
									int slideIndex = 1;
									foreach (Slide slide in _activePresentation.Slides)
									{
										if (slideIndex > 1)
										{
											bool pageNumberFound = false;
											Shape oldPageNumberShape = null;
											foreach (Shape shape in slide.Shapes)
											{
												for (int i = 1; i <= shape.Tags.Count && !pageNumberFound; i++)
												{
													switch (shape.Tags.Name(i))
													{
														case "NEWPAGENUMBER":
														case "PAGE_NUMBER":
															oldPageNumberShape = shape;
															pageNumberFound = true;
															break;
													}
												}
												if (pageNumberFound)
													break;
											}
											if (oldPageNumberShape != null)
												oldPageNumberShape.Delete();
											pageNumberShape.TextFrame.TextRange.Text = slideIndex.ToString();
											pageNumberShape.Copy();
											slide.Shapes.Paste();
										}
										slideIndex++;
									}
									_containsPageNumbers = true;
								}
								if (pageNumberPresentation != null)
									pageNumberPresentation.Close();
							}
						}
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

		public void RemovePageNumbers()
		{
			try
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Chill-Out for a few seconds...\nDeleting Page Numbers from your presentation...";
					form.TopMost = true;
					var thread = new Thread(delegate()
					{
						MessageFilter.Register();
						if (_activePresentation != null)
						{
							if (_activePresentation.Slides.Count > 0)
							{
								foreach (Slide slide in _activePresentation.Slides)
								{
									bool pageNumberFound = false;
									Shape oldPageNumberShape = null;
									foreach (Shape shape in slide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count && !pageNumberFound; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "NEWPAGENUMBER":
												case "PAGE_NUMBER":
													oldPageNumberShape = shape;
													pageNumberFound = true;
													break;
											}
										}
										if (pageNumberFound)
											break;
									}
									if (oldPageNumberShape != null)
										oldPageNumberShape.Delete();
								}
							}
						}
						_containsPageNumbers = false;
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
	}
}