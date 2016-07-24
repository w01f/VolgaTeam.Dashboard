using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public partial class FormConfigureOutput : MetroForm
	{
		private bool _handleNodeEvents;
		public FormConfigureOutput(string scheduleName, IEnumerable<ScheduleSectionOutputType> outputOptionItems)
		{
			InitializeComponent();

			_handleNodeEvents = false;

			var groupNode = treeView.Nodes.Add(scheduleName);
			groupNode.Checked = true;

			foreach (var optionItem in outputOptionItems.OrderBy(v => v))
			{
				var itemName = String.Empty;
				switch (optionItem)
				{
					case ScheduleSectionOutputType.Program:
						itemName = MediaMetaData.Instance.DataTypeString;
						break;
					case ScheduleSectionOutputType.DigitalOneSheet:
						itemName = "Digital";
						break;
					case ScheduleSectionOutputType.ProgramAndDigital:
						itemName = String.Format("{0} + Digital", MediaMetaData.Instance.DataTypeString);
						break;
					case ScheduleSectionOutputType.Summary:
						itemName = "Summary";
						break;
					case ScheduleSectionOutputType.DigitalStrategy:
						itemName = "Digital Strategies";
						break;
				}

				var configNode = groupNode.Nodes.Add(itemName);
				configNode.Tag = optionItem;
				configNode.Checked = true;
			}
			treeView.ExpandAll();
			_handleNodeEvents = true;
		}

		public IEnumerable<ScheduleSectionOutputType> GetSelectedOutputTypes()
		{
			return treeView.Nodes[0].Nodes
				.OfType<TreeNode>()
				.Where(n => n.Checked)
				.Select(node => (ScheduleSectionOutputType)node.Tag)
				.OrderBy(selectedOption => selectedOption)
				.ToList();
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
		}

		private void OnSelectNoneClick(object sender, EventArgs e)
		{
			_handleNodeEvents = false;
			foreach (var treeNode in treeView.Nodes.OfType<TreeNode>())
				UncheckWithDecendants(treeNode);
			_handleNodeEvents = true;
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