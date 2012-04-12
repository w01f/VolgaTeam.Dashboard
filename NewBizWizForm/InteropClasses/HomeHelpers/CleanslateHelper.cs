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
        public void AppendCleanslate()
        {
            if (File.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile))
            {
                string presentationTemplatePath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
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

        public void AppendCleanslateSilent()
        {
            if (File.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile))
            {
                string presentationTemplatePath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
                try
                {
                    MessageFilter.Register();
                    PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: presentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                    AppendSlide(presentation, -1);
                    presentation.Close();

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
