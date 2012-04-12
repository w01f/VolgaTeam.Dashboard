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
        public void AppendTargetCustomers()
        {
            if (Directory.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.TargetCustomersFolder))
            {
                string presentationTemplatePath = Path.Combine(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.TargetCustomersFolder, string.Format(BusinessClasses.MasterWizardManager.TargetCustomersSlideTemplate, 1));
                if (File.Exists(presentationTemplatePath))
                {
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
                                                case "HEADER":
                                                    shape.TextFrame.TextRange.Text = TabHomeForms.TargetCustomersControl.Instance.Title;
                                                    break;
                                                case "TEXTBOX0":
                                                    shape.TextFrame.TextRange.Text = TabHomeForms.TargetCustomersControl.Instance.TargetDemo;
                                                    break;
                                                case "TEXTBOX1":
                                                    shape.TextFrame.TextRange.Text = TabHomeForms.TargetCustomersControl.Instance.HHI;
                                                    break;
                                                case "TEXTBOX2":
                                                    shape.TextFrame.TextRange.Text = TabHomeForms.TargetCustomersControl.Instance.Geography;
                                                    break;
                                            }
                                        }
                                    }
                                }
                                AppendSlide(presentation, -1);
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
        }
    }
}
