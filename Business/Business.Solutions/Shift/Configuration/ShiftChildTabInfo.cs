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
					case ShiftTopTabType.Contract:
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
					case ShiftTopTabType.SupportMaterials:
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
					case ShiftTopTabType.Contract:
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
					case ShiftTopTabType.SupportMaterials:
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
								return _resourceManager.GraphicResources?.Tab1_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab1_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab1_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab1_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab1_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab1_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab1_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab1_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab1_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab1_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab1_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab1_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab1_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab1_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab1_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab1_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Intro:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab2_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab2_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab2_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab2_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab2_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab2_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab2_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab2_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab2_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab2_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab2_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab2_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab2_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab2_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab2_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab2_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Agenda:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab3_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab3_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab3_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab3_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab3_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab3_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab3_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab3_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab3_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab3_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab3_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab3_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab3_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab3_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab3_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab3_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Goals:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab4_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab4_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab4_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab4_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab4_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab4_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab4_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab4_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab4_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab4_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab4_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab4_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab4_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab4_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab4_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab4_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Market:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab5_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab5_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab5_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab5_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab5_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab5_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab5_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab5_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab5_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab5_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab5_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab5_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab5_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab5_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab5_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab5_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Partnership:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab6_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab6_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab6_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab6_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab6_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab6_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab6_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab6_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab6_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab6_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab6_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab6_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab6_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab6_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab6_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab6_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NeedsSolutions:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab7_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab7_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab7_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab7_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab7_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab7_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab7_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab7_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab7_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab7_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab7_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab7_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab7_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab7_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab7_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab7_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.CBC:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab8_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab8_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab8_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab8_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab8_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab8_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab8_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab8_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab8_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab8_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab8_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab8_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab8_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab8_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab8_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab8_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.IntegratedSolution:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab10_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab10_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab10_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab10_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab10_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab10_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab10_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab10_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab10_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab10_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab10_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab10_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab10_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab10_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab10_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab10_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Investment:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab12_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab12_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab12_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab12_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab12_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab12_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab12_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab12_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab12_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab12_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab12_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab12_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab12_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab12_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab12_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab12_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.NextSteps:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab13_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab13_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab13_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab13_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab13_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab13_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab13_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab13_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab13_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab13_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab13_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab13_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab13_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab13_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab13_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab13_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Contract:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab14_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab14_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab14_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab14_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab14_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab14_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab14_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab14_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab14_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab14_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab14_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab14_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab14_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab14_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab14_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab14_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SupportMaterials:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab15_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab15_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab15_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab15_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab15_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab15_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab15_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab15_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab15_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab15_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab15_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab15_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab15_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab15_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab15_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab15_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.SpecBuilder:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab11_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab11_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab11_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab11_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab11_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab11_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab11_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab11_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab11_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab11_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab11_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab11_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab11_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab11_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab11_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab11_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
					case ShiftTopTabType.Approach:
						switch (TabType)
						{
							case ShiftChildTabType.A:
								return _resourceManager.GraphicResources?.Tab9_A_Backgroound;
							case ShiftChildTabType.B:
								return _resourceManager.GraphicResources?.Tab9_B_Backgroound;
							case ShiftChildTabType.C:
								return _resourceManager.GraphicResources?.Tab9_C_Backgroound;
							case ShiftChildTabType.D:
								return _resourceManager.GraphicResources?.Tab9_D_Backgroound;
							case ShiftChildTabType.E:
								return _resourceManager.GraphicResources?.Tab9_E_Backgroound;
							case ShiftChildTabType.F:
								return _resourceManager.GraphicResources?.Tab9_F_Backgroound;
							case ShiftChildTabType.G:
								return _resourceManager.GraphicResources?.Tab9_G_Backgroound;
							case ShiftChildTabType.H:
								return _resourceManager.GraphicResources?.Tab9_H_Backgroound;
							case ShiftChildTabType.I:
								return _resourceManager.GraphicResources?.Tab9_I_Backgroound;
							case ShiftChildTabType.J:
								return _resourceManager.GraphicResources?.Tab9_J_Backgroound;
							case ShiftChildTabType.U:
								return _resourceManager.GraphicResources?.Tab9_U_Backgroound;
							case ShiftChildTabType.V:
								return _resourceManager.GraphicResources?.Tab9_V_Backgroound;
							case ShiftChildTabType.W:
								return _resourceManager.GraphicResources?.Tab9_W_Backgroound;
							case ShiftChildTabType.X:
								return _resourceManager.GraphicResources?.Tab9_X_Backgroound;
							case ShiftChildTabType.Y:
								return _resourceManager.GraphicResources?.Tab9_Y_Backgroound;
							case ShiftChildTabType.Z:
								return _resourceManager.GraphicResources?.Tab9_Z_Backgroound;
							default:
								throw new ArgumentOutOfRangeException("Shift tab type is not defined");
						}
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
