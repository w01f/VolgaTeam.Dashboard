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
        public void AppendSimpleSummary()
        {
            if (Directory.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder))
            {
                int itemsCount = TabHomeForms.SimpleSummaryControl.Instance.ItemsCount;
                int mainFileTemplateIndex = itemsCount >= 5 ? 5 : itemsCount;

                int additionalFileTemplateIndex = itemsCount > 5 ? itemsCount % 5 : 0;
                if (additionalFileTemplateIndex != 0)
                    additionalFileTemplateIndex += mainFileTemplateIndex;

                int mainFilesCount = itemsCount / 5;
                if (mainFilesCount == 0 && itemsCount > 0)
                    mainFilesCount++;


                string mainPresentationTemplatePath = Path.Combine(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder, string.Format(BusinessClasses.MasterWizardManager.SimpleSummarySlideTemplate, mainFileTemplateIndex));
                string additionalPresentationTemplatePath = Path.Combine(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.SimpleSummaryFolder, string.Format(BusinessClasses.MasterWizardManager.SimpleSummarySlideTemplate, additionalFileTemplateIndex));

                if (File.Exists(mainPresentationTemplatePath))
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
                                PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: mainPresentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                for (int j = 0; j < (itemsCount - additionalFileTemplateIndex); j += mainFileTemplateIndex)
                                {
                                    foreach (PowerPoint.Slide slide in presentation.Slides)
                                    {
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
                                        {
                                            for (int i = 1; i <= shape.Tags.Count; i++)
                                            {
                                                switch (shape.Tags.Name(i))
                                                {
                                                    case "HEADER":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.Title;
                                                        break;
                                                    case "CAMPAIGN":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.CampaignDates))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "STARTENDDATE":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.CampaignDates;
                                                        break;
                                                    case "PREPAREDFOR":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.Advertiser))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "ADVERTISER":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.Advertiser;
                                                        break;
                                                    case "LINECLIENT":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.DecisionMaker))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "DECISIONMAKER":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.DecisionMaker;
                                                        break;
                                                    case "DATE_FORMAT":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.PresentationDate;
                                                        break;
                                                    case "MNTHLY1":
                                                        shape.Visible = TabHomeForms.SimpleSummaryControl.Instance.ShowMonhlyHeader ? Microsoft.Office.Core.MsoTriState.msoTrue : Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "TOTAL2":
                                                        shape.Visible = TabHomeForms.SimpleSummaryControl.Instance.ShowTotalHeader ? Microsoft.Office.Core.MsoTriState.msoTrue : Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "MWH":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.TotalMonthlyValue))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "TOTALMW":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.TotalMonthlyValue;
                                                        break;
                                                    case "MWT":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.TotalTotalValue))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "TOTALINVEST":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.TotalTotalValue;
                                                        break;
                                                    default:
                                                        for (int k = 0; k < mainFileTemplateIndex; k++)
                                                        {
                                                            if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.ItemTitles != null)
                                                                    if ((j + k) < itemsCount)
                                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.ItemTitles[j + k]))
                                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.ItemTitles != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.ItemTitles[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.ItemDetails != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.ItemDetails[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.MonthlyValues != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.MonthlyValues[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.TotalValues != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.TotalValues[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    AppendSlide(presentation, -1);
                                }
                                presentation.Close();
                            }));
                            thread.Start();

                            form.Show();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();
                            form.Close();
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        MessageFilter.Revoke();
                    }

                    if (File.Exists(additionalPresentationTemplatePath))
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
                                    PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: additionalPresentationTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                    foreach (PowerPoint.Slide slide in presentation.Slides)
                                    {
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
                                        {
                                            for (int i = 1; i <= shape.Tags.Count; i++)
                                            {
                                                switch (shape.Tags.Name(i))
                                                {
                                                    case "HEADER":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.Title;
                                                        break;
                                                    case "CAMPAIGN":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.CampaignDates))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "STARTENDDATE":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.CampaignDates;
                                                        break;
                                                    case "PREPAREDFOR":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.Advertiser))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "ADVERTISER":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.Advertiser;
                                                        break;
                                                    case "LINECLIENT":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.DecisionMaker))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "DECISIONMAKER":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.DecisionMaker;
                                                        break;
                                                    case "DATE_FORMAT":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.PresentationDate;
                                                        break;
                                                    case "MWH":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.TotalMonthlyValue))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "TOTALMW":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.TotalMonthlyValue;
                                                        break;
                                                    case "MWT":
                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.TotalTotalValue))
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                        else
                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                        break;
                                                    case "TOTALINVEST":
                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.TotalTotalValue;
                                                        break;
                                                    default:
                                                        int j = mainFileTemplateIndex * mainFilesCount;
                                                        for (int k = 0; k < additionalFileTemplateIndex; k++)
                                                        {
                                                            if (shape.Tags.Name(i).Equals(string.Format("SHAPE{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.ItemTitles != null)
                                                                    if ((j + k) < itemsCount)
                                                                        if (!string.IsNullOrEmpty(TabHomeForms.SimpleSummaryControl.Instance.ItemTitles[j + k]))
                                                                            shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("SUBHEADER{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.ItemTitles != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.ItemTitles[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("SELECT{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.ItemDetails != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.ItemDetails[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("TINVEST{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.MonthlyValues != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.MonthlyValues[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                            else if (shape.Tags.Name(i).Equals(string.Format("MWINVEST{0}", k)))
                                                            {
                                                                shape.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                                                                if (TabHomeForms.SimpleSummaryControl.Instance.TotalValues != null)
                                                                    if ((j + k) < itemsCount)
                                                                    {
                                                                        shape.TextFrame.TextRange.Text = TabHomeForms.SimpleSummaryControl.Instance.TotalValues[j + k];
                                                                        shape.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                                                                    }
                                                            }
                                                        }
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
                using (ToolForms.FormSlideOutput form = new ToolForms.FormSlideOutput())
                {
                    if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        AppManager.Instance.ActivateMainForm();
                }
            }
        }
    }
}


