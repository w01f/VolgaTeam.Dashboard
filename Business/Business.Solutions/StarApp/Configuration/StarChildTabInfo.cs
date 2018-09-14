using System;
using System.Drawing;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarChildTabInfo : StarTabInfo
	{
		public abstract StarChildTabType TabType { get; }
		public StarTopTabType TopTabType { get; }
		public virtual bool IsRegularChildTab => false;
		public bool EnableOutput { get; protected set; }

		public Image RightLogo
		{
			get
			{
				switch (TopTabType)
				{
					case StarTopTabType.Cover:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab1_A_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.CNA:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Fishing:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Customer:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Share:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_Right;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_Right;
							case StarChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.ROI:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_Right;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Market:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Video:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_Right;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Audience:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Solution:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_Right;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Closers:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_Right;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_Right;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Right;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
		}

		public Image FooterLogo
		{
			get
			{
				switch (TopTabType)
				{
					case StarTopTabType.Cover:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab1_A_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.CNA:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Fishing:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Customer:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Share:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_Footer;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_Footer;
							case StarChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.ROI:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_Footer;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Market:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Video:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_Footer;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Audience:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Solution:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_Footer;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Closers:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_Footer;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_Footer;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Footer;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
		}

		public Image BackgroundLogo
		{
			get
			{
				switch (TopTabType)
				{
					case StarTopTabType.Cover:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab1_A_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.CNA:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Fishing:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Customer:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Share:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_Background;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_Background;
							case StarChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.ROI:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_Background;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Market:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Video:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_Background;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Audience:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Solution:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_Background;
							case StarChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					case StarTopTabType.Closers:
						switch (TabType)
						{
							case StarChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_Background;
							case StarChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_Background;
							case StarChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
		}

		protected StarChildTabInfo(StarTopTabType topTabType)
		{
			TopTabType = topTabType;
			EnableOutput = true;
		}
	}
}
