using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.RateCard
{
	[ToolboxItem(false)]
	public partial class PowerPointViewer : UserControl, IRateCardViewer
	{
		#region Properties
		public FileInfo File { get; private set; }
		public bool Loaded { get; private set; }
		#endregion

		public PowerPointViewer(FileInfo file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;

			laFileInfo.Text = string.Empty;
			laSlideSize.Text = string.Empty;
			pictureBoxPreview.Image = null;
			laSlideNumber.Text = string.Empty;
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void LoadViewer()
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Rate Card...";
				form.TopMost = true;
				var thread = new Thread(delegate()
											{
												Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
																									   {
																										   //if (this.File.PreviewContainer != null)
																										   //{
																										   //    laFileInfo.Text = this.File.PropertiesName + Environment.NewLine + "Added: " + this.File.AddDate.ToString("MM/dd/yy h:mm:ss tt") + Environment.NewLine + (this.File.ExpirationDateOptions.EnableExpirationDate && this.File.ExpirationDateOptions.ExpirationDate != DateTime.MinValue ? ("Expires: " + this.File.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt")) : "No Expiration Date");
																										   //    if (this.File.PresentationProperties != null)
																										   //        laSlideSize.Text = string.Format("{0} {1} x {2}", new object[] { this.File.PresentationProperties.Orientation, this.File.PresentationProperties.Width.ToString("#.##"), this.File.PresentationProperties.Height.ToString("#.##") });
																										   //    comboBoxEditSlides.SelectedIndexChanged -= new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
																										   //    comboBoxEditSlides.Properties.Items.Clear();
																										   //    comboBoxEditSlides.Properties.Items.AddRange(this.File.PreviewContainer.Slides.Select(x => x.Index + 1).ToArray());
																										   //    if (this.File.PreviewContainer.Slides.Count > 0)
																										   //        comboBoxEditSlides.SelectedIndex = 0;
																										   //    comboBoxEditSlides_SelectedIndexChanged(null, null);
																										   //    comboBoxEditSlides.SelectedIndexChanged += new EventHandler(comboBoxEditSlides_SelectedIndexChanged);
																										   //}
																										   Loaded = true;
																									   });
											});
				thread.Start();

				form.Show();

				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}

		public void Email() { }
		#endregion

		#region GUI Event Handlers
		private void comboBoxEditSlides_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (this.File != null)
			//{
			//    if (this.File.PreviewContainer != null)
			//    {
			//        this.File.PreviewContainer.SelectedIndex = comboBoxEditSlides.SelectedIndex;
			//        pictureBoxPreview.Image = this.File.PreviewContainer.SelectedSlide;
			//        laSlideNumber.Text = string.Format("Slide {0} of {1}", new object[] { (this.File.PreviewContainer.SelectedIndex + 1).ToString(), this.File.PreviewContainer.Slides.Count.ToString() });
			//    }
			//}
		}

		private void comboBoxEditSlides_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			//if (this.File != null)
			//{
			//    if (this.File.PreviewContainer != null)
			//    {
			//        if (e.Button.Index == 1)
			//        {
			//            this.File.PreviewContainer.SelectedIndex++;
			//            if (this.File.PreviewContainer.SelectedIndex >= this.File.PreviewContainer.Slides.Count)
			//                this.File.PreviewContainer.SelectedIndex = 0;
			//            comboBoxEditSlides.SelectedIndex = this.File.PreviewContainer.SelectedIndex;
			//        }
			//        else if (e.Button.Index == 2)
			//        {
			//            this.File.PreviewContainer.SelectedIndex--;
			//            if (this.File.PreviewContainer.SelectedIndex < 0)
			//                this.File.PreviewContainer.SelectedIndex = this.File.PreviewContainer.Slides.Count - 1;
			//            comboBoxEditSlides.SelectedIndex = this.File.PreviewContainer.SelectedIndex;
			//        }
			//    }
			//}
		}

		private void pnNavigationArea_Resize(object sender, EventArgs e)
		{
			comboBoxEditSlides.Left = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
			laFileInfo.Width = (pnNavigationArea.Width - comboBoxEditSlides.Width) / 2;
		}
		#endregion
	}
}