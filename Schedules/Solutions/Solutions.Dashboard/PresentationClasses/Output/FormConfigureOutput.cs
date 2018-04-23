using System;
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

namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public partial class FormConfigureOutput : MetroForm
	{
		private bool _allowHandleEvents;

		public FormConfigureOutput(IList<DashboardSlideInfo> slideItems, PreviewGroup currentPreviewGroup)
		{
			InitializeComponent();

			_allowHandleEvents = false;

			var currentSlideInfo = slideItems.FirstOrDefault(slideInfo => slideInfo.IsCurrent) ?? slideItems.First();
			simpleLabelItemCurrentSlideName.Text = String.Format("<size=+2>{0}</size>", currentSlideInfo.SlideName);

			var imageSourcePath = currentPreviewGroup.PresentationSourcePath.Replace(Path.GetExtension(currentPreviewGroup.PresentationSourcePath), String.Empty);
			if (Directory.Exists(imageSourcePath))
			{
				var previewImageFile = Directory.GetFiles(imageSourcePath, "*.png").FirstOrDefault();
				if (previewImageFile != null)
					pictureEditCurrentSlidePreview.Image = Image.FromFile(previewImageFile);
			}

			treeList.Nodes.Clear();
			foreach (var slideInfo in slideItems)
			{
				var groupNode = treeList.AppendNode(new object[] { slideInfo.SlideName }, null);
				groupNode.Tag = slideInfo;
				groupNode.Checked = true;
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

		public IList<DashboardSlideInfo> GetSelectedSlideItems()
		{
			var selectedSlideItems = new List<DashboardSlideInfo>();

			if (tabbedControlGroup.SelectedTabPageIndex == 0)
			{
				var slideInfoItems = treeList.Nodes.OfType<TreeListNode>().Select(node => node.Tag).OfType<DashboardSlideInfo>().ToList();
				selectedSlideItems.Add(slideInfoItems.FirstOrDefault(slideInfo => slideInfo.IsCurrent) ?? slideInfoItems.FirstOrDefault());
			}
			else
				foreach (var node in treeList.Nodes.OfType<TreeListNode>().Where(node => node.Checked).ToList())
				{
					var slideInfo = (DashboardSlideInfo)node.Tag;
					selectedSlideItems.Add(slideInfo);
				}

			return selectedSlideItems;
		}

		private void UpdateSlidesCount()
		{
			var slidesCount = treeList.Nodes.OfType<TreeListNode>().Count(node => node.Checked);
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