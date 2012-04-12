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
        private bool _containsPageNumbers = false;

        private void SearchPageNumbers()
        {
            if (_activePresentation != null)
            {
                foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                {
                    foreach (PowerPoint.Shape shape in slide.Shapes)
                    {
                        for (int i = 1; i <= shape.Tags.Count && !_containsPageNumbers; i++)
                        {
                            switch (shape.Tags.Name(i))
                            {
                                case "NEWPAGENUMBER":
                                case "PAGE_NUMBER":
                                    _containsPageNumbers = true;
                                    break;
                            }
                        }
                        if (_containsPageNumbers)
                            break;
                    }
                }
            }
        }

        public void AddPageNumbers()
        {
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Page Numbers for your presentation...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (_activePresentation != null)
                        {
                            if (_activePresentation.Slides.Count > 0)
                            {
                                PowerPoint.Shape pageNumberShape = null;
                                PowerPoint.Presentation pageNumberPresentation = null;
                                string pageNumberPath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.PageNumbersFile;
                                if (File.Exists(pageNumberPath))
                                {
                                    pageNumberPresentation = _powerPointObject.Presentations.Open(FileName: pageNumberPath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                    foreach (PowerPoint.Slide slide in pageNumberPresentation.Slides)
                                    {
                                        bool pageNumberFound = false;
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
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
                                    foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                                    {
                                        if (slideIndex > 1)
                                        {
                                            bool pageNumberFound = false;
                                            PowerPoint.Shape oldPageNumberShape = null;
                                            foreach (PowerPoint.Shape shape in slide.Shapes)
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

        public void RemovePageNumbers()
        {
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nDeleting Page Numbers from your presentation...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        MessageFilter.Register();
                        if (_activePresentation != null)
                        {
                            if (_activePresentation.Slides.Count > 0)
                            {
                                foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                                {
                                    bool pageNumberFound = false;
                                    PowerPoint.Shape oldPageNumberShape = null;
                                    foreach (PowerPoint.Shape shape in slide.Shapes)
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
}
