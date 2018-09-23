using System;
using System.Drawing;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftChildTabInfo : ShiftTabInfo
	{
		public ShiftChildTabType TabType { get; }
		public ShiftTopTabType TopTabType { get; }
		public virtual bool IsRegularChildTab => false;
		public bool EnableOutput { get; protected set; }
		public TextEditorConfiguration CommonEditorConfiguration { get; set; }

		public Image RightLogo
		{
			get
			{
				switch (TopTabType)
				{
					case ShiftTopTabType.Cover:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab1_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab1_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab1_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab1_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab1_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab1_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab1_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab1_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab1_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab1_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab1_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab1_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab1_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab1_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab1_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Intro:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab2_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab2_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab2_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab2_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab2_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab2_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab2_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab2_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab2_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab2_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab2_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab2_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab2_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Agenda:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab3_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab3_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab3_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab3_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab3_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab3_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab3_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab3_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab3_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab3_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab3_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab3_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Goals:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab4_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab4_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab4_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab4_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab4_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab4_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab4_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab4_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab4_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab4_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab4_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab4_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Market:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab5_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab5_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab5_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab5_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab5_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab5_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab5_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab5_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab5_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab5_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Partnership:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab6_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab6_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab6_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab6_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab6_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab6_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab6_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab6_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab6_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab6_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab6_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NeedsSolutions:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab7_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab7_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab7_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab7_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab7_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab7_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab7_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab7_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab7_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab7_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab7_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab7_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.CBC:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab8_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab8_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab8_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab8_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab8_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab8_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab8_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab8_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab8_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab8_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab8_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.IntegratedSolution:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab10_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab10_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab10_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab10_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab10_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab10_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab10_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab10_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab10_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab10_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab10_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Investment:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab12_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab12_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab12_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab12_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab12_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab12_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab12_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab12_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab12_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab12_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab12_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab12_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab12_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab12_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab12_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab12_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab12_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab12_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab12_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab12_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab12_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NextSteps:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab14_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab14_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab14_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab14_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab14_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab14_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab14_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab14_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab14_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab14_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab14_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab14_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab14_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab14_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab14_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab14_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab14_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab14_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab14_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab14_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab14_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Contract:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab15_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab15_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab15_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab15_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab15_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab15_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab15_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab15_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab15_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab15_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab15_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab15_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab15_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab15_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab15_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab15_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab15_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab15_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab15_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab15_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab15_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SupportMaterials:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab16_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab16_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab16_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab16_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab16_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab16_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab16_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab16_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab16_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab16_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab16_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab16_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab16_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab16_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab16_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab16_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab16_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab16_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab16_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab16_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab16_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SpecBuilder:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab11_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab11_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab11_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab11_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab11_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab11_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab11_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab11_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab11_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab11_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab11_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab11_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Approach:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab9_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab9_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab9_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab9_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab9_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab9_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab9_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab9_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab9_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab9_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab9_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab9_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab9_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab9_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab9_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.ROI:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab13_A_RightLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab13_B_RightLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab13_C_RightLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab13_D_RightLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab13_E_RightLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab13_F_RightLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab13_G_RightLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab13_H_RightLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab13_I_RightLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab13_J_RightLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab13_K_RightLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab13_L_RightLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab13_M_RightLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab13_N_RightLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab13_O_RightLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab13_U_RightLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab13_V_RightLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab13_W_RightLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab13_X_RightLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab13_Y_RightLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab13_Z_RightLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
		}

		public Image FooterLogo
		{
			get
			{
				switch (TopTabType)
				{
					case ShiftTopTabType.Cover:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab1_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab1_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab1_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab1_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab1_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab1_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab1_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab1_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab1_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab1_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab1_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab1_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab1_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab1_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab1_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Intro:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab2_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab2_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab2_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab2_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab2_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab2_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab2_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab2_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab2_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab2_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab2_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab2_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab2_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Agenda:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab3_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab3_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab3_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab3_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab3_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab3_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab3_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab3_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab3_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab3_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab3_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab3_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Goals:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab4_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab4_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab4_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab4_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab4_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab4_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab4_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab4_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab4_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab4_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab4_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab4_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Market:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab5_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab5_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab5_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab5_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab5_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab5_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab5_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab5_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab5_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab5_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Partnership:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab6_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab6_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab6_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab6_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab6_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab6_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab6_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab6_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab6_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab6_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab6_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NeedsSolutions:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab7_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab7_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab7_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab7_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab7_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab7_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab7_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab7_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab7_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab7_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab7_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab7_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.CBC:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab8_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab8_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab8_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab8_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab8_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab8_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab8_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab8_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab8_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab8_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab8_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.IntegratedSolution:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab10_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab10_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab10_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab10_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab10_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab10_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab10_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab10_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab10_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab10_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab10_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Investment:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab12_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab12_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab12_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab12_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab12_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab12_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab12_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab12_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab12_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab12_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab12_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab12_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab12_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab12_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab12_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab12_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab12_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab12_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab12_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab12_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab12_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NextSteps:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab14_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab14_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab14_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab14_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab14_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab14_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab14_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab14_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab14_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab14_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab14_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab14_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab14_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab14_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab14_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab14_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab14_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab14_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab14_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab14_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab14_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Contract:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab15_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab15_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab15_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab15_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab15_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab15_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab15_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab15_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab15_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab15_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab15_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab15_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab15_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab15_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab15_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab15_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab15_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab15_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab15_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab15_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab15_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SupportMaterials:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab16_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab16_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab16_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab16_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab16_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab16_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab16_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab16_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab16_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab16_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab16_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab16_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab16_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab16_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab16_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab16_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab16_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab16_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab16_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab16_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab16_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SpecBuilder:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab11_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab11_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab11_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab11_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab11_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab11_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab11_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab11_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab11_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab11_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab11_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab11_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Approach:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab9_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab9_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab9_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab9_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab9_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab9_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab9_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab9_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab9_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab9_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab9_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab9_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab9_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab9_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab9_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.ROI:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab13_A_FooterLogo;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab13_B_FooterLogo;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab13_C_FooterLogo;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab13_D_FooterLogo;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab13_E_FooterLogo;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab13_F_FooterLogo;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab13_G_FooterLogo;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab13_H_FooterLogo;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab13_I_FooterLogo;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab13_J_FooterLogo;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab13_K_FooterLogo;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab13_L_FooterLogo;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab13_M_FooterLogo;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab13_N_FooterLogo;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab13_O_FooterLogo;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab13_U_FooterLogo;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab13_V_FooterLogo;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab13_W_FooterLogo;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab13_X_FooterLogo;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab13_Y_FooterLogo;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab13_Z_FooterLogo;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
		}

		public Image BackgroundLogo
		{
			get
			{
				switch (TopTabType)
				{
					case ShiftTopTabType.Cover:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab1_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab1_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab1_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab1_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab1_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab1_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab1_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab1_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab1_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab1_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab1_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab1_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab1_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab1_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab1_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Intro:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab2_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab2_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab2_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab2_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab2_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab2_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab2_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab2_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab2_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab2_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab2_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab2_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab2_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Agenda:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab3_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab3_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab3_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab3_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab3_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab3_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab3_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab3_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab3_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab3_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab3_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab3_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Goals:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab4_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab4_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab4_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab4_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab4_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab4_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab4_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab4_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab4_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab4_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab4_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab4_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Market:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab5_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab5_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab5_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab5_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab5_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab5_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab5_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab5_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab5_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab5_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Partnership:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab6_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab6_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab6_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab6_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab6_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab6_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab6_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab6_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab6_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab6_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab6_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NeedsSolutions:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab7_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab7_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab7_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab7_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab7_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab7_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab7_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab7_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab7_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab7_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab7_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab7_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.CBC:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab8_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab8_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab8_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab8_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab8_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab8_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab8_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab8_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab8_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab8_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab8_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.IntegratedSolution:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab10_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab10_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab10_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab10_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab10_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab10_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab10_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab10_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab10_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab10_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab10_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Investment:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab12_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab12_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab12_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab12_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab12_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab12_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab12_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab12_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab12_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab12_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab12_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab12_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab12_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab12_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab12_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab12_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab12_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab12_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab12_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab12_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab12_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NextSteps:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab14_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab14_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab14_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab14_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab14_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab14_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab14_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab14_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab14_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab14_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab14_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab14_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab14_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab14_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab14_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab14_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab14_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab14_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab14_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab14_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab14_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Contract:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab15_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab15_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab15_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab15_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab15_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab15_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab15_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab15_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab15_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab15_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab15_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab15_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab15_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab15_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab15_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab15_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab15_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab15_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab15_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab15_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab15_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SupportMaterials:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab16_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab16_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab16_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab16_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab16_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab16_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab16_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab16_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab16_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab16_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab16_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab16_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab16_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab16_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab16_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab16_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab16_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab16_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab16_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab16_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab16_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SpecBuilder:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab11_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab11_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab11_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab11_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab11_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab11_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab11_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab11_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab11_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab11_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab11_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab11_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Approach:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab9_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab9_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab9_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab9_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab9_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab9_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab9_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab9_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab9_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab9_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab9_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab9_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab9_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab9_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab9_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.ROI:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab13_A_Background;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab13_B_Background;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab13_C_Background;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab13_D_Background;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab13_E_Background;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab13_F_Background;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab13_G_Background;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab13_H_Background;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab13_I_Background;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab13_J_Background;
							case ShiftChildTabType.K:
								return _resourceManager.GraphicResources?.Tab13_K_Background;
							case ShiftChildTabType.L:
								return _resourceManager.GraphicResources?.Tab13_L_Background;
							case ShiftChildTabType.M:
								return _resourceManager.GraphicResources?.Tab13_M_Background;
							case ShiftChildTabType.N:
								return _resourceManager.GraphicResources?.Tab13_N_Background;
							case ShiftChildTabType.O:
								return _resourceManager.GraphicResources?.Tab13_O_Background;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab13_U_Background;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab13_V_Background;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab13_W_Background;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab13_X_Background;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab13_Y_Background;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab13_Z_Background;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					default:
						throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
		}

		public Image ListUpImage
		{
			get
			{
				switch (TopTabType)
				{
					case ShiftTopTabType.Cover:
						return _resourceManager.GraphicResources?.Tab1ListUp;
					case ShiftTopTabType.Intro:
						return _resourceManager.GraphicResources?.Tab2ListUp;
					case ShiftTopTabType.Agenda:
						return _resourceManager.GraphicResources?.Tab3ListUp;
					case ShiftTopTabType.Goals:
						return _resourceManager.GraphicResources?.Tab4ListUp;
					case ShiftTopTabType.Market:
						return _resourceManager.GraphicResources?.Tab5ListUp;
					case ShiftTopTabType.Partnership:
						return _resourceManager.GraphicResources?.Tab6ListUp;
					case ShiftTopTabType.NeedsSolutions:
						return _resourceManager.GraphicResources?.Tab7ListUp;
					case ShiftTopTabType.CBC:
						return _resourceManager.GraphicResources?.Tab8ListUp;
					case ShiftTopTabType.IntegratedSolution:
						return _resourceManager.GraphicResources?.Tab10ListUp;
					case ShiftTopTabType.Investment:
						return _resourceManager.GraphicResources?.Tab12ListUp;
					case ShiftTopTabType.NextSteps:
						return _resourceManager.GraphicResources?.Tab14ListUp;
					case ShiftTopTabType.Contract:
						return _resourceManager.GraphicResources?.Tab15ListUp;
					case ShiftTopTabType.SupportMaterials:
						return _resourceManager.GraphicResources?.Tab16ListUp;
					case ShiftTopTabType.SpecBuilder:
						return _resourceManager.GraphicResources?.Tab11ListUp;
					case ShiftTopTabType.Approach:
						return _resourceManager.GraphicResources?.Tab9ListUp;
					case ShiftTopTabType.ROI:
						return _resourceManager.GraphicResources?.Tab13ListUp;
					default:
						throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
		}

		public Image ListDownImage
		{
			get
			{
				switch (TopTabType)
				{
					case ShiftTopTabType.Cover:
						return _resourceManager.GraphicResources?.Tab1ListDown;
					case ShiftTopTabType.Intro:
						return _resourceManager.GraphicResources?.Tab2ListDown;
					case ShiftTopTabType.Agenda:
						return _resourceManager.GraphicResources?.Tab3ListDown;
					case ShiftTopTabType.Goals:
						return _resourceManager.GraphicResources?.Tab4ListDown;
					case ShiftTopTabType.Market:
						return _resourceManager.GraphicResources?.Tab5ListDown;
					case ShiftTopTabType.Partnership:
						return _resourceManager.GraphicResources?.Tab6ListDown;
					case ShiftTopTabType.NeedsSolutions:
						return _resourceManager.GraphicResources?.Tab7ListDown;
					case ShiftTopTabType.CBC:
						return _resourceManager.GraphicResources?.Tab8ListDown;
					case ShiftTopTabType.IntegratedSolution:
						return _resourceManager.GraphicResources?.Tab10ListDown;
					case ShiftTopTabType.Investment:
						return _resourceManager.GraphicResources?.Tab12ListDown;
					case ShiftTopTabType.NextSteps:
						return _resourceManager.GraphicResources?.Tab14ListDown;
					case ShiftTopTabType.Contract:
						return _resourceManager.GraphicResources?.Tab15ListDown;
					case ShiftTopTabType.SupportMaterials:
						return _resourceManager.GraphicResources?.Tab16ListDown;
					case ShiftTopTabType.SpecBuilder:
						return _resourceManager.GraphicResources?.Tab11ListDown;
					case ShiftTopTabType.Approach:
						return _resourceManager.GraphicResources?.Tab9ListDown;
					case ShiftTopTabType.ROI:
						return _resourceManager.GraphicResources?.Tab13ListDown;
					default:
						throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
		}

		public Image ListPopupImage
		{
			get
			{
				switch (TopTabType)
				{
					case ShiftTopTabType.Cover:
						return _resourceManager.GraphicResources?.Tab1ListPopup;
					case ShiftTopTabType.Intro:
						return _resourceManager.GraphicResources?.Tab2ListPopup;
					case ShiftTopTabType.Agenda:
						return _resourceManager.GraphicResources?.Tab3ListPopup;
					case ShiftTopTabType.Goals:
						return _resourceManager.GraphicResources?.Tab4ListPopup;
					case ShiftTopTabType.Market:
						return _resourceManager.GraphicResources?.Tab5ListPopup;
					case ShiftTopTabType.Partnership:
						return _resourceManager.GraphicResources?.Tab6ListPopup;
					case ShiftTopTabType.NeedsSolutions:
						return _resourceManager.GraphicResources?.Tab7ListPopup;
					case ShiftTopTabType.CBC:
						return _resourceManager.GraphicResources?.Tab8ListPopup;
					case ShiftTopTabType.IntegratedSolution:
						return _resourceManager.GraphicResources?.Tab10ListPopup;
					case ShiftTopTabType.Investment:
						return _resourceManager.GraphicResources?.Tab12ListPopup;
					case ShiftTopTabType.NextSteps:
						return _resourceManager.GraphicResources?.Tab14ListPopup;
					case ShiftTopTabType.Contract:
						return _resourceManager.GraphicResources?.Tab15ListPopup;
					case ShiftTopTabType.SupportMaterials:
						return _resourceManager.GraphicResources?.Tab16ListPopup;
					case ShiftTopTabType.SpecBuilder:
						return _resourceManager.GraphicResources?.Tab11ListPopup;
					case ShiftTopTabType.Approach:
						return _resourceManager.GraphicResources?.Tab9ListPopup;
					case ShiftTopTabType.ROI:
						return _resourceManager.GraphicResources?.Tab13ListPopup;
					default:
						throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
		}

		protected ShiftChildTabInfo(ShiftChildTabType tabType, ShiftTopTabType topTabType)
		{
			TabType = tabType;
			TopTabType = topTabType;
			EnableOutput = true;
			CommonEditorConfiguration = TextEditorConfiguration.Empty();
		}
	}
}
