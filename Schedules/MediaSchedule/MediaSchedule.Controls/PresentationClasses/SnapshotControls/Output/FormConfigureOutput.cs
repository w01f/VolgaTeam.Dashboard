using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public partial class FormConfigureOutput : MetroForm
	{
		private bool _handleNodeEvents;
		public FormConfigureOutput(IList<OutputGroup> outputGroups)
		{
			InitializeComponent();

			_handleNodeEvents = false;
			foreach (var outputGroup in outputGroups)
			{
				var groupNode = treeView.Nodes.Add(outputGroup.Name);
				groupNode.Tag = outputGroup;
				groupNode.Checked = true;
				if (outputGroup.Configurations.Length <= 1) continue;
				foreach (var outputConfiguration in outputGroup.Configurations.OrderBy(c => c.OutputType))
				{
					var configNode = groupNode.Nodes.Add(outputConfiguration.DisplayName);
					configNode.Tag = outputConfiguration;
					configNode.Checked = true;
				}
			}
			treeView.ExpandAll();
			_handleNodeEvents = true;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			foreach (var groupNode in treeView.Nodes.OfType<TreeNode>())
			{
				var outputGroup = (OutputGroup) groupNode.Tag;
				if (groupNode.Nodes.Count > 0)
					outputGroup.Configurations = groupNode.Nodes
						.OfType<TreeNode>()
						.Where(n => n.Checked)
						.Select(n => (OutputConfiguration) n.Tag)
						.ToArray();
				else if (!groupNode.Checked)
					outputGroup.Configurations = new OutputConfiguration[] {};
			}
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

		private void OnSelectCurrentClick(object sender, EventArgs e)
		{
			OnSelectNoneClick(sender, e);

			_handleNodeEvents = false;
			foreach (var treeNode in treeView.Nodes.OfType<TreeNode>())
				if (((OutputGroup)treeNode.Tag).IsCurrent)
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