using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace AdSalesAddIn
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            InteropClasses.PowerPointHelper.Instance.Connect(this.Application);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            InteropClasses.PowerPointHelper.Instance.Disconnect();
        }

        private void Application_AfterNewPresentation(PowerPoint.Presentation Pres)
        {
        }

        private void Application_AfterPresentationOpen(PowerPoint.Presentation Pres)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
            this.Application.AfterPresentationOpen += new PowerPoint.EApplication_AfterPresentationOpenEventHandler(Application_AfterPresentationOpen);
            this.Application.AfterNewPresentation += new PowerPoint.EApplication_AfterNewPresentationEventHandler(Application_AfterNewPresentation);
        }
        #endregion
    }
}
