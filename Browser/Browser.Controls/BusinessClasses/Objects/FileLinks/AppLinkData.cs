using System;
using System.Collections.Generic;
using Asa.Browser.Controls.BusinessClasses.Enums;

namespace Asa.Browser.Controls.BusinessClasses.Objects.FileLinks
{
	class AppLinkData : BaseLinkData
	{
		private string _secondPath;

		public override LinkType Type => LinkType.App;

		public override void Load(object[] data)
		{
			base.Load(data);
			_secondPath = data[3] as String;
		}

		public IEnumerable<string> GetExecutablePaths()
		{
			return new[] { FileUrl, _secondPath };
		}
	}
}
