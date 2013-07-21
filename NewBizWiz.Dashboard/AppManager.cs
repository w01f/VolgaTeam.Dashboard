using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard
{
	public class AppManager
	{
		#region Delegates
		public delegate void EmptyParametersDelegate();

		public delegate void SingleParameterDelegate(bool parameter);
		#endregion

		private static readonly AppManager _instance = new AppManager();
		public HelpManager HelpManager { get; private set; }

		private AppManager()
		{
			ShowCover = false;
			HelpManager = new HelpManager(Core.Dashboard.SettingsManager.Instance.HelpLinksPath);
		}

		public bool ShowCover { get; set; }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public static string FormCaption
		{
			get { return SettingsManager.Instance.DashboardName + " - " + SettingsManager.Instance.SelectedWizard + " - " + SettingsManager.Instance.Size; }
		}

		public void RunForm()
		{
			using (var form = new FormLoadSplash())
			{
				form.TopMost = true;
				var thread = new Thread(delegate()
				{
					RunMinibar();
					DashboardPowerPointHelper.Instance.SetPresentationSettings();
				});
				thread.Start();

				form.Show();

				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			FormMain.Instance.Init();
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			Application.Run(FormMain.Instance);
		}

		public bool RunPowerPoint()
		{
			return DashboardPowerPointHelper.Instance.Connect();
		}

		public void RunMinibar()
		{
			Process[] processes = Process.GetProcesses();
			if (processes.Where(x => x.ProcessName.ToLower().Contains("minibar")).Count() == 0)
				if (File.Exists(SettingsManager.Instance.MinibarApplicationPath))
					Process.Start(SettingsManager.Instance.MinibarApplicationPath);
		}

		public void RunOneDomain()
		{
			Process[] processes = Process.GetProcesses();
			if (processes.Where(x => x.ProcessName.ToLower().Contains("onedomain")).Count() == 0)
				if (File.Exists(SettingsManager.Instance.OneDomainApplicationPath))
					Process.Start(SettingsManager.Instance.OneDomainApplicationPath);
		}

		public void RunSalesDepot()
		{
			Process[] processes = Process.GetProcesses();
			if (processes.Where(x => x.ProcessName.ToLower().Contains("salesdepot")).Count() == 0)
				if (File.Exists(SettingsManager.Instance.SalesDepotApplicationPath))
					Process.Start(SettingsManager.Instance.SalesDepotApplicationPath);
		}

		public void ActivateMainForm()
		{
			IntPtr mainFormHandle = RegistryHelper.MainFormHandle;
			if (mainFormHandle.ToInt32() == 0)
			{
				Process[] processList = Process.GetProcesses();
				foreach (Process process in processList.Where(x => x.ProcessName.Contains("adSALESapp")))
				{
					if (process.MainWindowHandle.ToInt32() != 0)
					{
						mainFormHandle = process.MainWindowHandle;
						break;
					}
				}
			}
			Utilities.Instance.ActivateForm(mainFormHandle, RegistryHelper.MaximizeMainForm, false);
		}

		public static void SetAutoScrollPosition(ScrollableControl sender, Point p)
		{
			p.X = Math.Abs(p.X);
			p.Y = Math.Abs(p.Y);
			sender.AutoScrollPosition = p;
		}

		public void SetClickEventHandler(Control control)
		{
			foreach (Control childControl in control.Controls)
				SetClickEventHandler(childControl);
			if (control.GetType() != typeof(TextBoxMaskBox) && control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				control.Click += ControlClick;
			}
		}

		private void ControlClick(object sender, EventArgs e)
		{
			((Control)sender).Select();
			if (((Control)sender).Parent != null)
				((Control)sender).Parent.Select();
		}

		public Bitmap MakeGrayscale(Bitmap original)
		{
			var newBitmap = new Bitmap(original.Width, original.Height);

			//get a graphics object from the new image
			Graphics g = Graphics.FromImage(newBitmap);

			//create the grayscale ColorMatrix
			var colorMatrix = new ColorMatrix(
				new[]
				{
					new[] { .3f, .3f, .3f, 0, 0 },
					new[] { .59f, .59f, .59f, 0, 0 },
					new[] { .11f, .11f, .11f, 0, 0 },
					new float[] { 0, 0, 0, 1, 0 },
					new float[] { 0, 0, 0, 0, 1 }
				});

			//create some image attributes
			var attributes = new ImageAttributes();

			//set the color matrix attribute
			attributes.SetColorMatrix(colorMatrix);

			//draw the original image on the new image
			//using the grayscale color matrix
			g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
						0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

			//dispose the Graphics object
			g.Dispose();
			return newBitmap;
		}
	}
}