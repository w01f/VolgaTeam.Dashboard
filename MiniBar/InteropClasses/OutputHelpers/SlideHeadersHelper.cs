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
        public void AddSlideHeaderOnActiveSlide()
        {
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Slide Header for the slide...";
                    form.TopMost = true;
                    MessageFilter.Register();
                    PowerPoint.Shape oldSlideHeaderShape = null;
                    PowerPoint.Shape slideHeaderShape = null;
                    PowerPoint.Presentation slideHeaderPresentation = null;
                    PowerPoint.Slide activeSlide = GetActiveSlide();
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        if (_activePresentation != null)
                        {
                            if (activeSlide != null)
                            {
                                string slideHeaderPath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
                                if (File.Exists(slideHeaderPath))
                                {
                                    slideHeaderPresentation = _powerPointObject.Presentations.Open(FileName: slideHeaderPath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                    foreach (PowerPoint.Slide slide in slideHeaderPresentation.Slides)
                                    {
                                        bool slideHeaderFound = false;
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
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
                                    foreach (PowerPoint.Shape shape in activeSlide.Shapes)
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
                    }));
                    thread.Start();

                    form.Show();

                    while (thread.IsAlive)
                        System.Windows.Forms.Application.DoEvents();

                    form.Close();

                    if (oldSlideHeaderShape != null)
                    {
                        if (AppManager.Instance.ShowWarningQuestion("This slide already has a Slide Header\nDo you still want to replace it?") == System.Windows.Forms.DialogResult.Yes)
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
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }

        public void AddSlideHeaderOnAllSlides()
        {
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating Slide Headers for the presentation...";
                    form.TopMost = true;
                    MessageFilter.Register();
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        if (_activePresentation != null)
                        {
                            if (_activePresentation.Slides.Count > 0)
                            {
                                PowerPoint.Presentation slideHeaderPresentation = null;
                                PowerPoint.Shape slideHeaderShape = null;
                                string slideHeaderPath = BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CleanslateFile;
                                if (File.Exists(slideHeaderPath))
                                {
                                    slideHeaderPresentation = _powerPointObject.Presentations.Open(FileName: slideHeaderPath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                                    foreach (PowerPoint.Slide slide in slideHeaderPresentation.Slides)
                                    {
                                        bool slideHeaderFound = false;
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
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
                                    foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                                    {
                                        PowerPoint.Shape oldSlideHeaderShape = null;
                                        bool slideHeaderFound = false;
                                        foreach (PowerPoint.Shape shape in slide.Shapes)
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

        public void RemoveSlideHeaderFromAllSlides()
        {
            try
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Chill-Out for a few seconds...\nDeleting Slide Headers from the presentation...";
                    form.TopMost = true;
                    MessageFilter.Register();
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        if (_activePresentation != null)
                        {
                            foreach (PowerPoint.Slide slide in _activePresentation.Slides)
                            {
                                PowerPoint.Shape oldSlideHeaderShape = null;
                                bool slideHeaderFound = false;
                                foreach (PowerPoint.Shape shape in slide.Shapes)
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
