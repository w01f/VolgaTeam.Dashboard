using DevExpress.XtraTreeList;
using System.Drawing;
using System.Linq;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;

namespace Asa.Common.GUI.OutputSelector
{
	public class OutputItemsTreeList : TreeList
	{
		protected override TreeListViewInfo CreateViewInfo()
		{
			return new OutputItemsTreeListInfo(this);
		}

		protected override CheckNodeEventArgs RaiseBeforeCheckNode(TreeListNode node, System.Windows.Forms.CheckState prevState, System.Windows.Forms.CheckState state)
		{
			var e = base.RaiseBeforeCheckNode(node, prevState, state);
			e.CanCheck = IsCheckNode(e.Node);
			return e;
		}

		protected override void RaiseCustomDrawNodeCheckBox(CustomDrawNodeCheckBoxEventArgs e)
		{
			var canCheckNode = IsCheckNode(e.Node);
			if (canCheckNode)
				return;
			e.ObjectArgs.State = DevExpress.Utils.Drawing.ObjectState.Disabled;
			e.Handled = true;

			base.RaiseCustomDrawNodeCheckBox(e);
		}

		public bool IsCheckNode(TreeListNode node)
		{
			return !IsEmptyNode(node) && !IsGroupNode(node);
		}

		public bool IsGroupNode(TreeListNode node)
		{
			return node.Tag is IOutputItem item && item.SlideItems.Any();
		}

		public bool IsEmptyNode(TreeListNode node)
		{
			return node.Tag == null;
		}
	}

	class OutputItemsTreeListInfo : TreeListViewInfo
	{
		public OutputItemsTreeListInfo(TreeList treeList) : base(treeList) { }

		protected override Point GetDataBoundsLocation(TreeListNode node, int top)
		{
			var result = base.GetDataBoundsLocation(node, top);
			if (!((OutputItemsTreeList)TreeList).IsCheckNode(node))
				result.X -= RC.CheckBoxSize.Width;
			if (Size.Empty != RC.SelectImageSize && -1 == node.SelectImageIndex)
				result.X -= RC.SelectImageSize.Width;
			if (Size.Empty != RC.StateImageSize && -1 == node.StateImageIndex)
				result.X -= RC.StateImageSize.Width;
			return result;
		}

		protected override void CalcStateImage(RowInfo ri)
		{
			base.CalcStateImage(ri);
			if (Size.Empty != RC.StateImageSize && !((OutputItemsTreeList)TreeList).IsCheckNode(ri.Node))
				ri.StateImageLocation.X -= RC.CheckBoxSize.Width;
			if (Size.Empty != RC.StateImageSize && -1 == ri.Node.StateImageIndex)
				ri.StateImageLocation.X -= RC.StateImageSize.Width;
		}

		protected override void CalcSelectImage(RowInfo ri)
		{
			base.CalcSelectImage(ri);
			if (-1 == ri.Node.SelectImageIndex)
				ri.SelectImageLocation = Point.Empty;
		}
	}
}
