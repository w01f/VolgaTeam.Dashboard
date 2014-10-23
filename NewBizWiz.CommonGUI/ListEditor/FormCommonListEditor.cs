using System;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.ListEditor
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
		}

		private void FormCommonListEditor_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;

			ListManager.Instance.Advertisers.Clear();
			ListManager.Instance.Advertisers.AddRange(_advertiser.GetRecords().Where(nc => !String.IsNullOrEmpty(nc.Name)).Select(nc => nc.Name));
			ListManager.Instance.Advertisers.Save();

			ListManager.Instance.DecisionMakers.Clear();
			ListManager.Instance.DecisionMakers.AddRange(_decisionMakers.GetRecords().Where(nc => !String.IsNullOrEmpty(nc.Name)).Select(nc => nc.Name));
			ListManager.Instance.DecisionMakers.Save();
		}
	}
}