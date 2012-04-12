using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace NewBizWizForm.InteropClasses
{
    public partial class PowerPointHelper
    {
        public void AppendCover(bool firstSlide)
        {
            if (BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CoverFile != null)
            {
                string presentationTemplatePath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CoverFile;
                try
                {
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        form.TopMost = true;
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "DATE_DATA0":
                                                shape.TextFrame.TextRange.Text = TabHomeForms.CoverControl.Instance.PresentationDate;
                                                break;
                                            case "TITLE":
                                                shape.TextFrame.TextRange.Text = TabHomeForms.CoverControl.Instance.Title;
                                                break;
                                            case "BUSINESS_NAME":
                                                shape.TextFrame.TextRange.Text = TabHomeForms.CoverControl.Instance.DecisionMaker;
                                                break;
                                            case "DECISION_MAKER":
                                                shape.TextFrame.TextRange.Text = TabHomeForms.CoverControl.Instance.Advertiser;
                                                break;
                                            case "QUOTE":
                                                shape.TextFrame.TextRange.Text = TabHomeForms.CoverControl.Instance.Quote;
                                                break;
                                            case "SALESPERSON_NAME":
                                                shape.TextFrame.TextRange.Text = TabHomeForms.CoverControl.Instance.SalesRep;
                                                break;
                                        }
                                    }
                                }
                            }
                            AppendSlide(presentation, -1, firstSlide);
                            presentation.Close();
                        }));
                        thread.Start();

                        form.Show();

                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
                        form.Close();
                    }
                    using (ToolForms.FormSlideOutput form = new ToolForms.FormSlideOutput())
                    {
                        if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            AppManager.Instance.ActivateMainForm();
                    }
                }
                catch
                {
                }
                finally
                {
                    MessageFilter.Revoke();
                }
            }
        }

        public void AppendGenericCover(bool firstSlide)
        {
            if (File.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.GenericCoverFile))
            {
                string presentationTemplatePath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.GenericCoverFile;
                try
                {
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        form.TopMost = true;
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            MessageFilter.Register();
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            AppendSlide(presentation, -1, firstSlide);
                            presentation.Close();
                        }));
                        thread.Start();

                        form.Show();

                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
                        form.Close();
                    }
                    using (ToolForms.FormSlideOutput form = new ToolForms.FormSlideOutput())
                    {
                        if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            AppManager.Instance.ActivateMainForm();
                    }
                }
                catch
                {
                }
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
