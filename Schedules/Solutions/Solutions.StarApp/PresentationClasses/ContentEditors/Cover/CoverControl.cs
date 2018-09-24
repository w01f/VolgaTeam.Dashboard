using System;
using System.Collections.Generic;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Enums;
using DevExpress.XtraLayout.Utils;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Cover
{
	class CoverControl : MultiTabControl
	{
		public override SlideType SlideType => SlideType.StarAppCover;

		public CoverControl(BaseStarAppContainer slideContainer, StarChildTabsContainer tabInfo) : base(slideContainer, tabInfo) { }

		public override void InitControls()
		{
			base.InitControls();
			layoutControlItemAddAsPageOne.Visibility = LayoutVisibility.Always;
		}

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case StarChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<CoverTabAControl>(this, tabInfo));
						break;
					case StarChildTabType.K:
					case StarChildTabType.L:
					case StarChildTabType.M:
					case StarChildTabType.N:
					case StarChildTabType.O:
						tabPages.Add(new ChildTabPageContainerControl<SingleSlidesTabControl>(this, tabInfo));
						break;
					case StarChildTabType.U:
					case StarChildTabType.V:
					case StarChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<MultiSlidesTabControl>(this, tabInfo));
						break;
					case StarChildTabType.X:
					case StarChildTabType.Y:
					case StarChildTabType.Z:
						tabPages.Add(new ChildTabPageContainerControl<TilesTabControl>(this, tabInfo));
						break;
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
			return tabPages;
		}

		public override void LoadData()
		{
			base.LoadData();

			_allowToSave = false;

			checkEditAddAsPageOne.Checked = SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne;

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			base.ApplyChanges();

			if (!_dataChanged) return;
			SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne = checkEditAddAsPageOne.Checked;
		}
	}
}
