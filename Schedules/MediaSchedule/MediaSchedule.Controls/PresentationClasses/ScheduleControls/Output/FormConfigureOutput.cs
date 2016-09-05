using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public partial class FormConfigureOutput : MetroForm
	{
		private bool _handleNodeEvents;
		public FormConfigureOutput(string scheduleName, IEnumerable<ScheduleSectionOutputItem> outputItems)
		{
			InitializeComponent();

			_handleNodeEvents = false;

			var groupNode = treeView.Nodes.Add(scheduleName);
			groupNode.Checked = true;

			foreach (var optionItem in outputItems.OrderBy(outputItem => outputItem.OutputType))
			{
				var configNode = groupNode.Nodes.Add(optionItem.DisplayName);
				configNode.Tag = optionItem;
				configNode.Checked = true;
			}
			treeView.ExpandAll();
			_handleNodeEvents = true;

			UpdateSlidesCount();

			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				buttonXSelectAll.Font = new Font(buttonXSelectAll.Font.FontFamily, buttonXSelectAll.Font.Size - 2, buttonXSelectAll.Font.Style);
				buttonXSelectNone.Font = new Font(buttonXSelectNone.Font.FontFamily, buttonXSelectNone.Font.Size - 2, buttonXSelectNone.Font.Style);
				buttonXContinue.Font = new Font(buttonXContinue.Font.FontFamily, buttonXContinue.Font.Size - 2, buttonXContinue.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 2, buttonXClose.Font.Style);
			}
		}

		public IEnumerable<ScheduleSectionOutputType> GetSelectedOutputTypes()
		{
			return treeView.Nodes[0].Nodes
				.OfType<TreeNode>()
				.Where(n => n.Checked)
				.Select(node => ((ScheduleSectionOutputItem)node.Tag).OutputType)
				.OrderBy(selectedOption => selectedOption)
				.ToList();
		}

		private void UpdateSlidesCount()
		{
			labelControlSlidesCount.Text = String.Format("<color=gray>Estimated Slides: {0}</color>",
				treeView.Nodes[0].Nodes
					.OfType<TreeNode>()
					.Where(n => n.Checked)
					.Sum(node => ((ScheduleSectionOutputItem)node.Tag).SlidesCount)
				);
		}

		private void CheckWithDecendants(TreeNode node)
		{
			foreach (var chiledNode in node.Nodes.OfType<TreeNode>())
				CheckWithDecendants(chiledNode);
			node.Checked = true;
		}

		private void UncheckWithDecendants(TreeNode node)
		{
			foreach (var childNode in node.Nodes.OfType<TreeNode>())
				UncheckWithDecendants(childNode);
			node.Checked = false;
		}

		private void OnSelectAllClick(object sender, EventArgs e)
		{
			_handleNodeEvents = false;
			foreach (var treeNode in treeView.Nodes.OfType<TreeNode>())
				CheckWithDecendants(treeNode);
			_handleNodeEvents = true;

			UpdateSlidesCount();
		}

		private void OnSelectNoneClick(object sender, EventArgs e)
		{
			_handleNodeEvents = false;
			foreach (var treeNode in treeView.Nodes.OfType<TreeNode>())
				UncheckWithDecendants(treeNode);
			_handleNodeEvents = true;

			UpdateSlidesCount();
		}

		private void OnTreeViewAfterCheck(object sender, TreeViewEventArgs e)
		{
			buttonXContinue.Enabled = treeView.Nodes.OfType<TreeNode>().Count(n => n.Checked) > 0;

			if (!_handleNodeEvents) return;

			_handleNodeEvents = false;
			if (e.Node.Nodes.Count > 0)
			{
				if (e.Node.Checked)
					CheckWithDecendants(e.Node);
				else
					UncheckWithDecendants(e.Node);
			}
			else if (e.Node.Parent != null)
				e.Node.Parent.Checked = e.Node.Parent.Nodes.OfType<TreeNode>().Any(n => n.Checked);
			_handleNodeEvents = true;

			UpdateSlidesCount();
		}

		private void OnTreeViewBeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = true;
		}

		private void OnTreeViewBeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = true;
		}

		private void OnTreeViewMouseDown(object sender, MouseEventArgs e)
		{
			var tn = treeView.GetNodeAt(e.Location);
			if (tn == null) return;
			tn.BackColor = Color.White;
			tn.ForeColor = Color.Black;
		}

		private void OnTreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.Node.Bounds.Contains(new Point(e.X, e.Y)))
				e.Node.Checked = !e.Node.Checked;
		}
	}
}