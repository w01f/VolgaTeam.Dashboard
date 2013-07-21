using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace NewBizWiz.SyncPC
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		public string serverpass;
		public string userid;

		private FormMain()
		{
			InitializeComponent();
		}

		public static FormMain Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormMain();
				return _instance;
			}
		}

		private void UpdateSaveButtonState()
		{
			buttonSaveSettings.Enabled = checkEditAuto.Checked ? (comboBoxEditSelectMedia.SelectedIndex >= 0) : (!string.IsNullOrEmpty(textBoxStationName.Text.Trim()) && !string.IsNullOrEmpty(textBoxPath.Text.Trim()));
		}

		private string GetXML()
		{
			string url = "http://isyncpc.com/generateXML.php?userid=" + userid + "&pass=" + serverpass;
			string result = string.Empty;
			try
			{
				using (var client = new WebClient())
				{
					result = client.DownloadString(url);
					string thisdate = "";
					thisdate += DateTime.Now.ToShortTimeString();
					result = result.Replace("\"EXETIMEHERE\"", thisdate);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return result;
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
				string pasword = File.ReadAllText(Path.Combine(AppManager.Instance.SettingsFolderPath, AppManager.ConfigFileName));
				if (pasword != "")
				{
					serverpass = pasword;
					GetComp();
				}
				else
				{
					MessageBox.Show("Either Config file is missing or password is incorrect");
				}
			}
			catch (Exception ex)
			{
				AppManager.Instance.ShowWarning(ex.Message);
			}
		}

		private void GetComp()
		{
			string url = "http://isyncpc.com/generateComp.php?pass=" + serverpass;

			string result = null;
			var check = new string[10000];
			var cval = new string[2];
			int x;

			try
			{
				var client = new WebClient();
				result = client.DownloadString(url);
				check = result.Split('\n');
				for (x = 0; x < 999; x++)
				{
					if (check[x] != "")
					{
						cval = check[x].Split('|');
						comboBoxEditSelectMedia.Properties.Items.Add(new User(int.Parse(cval[0]), cval[1]));
					}
					else
						break;
				}
			}
			catch (Exception ex)
			{
				AppManager.Instance.ShowWarning(ex.Message);
			}
		}

		private void buttonSaveSettings_Click(object sender, EventArgs e)
		{
			string tempXml = string.Empty;
			DialogResult = DialogResult.Cancel;
			if (string.IsNullOrEmpty(textBoxPath.Text.Trim()) && string.IsNullOrEmpty(textBoxStationName.Text.Trim()))
			{
				tempXml = GetXML();
			}
			else
			{
				string thisdate = "";
				thisdate += DateTime.Now.ToShortTimeString();
				tempXml = "<?xml version=\"1.0\" standalone=\"yes\"?>\n<Settings>\n <MediaProperty>\n  <Name>" + textBoxStationName.Text + "</Name>\n  <Path>\"" + textBoxPath.Text + "\"</Path>\n  <SyncTime>" + thisdate + "</SyncTime>\n </MediaProperty>\n</Settings>";
			}
			if (!string.IsNullOrEmpty(tempXml))
			{
				if (AppManager.Instance.IsConfigured(tempXml))
				{
					using (TextWriter tw = new StreamWriter(Path.Combine(AppManager.Instance.SettingsFolderPath, AppManager.SyncSettingsFileName)))
					{
						tw.WriteLine(tempXml);
						tw.Close();
					}
					DialogResult = DialogResult.OK;
				}
			}

			if (DialogResult == DialogResult.OK)
			{
				if (File.Exists(Path.Combine(AppManager.Instance.SettingsFolderPath, AppManager.SyncProcessFileName)))
					Process.Start(Path.Combine(AppManager.Instance.SettingsFolderPath, AppManager.SyncProcessFileName));
			}
			else
				AppManager.Instance.ShowWarning("Your Network Directory is not yet configured\nTry again another time");

			Close();
		}

		private void comboBoxEditSelectMedia_SelectedIndexChanged(object sender, EventArgs e)
		{
			var user = (User)comboBoxEditSelectMedia.SelectedItem;
			string selectedUser = user.Id.ToString();
			userid = selectedUser;
			UpdateSaveButtonState();
		}

		private void buttonGo_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.isyncpc.com");
		}

		private void textBoxStationName_TextChanged(object sender, EventArgs e)
		{
			UpdateSaveButtonState();
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			pnAuto.Visible = checkEditAuto.Checked;
			pnManual.Visible = checkEditManual.Checked;
			UpdateSaveButtonState();
		}

		private void pbHelp_Click(object sender, EventArgs e)
		{
			AppManager.Instance.HelpManager.OpenHelpLink("initial");
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}

	public class User
	{
		public User(long id, string name)
		{
			Id = id;
			Name = name;
		}

		public long Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}