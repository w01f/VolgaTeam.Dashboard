﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public partial class FormConfigureOutput : MetroForm
	{
		private bool _allowHandleEvents;

		public FormConfigureOutput(IList<OutputGroup> outputGroups, PreviewGroup currentPreviewGroup)
		{
			InitializeComponent();

			_allowHandleEvents = false;

			var currentGroup = outputGroups.First(group => group.Configurations.Any(configuration => configuration.IsCurrent));
			var currentConfiguration = currentGroup.Configurations.First(configuration => configuration.IsCurrent);

			simpleLabelItemCurrentSlideGroupName.Text = String.Format("<size=+2><color=gray>{0}</color></size>", currentGroup.Name);
			simpleLabelItemCurrentSlideName.Text = String.Format("<size=+2>{0}</size>", currentConfiguration.DisplayName);

			var imageSourcePath = currentPreviewGroup.PresentationSourcePath.Replace(Path.GetExtension(currentPreviewGroup.PresentationSourcePath), String.Empty);
			if (Directory.Exists(imageSourcePath))
			{
				var previewImageFile = Directory.GetFiles(imageSourcePath, "*.png").FirstOrDefault();
				if (previewImageFile != null)
					pictureEditCurrentSlidePreview.Image = Image.FromFile(previewImageFile);
			}

			treeList.Nodes.Clear();
			foreach (var outputGroup in outputGroups)
			{
				var groupNode = treeList.AppendNode(new object[] { outputGroup.Name }, null);
				groupNode.Tag = outputGroup;
				groupNode.Checked = true;
				foreach (var outputConfiguration in outputGroup.Configurations)
				{
					var configNode = treeList.AppendNode(new object[] { outputConfiguration.DisplayName }, groupNode);
					configNode.Tag = outputConfiguration;
					configNode.Checked = true;
				}

				var emptyNode = treeList.AppendNode(new object[] { String.Empty }, groupNode);
				emptyNode.Checked = false;
			}
			treeList.ExpandAll();

			UpdateSlidesCount();

			_allowHandleEvents = true;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			treeList.RowHeight = (Int32)(treeList.RowHeight * scaleFactor.Height);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (tabbedControlGroup.SelectedTabPageIndex == 0)
				foreach (var groupNode in treeList.Nodes.OfType<TreeListNode>().ToList())
				{
					var outputGroup = (OutputGroup)groupNode.Tag;
					outputGroup.Configurations = outputGroup.Configurations.Where(configuration => configuration.IsCurrent).ToArray();
				}
			else
				foreach (var groupNode in treeList.Nodes.OfType<TreeListNode>().ToList())
				{
					var outputGroup = (OutputGroup)groupNode.Tag;
					if (groupNode.Nodes.Count > 0)
						outputGroup.Configurations = groupNode.Nodes
							.OfType<TreeListNode>()
							.Where(n => n.Checked && n.Tag is OutputConfiguration)
							.Select(n => (OutputConfiguration)n.Tag)
							.ToArray();
					else if (!groupNode.Checked)
						outputGroup.Configurations = new OutputConfiguration[] { };
				}
		}

		private void UpdateSlidesCount()
		{
			var slidesCount = treeList.Nodes
				.OfType<TreeListNode>()
				.Sum(n =>
				{
					if (n.Nodes.Count > 0)
						return n.Nodes
							.OfType<TreeListNode>()
							.Where(childNode => childNode.Checked && childNode.Tag is OutputConfiguration)
							.Sum(childNode => ((OutputConfiguration)childNode.Tag).SlidesCount);
					return ((OutputGroup)n.Tag).Configurations.Sum(c => c.SlidesCount);
				});

			simpleLabelItemSlideCount.Text = String.Format("<color=gray>Estimated Slides: {0}</color>", slidesCount);
		}

		private void CheckWithDecendants(TreeListNode node)
		{
			_allowHandleEvents = false;

			foreach (var chiledNode in node.Nodes.OfType<TreeListNode>())
				CheckWithDecendants(chiledNode);
			if (node.Tag != null)
				node.Checked = true;

			_allowHandleEvents = true;
		}

		private void UncheckWithDecendants(TreeListNode node)
		{
			_allowHandleEvents = false;

			foreach (var childNode in node.Nodes.OfType<TreeListNode>())
				UncheckWithDecendants(childNode);
			if (node.Tag != null)
				node.Checked = false;

			_allowHandleEvents = true;
		}

		private void OnAddSingleSlideClick(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void OnCustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
		{
			if (e.Node.Tag is OutputGroup)
				e.Appearance.ForeColor = Color.Gray;
		}

		private void OnAfterCheckNode(object sender, NodeEventArgs e)
		{
			if (!_allowHandleEvents) return;

			UpdateSlidesCount();
		}

		private void OnSelectAllClick(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;

			foreach (var treeNode in treeList.Nodes.OfType<TreeListNode>())
				CheckWithDecendants(treeNode);

			UpdateSlidesCount();
		}

		private void OnClearAllClick(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;

			foreach (var treeNode in treeList.Nodes.OfType<TreeListNode>())
				UncheckWithDecendants(treeNode);

			UpdateSlidesCount();
		}
	}
}