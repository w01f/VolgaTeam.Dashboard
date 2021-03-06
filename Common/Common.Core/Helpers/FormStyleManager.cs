﻿using System.Xml;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Helpers
{
	public class FormStyleManager
	{
		private readonly StorageFile _contentFile;

		public MainFormStyleConfiguration Style { get; }

		public FormStyleManager(StorageFile contentFile)
		{
			_contentFile = contentFile;
			Style = new MainFormStyleConfiguration();
			LoadConfig();
		}

		private void LoadConfig()
		{
			if (!_contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(_contentFile.LocalPath);
			var node = document.SelectSingleNode(@"/Config/Style");
			if (node == null) return;
			Style.Deserialize(node);
		}
	}
}
