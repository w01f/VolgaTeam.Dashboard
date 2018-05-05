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
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Asa.Common.GUI.OutputSelector
{
	public partial class FormConfigureOutput<TOutputItem> : MetroForm where TOutputItem : class, IOutputItem
	{
		private bool _allowHandleEvents;

		public FormConfigureOutput(IList<TOutputItem> outputItems, PreviewGroup currentPreviewGroup)
		{
			InitializeComponent();

			_allowHandleEvents = false;

			var currentGroup = outputItems.FirstOrDefault(item => item.IsCurrent && item.SlideItems.Any(subItem => subItem.IsCurrent));
			var currentSubItem = currentGroup != null ?
				currentGroup.SlideItems.OfType<IOutputItem>().First(configuration => configuration.IsCurrent) :
				outputItems.OfType<IOutputItem>().FirstOrDefault(item => item.IsCurrent) ?? outputItems.OfType<IOutputItem>().First();

			if (currentGroup != null)
			{
				simpleLabelItemCurrentSlideGroupName.Text =
					String.Format("<size=+2><color=gray>{0}</color></size>", currentGroup.DisplayName);
			}
			else
			{
				simpleLabelItemCurrentSlideGroupName.Visibility = LayoutVisibility.Never;
			}
			simpleLabelItemCurrentSlideName.Text = String.Format("<size=+2>{0}</size>", currentSubItem.DisplayName);

			var imageSourcePath = currentPreviewGroup.PresentationSourcePath.Replace(Path.GetExtension(currentPreviewGroup.PresentationSourcePath), String.Empty);
			if (Directory.Exists(imageSourcePath))
			{
				var previewImageFile = Directory.GetFiles(imageSourcePath, "*.png").FirstOrDefault();
				if (previewImageFile != null)
					pictureEditCurrentSlidePreview.Image = Image.FromFile(previewImageFile);
			}

			treeList.Nodes.Clear();
			foreach (var outputItem in outputItems)
			{
				var itemNode = treeList.AppendNode(new object[] { outputItem.DisplayName }, null);
				itemNode.Tag = outputItem;
				itemNode.Checked = true;

				foreach (var subItem in outputItem.SlideItems)
				{
					var subItemNode = treeList.AppendNode(new object[] { subItem.DisplayName }, itemNode);
					subItemNode.Tag = subItem;
					subItemNode.Checked = subItem.SelectedForOutput;
				}

				var separatorNode = treeList.AppendNode(new object[] { String.Empty }, null);
				separatorNode.Checked = false;
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

		public IList<TOutputItem> GetSelectedItems()
		{
			var selectedItems = new List<TOutputItem>();

			if (tabbedControlGroup.SelectedTabPageIndex == 0)
			{
				foreach (var groupNode in treeList.Nodes.OfType<TreeListNode>().ToList())
				{
					var outputItem = groupNode.Tag as TOutputItem;
					if (outputItem == null) continue;
					if (!outputItem.IsCurrent) continue;
					if (groupNode.Nodes.Count > 0)
					{
						var slideItems = outputItem.SlideItems.Where(slideItem => slideItem.IsCurrent).ToArray();
						outputItem.SlideItems = slideItems;
					}
					selectedItems.Add(outputItem);
				}
				if (!selectedItems.Any())
					selectedItems.Add(treeList.Nodes.OfType<TreeListNode>().Select(node => node.Tag).OfType<TOutputItem>().FirstOrDefault());
			}
			else
				foreach (var groupNode in treeList.Nodes.OfType<TreeListNode>().ToList())
				{
					var outputItem = groupNode.Tag as TOutputItem;
					if (outputItem == null) continue;
					if (groupNode.Nodes.Count > 0)
					{
						var slideItems = groupNode.Nodes
							.OfType<TreeListNode>()
							.Where(n => n.Checked)
							.Select(n => n.Tag)
							.OfType<ISlideItem>()
							.ToArray();
						outputItem.SlideItems = slideItems;
						if (outputItem.SlideItems.Any())
							selectedItems.Add(outputItem);
					}
					else if (groupNode.Checked)
					{
						selectedItems.Add(outputItem);
					}
				}

			return selectedItems;
		}

		private void UpdateSlidesCount()
		{
			var slidesCount = treeList.Nodes
				.OfType<TreeListNode>()
				.Sum(itemNode =>
				{
					if (itemNode.Nodes.Count > 0)
						return itemNode.Nodes
							.OfType<TreeListNode>()
							.Where(subItemNodeNode => subItemNodeNode.Checked && subItemNodeNode.Tag is ISlideItem)
							.Sum(childNode => ((ISlideItem)childNode.Tag).SlidesCount);
					if (itemNode.Checked && itemNode.Tag is ISlideItem item)
						return item.SlidesCount;
					return 0;
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
			if (e.Node.Tag is IOutputItem item && item.SlideItems.Any())
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