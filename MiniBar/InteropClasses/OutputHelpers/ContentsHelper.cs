using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace MiniBar.InteropClasses
{
    public partial class PowerPointHelper
    {
        public Dictionary<int, string> Contents { get; private set; }

        private void CheckOldContents(ref bool contentsExisted, ref bool oldContentsExisted)
        {
            bool contentsFound = false;
            bool oldContentsFound = false;
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nSearching Contents slide on your presentation...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (_activePresentation != null)
                        {
                            foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                            {
                                for (int i = 1; i <= slide.Tags.Count && !contentsFound; i++)
                                    if (slide.Tags.Name(i).ToUpper().Contains("CONTENTS"))
                                        contentsFound = true;
                                if (contentsFound)
                                    break;
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count && !oldContentsFound; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "PAGE_NUMBERS":
                                                oldContentsFound = true;
                                                break;
                                        }
                                    }
                                    if (oldContentsFound)
                                        break;
                                }
                            }
                        }
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
                contentsExisted = contentsFound;
                oldContentsExisted = oldContentsFound;
                MessageFilter.Revoke();
            }
        }

        public void DeleteContents()
        {
            int slideIndex = 0;
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nDeleting Contents slide from your presentation...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (_activePresentation != null)
                        {
                            int j = 1;
                            bool oldContentFound = false;
                            foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                            {
                                for (int i = 1; i <= slide.Tags.Count; i++)
                                    if (slide.Tags.Name(i).ToUpper().Contains("CONTENTS"))
                                    {
                                        slideIndex = j;
                                        break;
                                    }
                                if (slideIndex != 0)
                                    break;
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count && !oldContentFound; i++)
                                    {
                                        switch (shape.Tags.Name(i))
                                        {
                                            case "PAGE_NUMBERS":
                                                oldContentFound = true;
                                                break;
                                        }
                                    }
                                    if (oldContentFound)
                                        break;
                                }
                                if (oldContentFound)
                                {
                                    slideIndex = j;
                                    break;
                                }
                                j++;
                            }
                            if (slideIndex != 0)
                                _activePresentation.Slides[slideIndex].Delete();
                        }
                    }));
                    thread.Start();

                    form.Show();

                    while (thread.IsAlive)
                        System.Windows.Forms.Application.DoEvents();
                    form.Close();

                    if (slideIndex != 0 && _containsPageNumbers)
                        AddPageNumbers();
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

        private void FillContents()
        {
            if (this.Contents.Count == 0)
                return;
            if (this.Contents.Count > 40)
                return;
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Contents slide for your presentation...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        string contentTemplatePath = Path.Combine(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.ContentsFolder, string.Format(BusinessClasses.MasterWizardManager.ContentsFileName, this.Contents.Count.ToString()));
                        if (File.Exists(contentTemplatePath))
                        {
                            PowerPoint.Presentation presentation = _powerPointObject.Presentations.Open(FileName: contentTemplatePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                            foreach (PowerPoint.Slide slide in presentation.Slides)
                            {
                                foreach (PowerPoint.Shape shape in slide.Shapes)
                                {
                                    for (int i = 1; i <= shape.Tags.Count; i++)
                                    {
                                        for (int j = 0; j < this.Contents.Count; j++)
                                        {
                                            if (shape.Tags.Name(i).Equals(string.Format("HEADER{0}", j + 1)))
                                                shape.TextFrame.TextRange.Text = this.Contents.ElementAt(j).Value;
                                            else if (shape.Tags.Name(i).Equals(string.Format("C{0}", j + 1)))
                                                shape.TextFrame.TextRange.Text = this.Contents.ElementAt(j).Key.ToString();
                                        }
                                    }
                                }
                            }
                            AppendSlide(presentation, -1, false, 2);
                            presentation.Close();
                        }
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

        public void AddContents(bool autoUpdate = false)
        {
            this.Contents = new Dictionary<int, string>();
            bool contentsFound = false;
            bool oldContentsFound = false;

            CheckOldContents(ref contentsFound, ref oldContentsFound);

            if (contentsFound && !oldContentsFound && !autoUpdate)
                if (!(AppManager.Instance.ShowWarningQuestion("This presentation already has a Contents Slide.\nDo you want to replace it?") == System.Windows.Forms.DialogResult.Yes))
                    contentsFound = false;
            if (contentsFound)
                DeleteContents();

            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Contents slide for your presentation...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (_activePresentation != null)
                        {
                            if (_activePresentation.Slides.Count > 0)
                            {
                                int slideIndex = 1;
                                foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                                {
                                    if (slideIndex > 1)
                                    {
                                        bool containsSeveralHeader = false;
                                        string headerText = string.Empty;
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
                                        {
                                            for (int i = 1; i <= shape.Tags.Count && !containsSeveralHeader; i++)
                                            {
                                                switch (shape.Tags.Name(i))
                                                {
                                                    case "HEADER":
                                                        if (string.IsNullOrEmpty(headerText))
                                                            headerText = shape.TextFrame.TextRange.Text;
                                                        else
                                                            containsSeveralHeader = true;
                                                        break;
                                                }
                                            }
                                            if (containsSeveralHeader)
                                                break;
                                        }
                                        if (containsSeveralHeader)
                                        {
                                            foreach (PowerPoint.Shape shape in slide.Shapes)
                                                for (int i = 1; i <= shape.Tags.Count && !containsSeveralHeader; i++)
                                                    switch (shape.Tags.Name(i))
                                                    {
                                                        case "HEADER":
                                                            shape.TextFrame.TextRange.Text = string.Empty;
                                                            break;
                                                    }
                                        }
                                        else if (!string.IsNullOrEmpty(headerText) && !this.Contents.Values.Contains(headerText))
                                            this.Contents.Add(slideIndex + 1, headerText);
                                    }
                                    slideIndex++;
                                }
                            }
                        }
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

            FillContents();
            if (this.Contents.Count > 40 && !autoUpdate)
                AppManager.Instance.ShowWarning("This Presentation has too many Slide Headers for the Contents Slide.\n\nThe Contents Slide will be disabled.");
            else if (this.Contents.Count > 40 && autoUpdate)
                AppManager.Instance.ShowWarning("This Presentation has too many Slide Headers for the Contents Slide.\n\nYour Presentation will be updated without a Contents Slide.");
            else if (this.Contents.Count > 0 && _containsPageNumbers && !autoUpdate)
                AddPageNumbers();
            this.Contents.Clear();
        }
    }
}
