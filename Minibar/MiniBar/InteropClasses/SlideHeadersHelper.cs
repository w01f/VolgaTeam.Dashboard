using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MiniBar.ToolForms;
using Application = System.Windows.Forms.Application;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace NewBizWiz.MiniBar.InteropClasses
{
	public partial class MinibarPowerPointHelper
	{
		public void AddSlideHeaderOnActiveSlide()
		{
			try
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Slide Header for the slide...";
					form.TopMost = true;
					MessageFilter.Register();
					Shape oldSlideHeaderShape = null;
					Shape slideHeaderShape = null;
					Presentation slideHeaderPresentation = null;
					Slide activeSlide = GetActiveSlide();
					var thread = new Thread(delegate()
					{
						if (_activePresentation != null)
						{
							if (activeSlide != null)
							{
								string slideHeaderPath = MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
								if (File.Exists(slideHeaderPath))
								{
									slideHeaderPresentation = _powerPointObject.Presentations.Open(FileName: slideHeaderPath, WithWindow: MsoTriState.msoFalse);
									foreach (Slide slide in slideHeaderPresentation.Slides)
									{
										bool slideHeaderFound = false;
										foreach (Shape shape in slide.Shapes)
										{
											for (int i = 1; i <= shape.Tags.Count && !slideHeaderFound; i++)
											{
												switch (shape.Tags.Name(i))
												{
													case "HEADER":
														slideHeaderShape = shape;
														slideHeaderFound = true;
														break;
												}
											}
											if (slideHeaderFound)
												break;
										}
									}
								}

								if (slideHeaderShape != null)
								{
									bool slideHeaderFound = false;
									foreach (Shape shape in activeSlide.Shapes)
									{
										for (int i = 1; i <= shape.Tags.Count && !slideHeaderFound; i++)
										{
											switch (shape.Tags.Name(i))
											{
												case "HEADER":
													oldSlideHeaderShape = shape;
													slideHeaderFound = true;
													break;
											}
										}
										if (slideHeaderFound)
											break;
									}
								}
							}
						}
					});
					thread.Start();

					form.Show();

					while (thread.IsAlive)
						Application.DoEvents();

					form.Close();

					if (oldSlideHeaderShape != null)
					{
						if (AppManager.Instance.ShowWarningQuestion("This slide already has a Slide Header\nDo you still want to replace it?") == DialogResult.Yes)
						{
							oldSlideHeaderShape.Delete();
							slideHeaderShape.Copy();
							activeSlide.Shapes.Paste();
						}
					}
					else
					{
						slideHeaderShape.Copy();
						activeSlide.Shapes.Paste();
					}

					if (slideHeaderPresentation != null)
						slideHeaderPresentation.Close();
				}
			}
			catch {}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void AddSlideHeaderOnAllSlides()
		{
			try
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Slide Headers for the presentation...";
					form.TopMost = true;
					MessageFilter.Register();
					var thread = new Thread(delegate()
					{
						if (_activePresentation != null)
						{
							if (_activePresentation.Slides.Count > 0)
							{
								Presentation slideHeaderPresentation = null;
								Shape slideHeaderShape = null;
								string slideHeaderPath = MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
								if (File.Exists(slideHeaderPath))
								{
									slideHeaderPresentation = _powerPointObject.Presentations.Open(FileName: slideHeaderPath, WithWindow: MsoTriState.msoFalse);
									foreach (Slide slide in slideHeaderPresentation.Slides)
									{
										bool slideHeaderFound = false;
										foreach (Shape shape in slide.Shapes)
										{
											for (int i = 1; i <= shape.Tags.Count && !slideHeaderFound; i++)
											{
												switch (shape.Tags.Name(i))
												{
													case "HEADER":
														slideHeaderShape = shape;
														slideHeaderFound = true;
														break;
												}
											}
											if (slideHeaderFound)
												break;
										}
									}
								}

								if (slideHeaderShape != null)
								{
									foreach (Slide slide in _activePresentation.Slides)
									{
										Shape oldSlideHeaderShape = null;
										bool slideHeaderFound = false;
										foreach (Shape shape in slide.Shapes)
										{
											for (int i = 1; i <= shape.Tags.Count && !slideHeaderFound; i++)
											{
												switch (shape.Tags.Name(i))
												{
													case "HEADER":
														oldSlideHeaderShape = shape;
														slideHeaderFound = true;
														break;
												}
											}
											if (slideHeaderFound)
												break;
										}
										if (oldSlideHeaderShape != null)
										{
											oldSlideHeaderShape.Delete();
											slideHeaderShape.Copy();
											slide.Shapes.Paste();
										}
									}
								}
								if (slideHeaderPresentation != null)
									slideHeaderPresentation.Close();
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

		public void RemoveSlideHeaderFromAllSlides()
		{
			try
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Chill-Out for a few seconds...\nDeleting Slide Headers from the presentation...";
					form.TopMost = true;
					MessageFilter.Register();
					var thread = new Thread(delegate()
					{
						if (_activePresentation != null)
						{
							foreach (Slide slide in _activePresentation.Slides)
							{
								Shape oldSlideHeaderShape = null;
								bool slideHeaderFound = false;
								foreach (Shape shape in slide.Shapes)
								{
									for (int i = 1; i <= shape.Tags.Count && !slideHeaderFound; i++)
									{
										switch (shape.Tags.Name(i))
										{
											case "HEADER":
												oldSlideHeaderShape = shape;
												slideHeaderFound = true;
												break;
										}
									}
									if (slideHeaderFound)
										break;
								}
								if (oldSlideHeaderShape != null)
									oldSlideHeaderShape.Delete();
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
	}
}