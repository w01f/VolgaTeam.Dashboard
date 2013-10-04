using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Reset.Properties;

namespace NewBizWiz.Reset
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			string archiveFolderPath = string.Format(@"{0}\newlocaldirect.com\z_archive", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			string syncSettingsFolderPath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			string adSyncLogPath = string.Format(@"{0}\newlocaldirect.com\xml\update_logs", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			string traceFileName = @"adSync_Trace.log";
			string syncSettingsFileName = @"syncfile.xml";

			bool result = true;
			Image flag = null;
			Exception ex = null;

			var thread = new Thread(delegate()
			{
				try
				{
					Process[] processes = Process.GetProcesses();
					foreach (Process process in processes.Where(x => x.ProcessName.ToLower().Contains("minibar")))
						process.Kill();
					flag = Resources.FlagGreen;
					result = result & true;
				}
				catch
				{
					flag = Resources.FlagRed;
					result = result & false;
				}
				finally
				{
					Invoke((MethodInvoker)delegate()
					{
						laActionTitle1.Visible = true;
						pictureBox1.Visible = true;
						pictureBox1.Image = flag;
						Refresh();
						Application.DoEvents();
					});
				}

				try
				{
					if (File.Exists(Path.Combine(syncSettingsFolderPath, syncSettingsFileName)))
						File.Delete(Path.Combine(syncSettingsFolderPath, syncSettingsFileName));
					flag = Resources.FlagGreen;
					result = result & true;
				}
				catch
				{
					flag = Resources.FlagRed;
					result = result & false;
				}
				finally
				{
					Invoke((MethodInvoker)delegate()
					{
						laActionTitle2.Visible = true;
						pictureBox2.Visible = true;
						pictureBox2.Image = flag;
						Refresh();
						Application.DoEvents();
					});
				}

				try
				{
					if (File.Exists(Path.Combine(syncSettingsFolderPath, traceFileName)))
						File.Delete(Path.Combine(syncSettingsFolderPath, traceFileName));
					if (!string.IsNullOrEmpty(adSyncLogPath))
						foreach (FileInfo file in (new DirectoryInfo(adSyncLogPath)).GetFiles())
							file.Delete();
					flag = Resources.FlagGreen;
					result = result & true;
				}
				catch
				{
					flag = Resources.FlagRed;
					result = result & false;
				}
				finally
				{
					Invoke((MethodInvoker)delegate()
					{
						laActionTitle3.Visible = true;
						pictureBox3.Visible = true;
						pictureBox3.Image = flag;
						Refresh();
						Application.DoEvents();
					});
				}

				try
				{
					if (!Directory.Exists(archiveFolderPath))
						Directory.CreateDirectory(archiveFolderPath);
					var outgoingFolder = new DirectoryInfo(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
					CopyDirectory(outgoingFolder.FullName, archiveFolderPath);
					flag = Resources.FlagGreen;
					result = result & true;
				}
				catch
				{
					flag = Resources.FlagRed;
					result = result & false;
				}
				finally
				{
					Invoke((MethodInvoker)delegate()
					{
						laActionTitle4.Visible = true;
						pictureBox4.Visible = true;
						pictureBox4.Image = flag;
						Refresh();
						Application.DoEvents();
					});
				}

				try
				{
					string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
					if (Directory.Exists(localSettingsFolder))
						foreach (DirectoryInfo xmlFolder in (new DirectoryInfo(localSettingsFolder)).GetDirectories())
							result = result & DeleteFolder(xmlFolder, ref ex);
					flag = result ? Resources.FlagGreen : Resources.FlagRed;
					result = result & true;
				}
				catch
				{
					flag = Resources.FlagRed;
					result = result & false;
				}
				finally
				{
					Invoke((MethodInvoker)delegate()
					{
						laActionTitle5.Visible = true;
						pictureBox5.Visible = true;
						pictureBox5.Image = flag;
						Refresh();
						Application.DoEvents();
					});
				}

				try
				{
					string syncFolder = string.Format(@"{0}\newlocaldirect.com\sync", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
					if (Directory.Exists(syncFolder))
						foreach (DirectoryInfo syncSubFolder in (new DirectoryInfo(syncFolder)).GetDirectories())
							result = result & DeleteFolder(syncSubFolder, ref ex);
					flag = result ? Resources.FlagGreen : Resources.FlagRed;
					result = result & true;
				}
				catch
				{
					flag = Resources.FlagRed;
					result = result & false;
				}
				finally
				{
					Invoke((MethodInvoker)delegate()
					{
						laActionTitle6.Visible = true;
						pictureBox6.Visible = true;
						pictureBox6.Image = flag;
						Refresh();
						Application.DoEvents();
					});
				}
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			flag = result ? Resources.FlagGreen : Resources.FlagRed;
			laActionTitle7.Visible = true;
			pictureBox7.Visible = true;
			pictureBox7.Image = flag;

			gbMain.Visible = false;
			buttonItemHomeConfig.Enabled = result;
			buttonItemHomeHelp.Enabled = true;
			buttonItemHomeExit.Enabled = true;

			if (!result && ex != null)
				AppManager.Instance.ShowWarning(ex.Message + Environment.NewLine + ex.InnerException);
		}

		private bool DeleteFolder(DirectoryInfo folder, ref Exception exception)
		{
			bool result = false;
			try
			{
				foreach (DirectoryInfo subFolder in folder.GetDirectories())
					if (!DeleteFolder(subFolder, ref exception))
						return result;
				FileInfo[] files = folder.GetFiles();
				foreach (FileInfo file in files)
				{
					try
					{
						if (File.Exists(file.FullName))
						{
							File.SetAttributes(file.FullName, FileAttributes.Normal);
							File.Delete(file.FullName);
						}
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName))
								File.Delete(file.FullName);
						}
						catch { }
					}
				}
				try
				{
					if (Directory.Exists(folder.FullName))
						Directory.Delete(folder.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName))
							Directory.Delete(folder.FullName, false);
					}
					catch { }
				}
				result = true;
			}
			catch (Exception ex)
			{
				exception = ex;
				result = false;
			}
			return result;
		}

		private void CopyDirectory(string source, string destination)
		{
			if (!Directory.Exists(source)) return;
			var dir = new DirectoryInfo(source);
			if (!Directory.Exists(destination))
				Directory.CreateDirectory(destination);
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				file.CopyTo(Path.Combine(destination, file.Name));
				try
				{
					if (File.Exists(file.FullName))
					{
						File.SetAttributes(file.FullName, FileAttributes.Normal);
						File.Delete(file.FullName);
					}
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (File.Exists(file.FullName))
							File.Delete(file.FullName);
					}
					catch { }
				}
			}
			DirectoryInfo[] children = dir.GetDirectories();
			foreach (DirectoryInfo subdir in children)
			{
				CopyDirectory(Path.Combine(source, subdir.FullName), Path.Combine(destination, subdir.Name));
				try
				{
					if (Directory.Exists(subdir.FullName))
						Directory.Delete(subdir.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(subdir.FullName))
							Directory.Delete(subdir.FullName, false);
					}
					catch { }
				}
			}
		}

		private void buttonItemHomeConfig_Click(object sender, EventArgs e)
		{
			string minibarFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBarLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			try
			{
				Process.Start(minibarFile);
			}
			catch { }
			Application.Exit();
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void buttonItemHomeHelp_Click(object sender, EventArgs e)
		{
			AppManager.Instance.HelpManager.OpenHelpLink("reset");
		}
	}
}