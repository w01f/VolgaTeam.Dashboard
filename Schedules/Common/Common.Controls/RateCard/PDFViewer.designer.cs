﻿namespace Asa.Schedules.Common.Controls.RateCard
{
    partial class PDFViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.pdfViewerControl = new DevExpress.XtraPdfViewer.PdfViewer();
			this.SuspendLayout();
			// 
			// pdfViewerControl
			// 
			this.pdfViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pdfViewerControl.Location = new System.Drawing.Point(0, 0);
			this.pdfViewerControl.Name = "pdfViewerControl";
			this.pdfViewerControl.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
			this.pdfViewerControl.Size = new System.Drawing.Size(407, 332);
			this.pdfViewerControl.TabIndex = 0;
			this.pdfViewerControl.DoubleClick += new System.EventHandler(this.pdfViewerControl_DoubleClick);
			this.pdfViewerControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pdfViewerControl_MouseMove);
			// 
			// PDFViewer
			// 
			this.Controls.Add(this.pdfViewerControl);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(407, 332);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraPdfViewer.PdfViewer pdfViewerControl;


	}
}
