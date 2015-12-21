using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Asa.Core.Common
{
	public class AuthManager
	{
		public StorageFile SettingsFile { get; private set; }
		public AuthSettings Settings { get; private set; }

		public void Init()
		{
			SettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Credentials.xml"
			});
			SettingsFile.AllocateParentFolder();

			Settings = AuthSettings.Load(SettingsFile);
		}

		public virtual void Auth(AuthorizingEventArgs authArgs)
		{
			authArgs.Authorized = Settings.HasCredentials && 
				(FileStorageManager.Instance.UseLocalMode || 
					IsAuthorized(authArgs.AuthServer, Settings.Login, Settings.GetPassword()));
		}

		protected bool IsAuthorized(string url, string login, string password)
		{
			var authorized = false;
			var thread = new Thread(() =>
			{
				try
				{
					var client = new AdSalesDataControllerService.AdSalesDataControllerService
					{
						Url = String.Format("{0}/AdSalesData/quote?ws=1", url)
					};
					var sessionKey = client.getSessionKey(login, password);
					authorized = !String.IsNullOrEmpty(sessionKey);
				}
				catch { }
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			return authorized;
		}
	}

	[Serializable]
	public class AuthSettings
	{
		private const string EncryptionKey = "MAKV2SPBNI99212";
		[NonSerialized]
		private readonly byte[] _salt = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
		private StorageFile _settingsFile;

		public string Password { get; set; }
		public string Login { get; set; }

		public bool HasCredentials
		{
			get { return !String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password); }
		}

		public void Init(StorageFile settingsFile)
		{
			_settingsFile = settingsFile;
		}

		public static AuthSettings Load(StorageFile settingsFile)
		{
			var settings = SettingsSerializeHelper.Load<AuthSettings>(settingsFile);
			settings.Init(settingsFile);
			return settings;
		}

		public void Save()
		{
			this.Save(_settingsFile);
		}

		public string GetPassword()
		{
			var clearText = String.Empty;
			var cipherBytes = Convert.FromBase64String(Password);
			using (var encryptor = Aes.Create())
			{
				var pdb = new Rfc2898DeriveBytes(EncryptionKey, _salt);
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					clearText = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return clearText;
		}

		public void SetPassword(string password)
		{
			var clearBytes = Encoding.Unicode.GetBytes(password);
			using (var encryptor = Aes.Create())
			{
				var pdb = new Rfc2898DeriveBytes(EncryptionKey, _salt);
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					Password = Convert.ToBase64String(ms.ToArray());
				}
			}
		}
	}
}
