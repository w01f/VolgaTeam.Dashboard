using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Dictionaries;
using Asa.Common.Core.Helpers;
using DevExpress.Skins;

namespace Asa.Common.GUI.ListEditor
{
	public partial class FormCommonListEditor : DevComponents.DotNetBar.Metro.MetroForm
	{
		private readonly ListContainer<NameCodePair> _advertiser;
		private readonly ListContainer<NameCodePair> _decisionMakers;
		public FormCommonListEditor()
		{
			InitializeComponent();
			_advertiser = new ListContainer<NameCodePair>("Advertisers", ListManager.Instance.Advertisers.Items.Select(a => new NameCodePair { Name = a }));
			_decisionMakers = new ListContainer<NameCodePair>("Decision Makers", ListManager.Instance.DecisionMakers.Items.Select(dm => new NameCodePair { Name = dm }));
			xtraTabControl.TabPages.AddRange(new[] { _advertiser, _decisionMakers });

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void FormCommonListEditor_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;

			ListManager.Instance.Advertisers.AddRange(_advertiser.GetRecords().Where(nc => !String.IsNullOrEmpty(nc.Name)).Select(nc => nc.Name));
			ListManager.Instance.DecisionMakers.AddRange(_decisionMakers.GetRecords().Where(nc => !String.IsNullOrEmpty(nc.Name)).Select(nc => nc.Name));
		}
	}
}