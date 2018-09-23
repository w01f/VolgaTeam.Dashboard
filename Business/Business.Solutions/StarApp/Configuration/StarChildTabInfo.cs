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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab1_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab1_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab1_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab1_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab1_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab2_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab2_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab2_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab2_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab2_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab3_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab3_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab3_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab3_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab3_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab4_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab4_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab4_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab4_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab4_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab5_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab5_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab5_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab5_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab5_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab6_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab6_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab6_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab6_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab6_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab7_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab7_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab7_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab7_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab7_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab8_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab8_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab8_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab8_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab8_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab9_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab9_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab9_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab9_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab9_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab10_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab10_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab10_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab10_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab10_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab11_K_Right;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab11_L_Right;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab11_M_Right;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab11_N_Right;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab11_O_Right;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Right;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Right;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Right;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_Right;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_Right;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_Right;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab1_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab1_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab1_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab1_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab1_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab2_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab2_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab2_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab2_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab2_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab3_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab3_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab3_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab3_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab3_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab4_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab4_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab4_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab4_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab4_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab5_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab5_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab5_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab5_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab5_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab6_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab6_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab6_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab6_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab6_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab7_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab7_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab7_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab7_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab7_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab8_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab8_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab8_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab8_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab8_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab9_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab9_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab9_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab9_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab9_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab10_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab10_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab10_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab10_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab10_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab11_K_Footer;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab11_L_Footer;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab11_M_Footer;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab11_N_Footer;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab11_O_Footer;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Footer;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Footer;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Footer;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_Footer;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_Footer;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_Footer;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab1_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab1_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab1_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab1_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab1_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab2_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab2_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab2_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab2_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab2_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab3_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab3_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab3_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab3_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab3_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab4_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab4_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab4_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab4_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab4_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab5_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab5_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab5_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab5_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab5_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab6_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab6_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab6_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab6_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab6_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab7_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab7_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab7_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab7_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab7_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab8_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab8_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab8_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab8_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab8_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab9_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab9_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab9_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab9_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab9_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab10_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab10_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab10_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab10_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab10_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_Background;
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
							case StarChildTabType.K:
								return _resourceManager.GraphicResources?.Tab11_K_Background;
							case StarChildTabType.L:
								return _resourceManager.GraphicResources?.Tab11_L_Background;
							case StarChildTabType.M:
								return _resourceManager.GraphicResources?.Tab11_M_Background;
							case StarChildTabType.N:
								return _resourceManager.GraphicResources?.Tab11_N_Background;
							case StarChildTabType.O:
								return _resourceManager.GraphicResources?.Tab11_O_Background;
							case StarChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Background;
							case StarChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Background;
							case StarChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Background;
							case StarChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_Background;
							case StarChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_Background;
							case StarChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Star tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
		}

		public Image ListUpImage
		{
			get
			{
				switch (TopTabType)
				{
					case StarTopTabType.Cover:
						return _resourceManager.GraphicResources?.Tab1ListUp;
					case StarTopTabType.CNA:
						return _resourceManager.GraphicResources?.Tab2ListUp;
					case StarTopTabType.Fishing:
						return _resourceManager.GraphicResources?.Tab3ListUp;
					case StarTopTabType.Customer:
						return _resourceManager.GraphicResources?.Tab4ListUp;
					case StarTopTabType.Share:
						return _resourceManager.GraphicResources?.Tab5ListUp;
					case StarTopTabType.ROI:
						return _resourceManager.GraphicResources?.Tab6ListUp;
					case StarTopTabType.Market:
						return _resourceManager.GraphicResources?.Tab7ListUp;
					case StarTopTabType.Video:
						return _resourceManager.GraphicResources?.Tab8ListUp;
					case StarTopTabType.Audience:
						return _resourceManager.GraphicResources?.Tab9ListUp;
					case StarTopTabType.Solution:
						return _resourceManager.GraphicResources?.Tab10ListUp;
					case StarTopTabType.Closers:
						return _resourceManager.GraphicResources?.Tab11ListUp;
					default:
						throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
				}
			}
		}

		public Image ListDownImage
		{
			get
			{
				switch (TopTabType)
				{
					case StarTopTabType.Cover:
						return _resourceManager.GraphicResources?.Tab1ListDown;
					case StarTopTabType.CNA:
						return _resourceManager.GraphicResources?.Tab2ListDown;
					case StarTopTabType.Fishing:
						return _resourceManager.GraphicResources?.Tab3ListDown;
					case StarTopTabType.Customer:
						return _resourceManager.GraphicResources?.Tab4ListDown;
					case StarTopTabType.Share:
						return _resourceManager.GraphicResources?.Tab5ListDown;
					case StarTopTabType.ROI:
						return _resourceManager.GraphicResources?.Tab6ListDown;
					case StarTopTabType.Market:
						return _resourceManager.GraphicResources?.Tab7ListDown;
					case StarTopTabType.Video:
						return _resourceManager.GraphicResources?.Tab8ListDown;
					case StarTopTabType.Audience:
						return _resourceManager.GraphicResources?.Tab9ListDown;
					case StarTopTabType.Solution:
						return _resourceManager.GraphicResources?.Tab10ListDown;
					case StarTopTabType.Closers:
						return _resourceManager.GraphicResources?.Tab11ListDown;
					default:
						throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
				}
			}
		}

		public Image ListPopupImage
		{
			get
			{
				switch (TopTabType)
				{
					case StarTopTabType.Cover:
						return _resourceManager.GraphicResources?.Tab1ListPopup;
					case StarTopTabType.CNA:
						return _resourceManager.GraphicResources?.Tab2ListPopup;
					case StarTopTabType.Fishing:
						return _resourceManager.GraphicResources?.Tab3ListPopup;
					case StarTopTabType.Customer:
						return _resourceManager.GraphicResources?.Tab4ListPopup;
					case StarTopTabType.Share:
						return _resourceManager.GraphicResources?.Tab5ListPopup;
					case StarTopTabType.ROI:
						return _resourceManager.GraphicResources?.Tab6ListPopup;
					case StarTopTabType.Market:
						return _resourceManager.GraphicResources?.Tab7ListPopup;
					case StarTopTabType.Video:
						return _resourceManager.GraphicResources?.Tab8ListPopup;
					case StarTopTabType.Audience:
						return _resourceManager.GraphicResources?.Tab9ListPopup;
					case StarTopTabType.Solution:
						return _resourceManager.GraphicResources?.Tab10ListPopup;
					case StarTopTabType.Closers:
						return _resourceManager.GraphicResources?.Tab11ListPopup;
					default:
						throw new ArgumentOutOfRangeException("StarApp tab type is not defined");
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
