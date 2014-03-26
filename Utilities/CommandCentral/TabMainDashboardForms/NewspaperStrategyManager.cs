using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommandCentral.CommonClasses;
using CommandCentral.CommonClasses.NewspaperViewSettings;
using CommandCentral.InteropClasses;

namespace CommandCentral.TabMainDashboard
{
	internal class NewspaperStrategyManager
	{
		private const string SourceFileName = @"Data\!Main_Dashboard\Newspaper Source\Print Strategy.xls";
		private const string DestinationFileName = @"Data\!Main_Dashboard\Newspaper XML\Print Strategy.xml";

		public const string ButtonText = "Print Strategy\nData";

		public static void ViewDataFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, DestinationFileName));
		}

		public static void ViewSourceFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SourceFileName));
		}

		public static void UpdateData()
		{
			bool tempBool;
			int tempInt;
			bool allowToLoad;


			var slideHeaders = new List<SlideHeader>();
			var publications = new List<Publication>();
			var adSizes = new List<AdSize>();
			var notes = new List<NameCodePair>();
			var clientTypes = new List<string>();
			var sections = new List<Section>();
			var mechanicals = new List<MechanicalType>();
			var deadlines = new List<string>();
			var statuses = new List<SlideHeader>();
			var shareUnits = new List<ShareUnit>();
			string selectedNotesBorderValue = string.Empty;
			string selectedSectionsBorderValue = string.Empty;

			var defaultHomeViewSettings = new HomeViewSettings();

			var defaultPrintScheduleViewSettings = new PrintScheduleViewSettings();

			var defaultPublicationBasicOverviewSettings = new PublicationBasicOverviewSettings();
			var defaultPublicationMultiSummarySettings = new PublicationMultiSummarySettings();
			var defaultSnapshotViewSettings = new SnapshotViewSettings();

			var defaultDetailedGridColumnState = new GridColumnsState();
			var defaultDetailedGridAdNotesState = new AdNotesState();
			var defaultDetailedGridSlideBulletsState = new SlideBulletsState();
			var defaultDetailedGridSlideHeaderState = new SlideHeaderState();

			var defaultMultiGridColumnState = new GridColumnsState();
			var defaultMultiGridAdNotesState = new AdNotesState();
			var defaultMultiGridSlideBulletsState = new SlideBulletsState();
			var defaultMultiGridSlideHeaderState = new SlideHeaderState();

			var defaultCalendarViewSettings = new CalendarViewSettings();

			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn't open source file");
				return;
			}

			if (connection.State == ConnectionState.Open)
			{
				DataTable dataTable;

				#region Load Headers
				OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
				dataTable = new DataTable();

				bool loadHeaders = false;
				slideHeaders.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Ad Schedule Slide Headers"))
							{
								loadHeaders = true;
								continue;
							}

							if (loadHeaders)
							{
								var title = new SlideHeader();
								title.Value = row[0].ToString().Trim();
								if (dataTable.Columns.Count > 1)
									if (row[1] != null)
										title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
								if (!string.IsNullOrEmpty(title.Value))
									slideHeaders.Add(title);
							}
						}

					slideHeaders.Sort((x, y) =>
					{
						int result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Statuses
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
				dataTable = new DataTable();

				loadHeaders = false;
				statuses.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*File-Status"))
							{
								loadHeaders = true;
								continue;
							}

							if (loadHeaders)
							{
								var status = new SlideHeader();
								status.Value = row[0].ToString().Trim();
								if (dataTable.Columns.Count > 1)
									if (row[1] != null)
										status.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
								if (!string.IsNullOrEmpty(status.Value))
									statuses.Add(status);
							}
						}

					statuses.Sort((x, y) =>
					{
						int result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = 1;
						return result;
					});
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Publications
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Publications$]", connection);
				dataTable = new DataTable();

				bool loadPublications = false;
				publications.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 16)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("User-Entry"))
							{
								loadPublications = true;
								continue;
							}

							if (loadPublications)
							{
								var publication = new Publication();
								publication.Name = row[0].ToString().Trim();
								if (row[1] != null)
									publication.SortOrder = row[1].ToString().Trim();
								if (row[2] != null)
									publication.Abbreviation = row[2].ToString().Trim();
								if (row[3] != null)
									publication.BigLogo = row[3].ToString().Trim();
								if (row[4] != null)
									publication.LittleLogo = row[4].ToString().Trim();
								if (row[5] != null)
									publication.TinyLogo = row[5].ToString().Trim();
								if (row[6] != null)
									publication.DailyCirculation = row[6].ToString().Trim();
								if (row[7] != null)
									publication.DailyReadership = row[7].ToString().Trim();
								if (row[8] != null)
									publication.SundayCirculation = row[8].ToString().Trim();
								if (row[9] != null)
									publication.SundayReadership = row[9].ToString().Trim();
								if (row[10] != null)
									publication.AllowSundaySelect = row[10].ToString().Trim().ToLower().Equals("y");
								if (row[11] != null)
									publication.AllowMondaySelect = row[11].ToString().Trim().ToLower().Equals("y");
								if (row[12] != null)
									publication.AllowTuesdaySelect = row[12].ToString().Trim().ToLower().Equals("y");
								if (row[13] != null)
									publication.AllowWednesdaySelect = row[13].ToString().Trim().ToLower().Equals("y");
								if (row[14] != null)
									publication.AllowThursdaySelect = row[14].ToString().Trim().ToLower().Equals("y");
								if (row[15] != null)
									publication.AllowFridaySelect = row[15].ToString().Trim().ToLower().Equals("y");
								if (row[16] != null)
									publication.AllowSaturdaySelect = row[16].ToString().Trim().ToLower().Equals("y");
								if (!string.IsNullOrEmpty(publication.Name))
									publications.Add(publication);
							}
						}
					publications.Sort((x, y) => x.SortOrder.CompareTo(y.SortOrder) == 0 ? WinAPIHelper.StrCmpLogicalW(x.Name, y.Name) : (string.IsNullOrEmpty(x.SortOrder) ? "z" : x.SortOrder).CompareTo((string.IsNullOrEmpty(y.SortOrder) ? "z" : y.SortOrder)));
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load AdSizes
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Ad-Sizes$]", connection);
				dataTable = new DataTable();

				adSizes.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataColumn column in dataTable.Columns)
					{
						var groupName = String.Empty;
						foreach (DataRow row in dataTable.Rows)
						{
							var rowValue = row[column] as String;
							if (String.IsNullOrEmpty(rowValue)) break;
							if (rowValue.StartsWith("*"))
							{
								groupName = rowValue.Replace("*", String.Empty);
								continue;
							}
							if (String.IsNullOrEmpty(groupName)) continue;
							adSizes.Add(new AdSize { Group = groupName, Name = rowValue });
						}
					}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load ShareUnits
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Share-Units$]", connection);
				dataTable = new DataTable();

				bool loadShareUnits = false;
				shareUnits.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 6)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Rate Card"))
							{
								loadShareUnits = true;
								continue;
							}

							if (loadShareUnits)
							{
								var shareUnit = new ShareUnit();
								if (row[0] != null)
									shareUnit.RateCard = row[0].ToString().Trim();
								if (row[1] != null)
									shareUnit.PercentOfPage = row[1].ToString().Trim();
								if (row[2] != null)
									shareUnit.Width = row[2].ToString().Trim();
								if (row[3] != null)
									shareUnit.WidthMeasureUnit = row[3].ToString().Trim();
								if (row[4] != null)
									shareUnit.Height = row[4].ToString().Trim();
								if (row[5] != null)
									shareUnit.HeightMeasureUnit = row[5].ToString().Trim();
								if (!string.IsNullOrEmpty(shareUnit.RateCard))
									shareUnits.Add(shareUnit);
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Notes
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Notes$]", connection);
				dataTable = new DataTable();

				bool loadNotes = false;
				notes.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Notes Library"))
							{
								loadNotes = true;
								if (dataTable.Columns.Count >= 2)
									selectedNotesBorderValue = row[1].ToString().Trim();
								continue;
							}

							if (loadNotes)
							{
								var note = new NameCodePair();
								note.Name = row[0].ToString().Trim();
								if (dataTable.Columns.Count >= 2)
									note.Code = row[1].ToString().Trim();
								if (!string.IsNullOrEmpty(note.Name))
									notes.Add(note);
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Client Types
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Client Type$]", connection);
				dataTable = new DataTable();

				bool loadClientTypes = false;
				clientTypes.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Client Type"))
							{
								loadClientTypes = true;
								continue;
							}

							if (loadClientTypes)
							{
								string clientType = row[0].ToString().Trim();
								if (!string.IsNullOrEmpty(clientType))
									clientTypes.Add(clientType);
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Sections
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sections$]", connection);
				dataTable = new DataTable();

				bool loadSections = false;
				sections.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Sections"))
							{
								loadSections = true;
								if (dataTable.Columns.Count >= 2)
									selectedSectionsBorderValue = row[1].ToString().Trim();
								continue;
							}

							if (loadSections)
							{
								var section = new Section();
								section.Name = row[0].ToString().Trim();
								if (dataTable.Columns.Count > 1)
									if (row[1] != null)
										section.Abbreviation = row[1].ToString().Trim();
								if (!string.IsNullOrEmpty(section.Name))
									sections.Add(section);
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Mechanicals
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Mechanicals$]", connection);
				dataTable = new DataTable();

				mechanicals.Clear();
				MechanicalType mechanicalType = null;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Contains("*") && !row[0].ToString().Trim().Contains("*Comment:"))
							{
								if (mechanicalType != null)
									mechanicals.Add(mechanicalType);
								mechanicalType = new MechanicalType();
								mechanicalType.Name = row[0].ToString().Replace("*", "");
								continue;
							}

							if (mechanicalType != null)
							{
								var mechanicalItem = new MechanicalItem();
								mechanicalItem.Name = row[0].ToString().Trim();
								if (dataTable.Columns.Count > 1)
									if (row[1] != null)
										mechanicalItem.Value = row[1].ToString().Trim();
								if (!string.IsNullOrEmpty(mechanicalItem.Name))
									mechanicalType.Items.Add(mechanicalItem);
							}
						}
					if (mechanicalType != null)
						mechanicals.Add(mechanicalType);
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Deadlines
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Deadlines$]", connection);
				dataTable = new DataTable();

				bool loadDeadlines = false;
				deadlines.Clear();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Deadlines"))
							{
								loadDeadlines = true;
								continue;
							}

							if (loadDeadlines)
							{
								string deadline = row[0].ToString().Trim();
								if (!string.IsNullOrEmpty(deadline))
									deadlines.Add(deadline);
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Default Home Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [HOME$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();
							switch (header)
							{
								case "Acct #":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.EnableAccountNumber = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.ShowAccountNumber = tempBool;
									break;
								case "Logo":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.EnableLogo = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.ShowLogo = tempBool;
									break;
								case "Code":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.EnableCode = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.ShowCode = tempBool;
									break;
								case "Delivery":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.EnableDelivery = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.ShowDelivery = tempBool;
									break;
								case "Readership":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.EnableReadership = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultHomeViewSettings.ShowReadership = tempBool;
									break;
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Default Print Schedule Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Print Schedule$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();
							switch (header)
							{
								case "Column Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnablePCI = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultPCI = tempBool;
									break;
								case "Flat":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableFlat = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultFlat = tempBool;
									break;
								case "Share":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableShare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultShare = tempBool;
									break;
								case "BW":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableBlackWhite = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultBlackWhite = tempBool;
									break;
								case "Spot Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableSpotColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultSpotColor = tempBool;
									break;
								case "Full Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableFullColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultFullColor = tempBool;
									break;
								case "Per Ad":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableCostPerAd = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultCostPerAd = tempBool;
									break;
								case "% of Ad":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnablePercentOfAd = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultPercentOfAd = tempBool;
									break;
								case "Included":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableColorIncluded = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultColorIncluded = tempBool;
									break;
								case "PCI":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.EnableCostPerInch = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPrintScheduleViewSettings.DefaultCostPerInch = tempBool;
									break;
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Default Publication BasicOverview Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Overview$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();
							switch (header)
							{
								case "Column Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowSquare = tempBool;
									break;
								case "Columns X Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableDimensions = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowDimensions = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowPercentOfPage = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowPageSize = tempBool;
									break;
								case "Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowColor = tempBool;
									break;
								case "Total Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableTotalInserts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowTotalInserts = tempBool;
									break;
								case "Total Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableTotalSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowTotalSquare = tempBool;
									break;
								case "Avg Ad Rate":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableAvgAdCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowAvgAdCost = tempBool;
									break;
								case "Avg PCI":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableAvgPCI = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowAvgPCI = tempBool;
									break;
								case "Total Discounts":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableDiscounts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowDiscounts = tempBool;
									break;
								case "Total Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableInvestment = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowInvestment = tempBool;
									break;
								case "Flight Dates":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableFlightDates2 = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowFlightDates2 = tempBool;
									break;
								case "Specific Days":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableDates = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowDates = tempBool;
									break;
								case "Custom Field":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.EnableComments = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationBasicOverviewSettings.ShowComments = tempBool;
									break;
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Default Publication Multi Summary Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Analysis$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();
							switch (header)
							{
								case "Total Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableTotalInserts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowTotalInserts = tempBool;
									break;
								case "Dimensions":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableDimensions = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowDimensions = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowPageSize = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowPercentOfPage = tempBool;
									break;
								case "Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableTotalColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowTotalColor = tempBool;
									break;
								case "BW Avg Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableAvgAdCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowAvgAdCost = tempBool;
									break;
								case "FINAL Avg Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableAvgFinalCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowAvgFinalCost = tempBool;
									break;
								case "Discounts":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableDiscounts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowDiscounts = tempBool;
									break;
								case "Sections":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableSection = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowSection = tempBool;
									break;
								case "Flight Dates":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableFlightDates = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowFlightDates = tempBool;
									break;
								case "Specific Days":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableDates = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowDates = tempBool;
									break;
								case "Custom Field":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.EnableComments = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultPublicationMultiSummarySettings.ShowComments = tempBool;
									break;
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Default Snapshot Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Snapshot$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();
							switch (header)
							{
								case "Logo":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableLogo = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowLogo = tempBool;
									break;
								case "Total Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableTotalInserts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowTotalInserts = tempBool;
									break;
								case "Investment":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableTotalFinalCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowTotalFinalCost = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowPageSize = tempBool;
									break;
								case "Columns X Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableDimensions = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowDimensions = tempBool;
									break;
								case "Total Column Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowSquare = tempBool;
									break;
								case "Total Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableTotalSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowTotalSquare = tempBool;
									break;
								case "Final Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableAvgFinalCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowAvgFinalCost = tempBool;
									break;
								case "Total Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableTotalColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowTotalColor = tempBool;
									break;
								case "Discounts":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableTotalDiscounts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowTotalDiscounts = tempBool;
									break;
								case "Readership":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableReadership = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowReadership = tempBool;
									break;
								case "Delivery":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnableDelivery = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowDelivery = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultSnapshotViewSettings.ShowPercentOfPage = tempBool;
									break;
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Detailed Grid Column State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Detailed Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*Print Grid Columns"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "ID":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableID = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowID = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.IDPosition = tempInt;
										break;
									case "INS#":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableIndex = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowIndex = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.IndexPosition = tempInt;
										break;
									case "Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowDate = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.DatePosition = tempInt;
										break;
									case "Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowColor = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.ColorPosition = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.SectionPosition = tempInt;
										break;
									case "PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnablePCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowPCI = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.PCIPosition = tempInt;
										break;
									case "Total Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowFinalCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.FinalCostPosition = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.PublicationPosition = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.PercentOfPagePosition = tempInt;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.CostPosition = tempInt;
										break;
									case "Col x In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.DimensionsPosition = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.MechanicalsPosition = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.DeliveryPosition = tempInt;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableDiscount = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowDiscount = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.DiscountPosition = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.PageSizePosition = tempInt;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.SquarePosition = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.DeadlinePosition = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridColumnState.ReadershipPosition = tempInt;
										break;
									case "Select up to 4 Ad-Notes":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.EnableAdNotes = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridColumnState.ShowAdNotes = tempBool;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Detailed Grid Ad Notes State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Detailed Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*AdNotes"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "Comment":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableComments = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowComments = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionComments = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionSection = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionMechanicals = tempInt;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionDimensions = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionDelivery = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionPublication = tempInt;
										break;
									case "Total Col In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionSquare = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionPageSize = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionPercentOfPage = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionReadership = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridAdNotesState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultDetailedGridAdNotesState.PositionDeadline = tempInt;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Detailed Grid Slide Bullets State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Detailed Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*Info Totals"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "Show Slide Totals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableSlideBullets = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowSlideBullets = tempBool;
										break;
									case "Last Slide":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableOnlyOnLastSlide = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowOnlyOnLastSlide = tempBool;
										break;
									case "Total Ads":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableTotalInserts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowTotalInserts = tempBool;
										break;
									case "Investment":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableTotalFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowTotalFinalCost = tempBool;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowPageSize = tempBool;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowDimensions = tempBool;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowPercentOfPage = tempBool;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableColumnInches = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowColumnInches = tempBool;
										break;
									case "Total Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableTotalSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowTotalSquare = tempBool;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableAvgAdCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowAvgAdCost = tempBool;
										break;
									case "Final Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableAvgFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowAvgFinalCost = tempBool;
										break;
									case "Avg PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableAvgPCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowAvgPCI = tempBool;
										break;
									case "Total Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableTotalColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowTotalColor = tempBool;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableDiscounts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowDiscounts = tempBool;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowDelivery = tempBool;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowReadership = tempBool;
										break;
									case "Show Signature":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.EnableSignature = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideBulletsState.ShowSignature = tempBool;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Detailed Grid Slide Header State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Detailed Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*Info Headers"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "Slide Header Options":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableSlideInfo = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowSlideInfo = tempBool;
										break;
									case "Slide Title":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableSlideHeader = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowSlideHeader = tempBool;
										break;
									case "Advertiser":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableAdvertiser = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowAdvertiser = tempBool;
										break;
									case "Decision Maker":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableDecisionMaker = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowDecisionMaker = tempBool;
										break;
									case "Presentation Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnablePresentationDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowPresentationDate = tempBool;
										break;
									case "Schedule Window":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableFlightDates = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowFlightDates = tempBool;
										break;
									case "Publication Name":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableName = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowName = tempBool;
										break;
									case "Publication Logo":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.EnableLogo1 = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultDetailedGridSlideHeaderState.ShowLogo1 = tempBool;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Multi Grid Column State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Logo Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*Print Grid Columns"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "ID":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableID = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowID = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.IDPosition = tempInt;
										break;
									case "INS#":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableIndex = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowIndex = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.IndexPosition = tempInt;
										break;
									case "Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowDate = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.DatePosition = tempInt;
										break;
									case "Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowColor = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.ColorPosition = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.SectionPosition = tempInt;
										break;
									case "PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnablePCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowPCI = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.PCIPosition = tempInt;
										break;
									case "Total Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowFinalCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.FinalCostPosition = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.PublicationPosition = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.PercentOfPagePosition = tempInt;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.CostPosition = tempInt;
										break;
									case "Col x In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.DimensionsPosition = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.MechanicalsPosition = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.DeliveryPosition = tempInt;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableDiscount = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowDiscount = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.DiscountPosition = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.PageSizePosition = tempInt;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.SquarePosition = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.DeadlinePosition = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridColumnState.ReadershipPosition = tempInt;
										break;
									case "Select up to 4 Ad-Notes":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.EnableAdNotes = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridColumnState.ShowAdNotes = tempBool;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Multi Grid Ad Notes State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Logo Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*AdNotes"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "Comment":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableComments = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowComments = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionComments = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionSection = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionMechanicals = tempInt;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionDimensions = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionDelivery = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionPublication = tempInt;
										break;
									case "Total Col In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionSquare = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionPageSize = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionPercentOfPage = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionReadership = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridAdNotesState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												defaultMultiGridAdNotesState.PositionDeadline = tempInt;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Multi Grid Slide Bullets State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Logo Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*Info Totals"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "Show Slide Totals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableSlideBullets = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowSlideBullets = tempBool;
										break;
									case "Last Slide":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableOnlyOnLastSlide = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowOnlyOnLastSlide = tempBool;
										break;
									case "Total Ads":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableTotalInserts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowTotalInserts = tempBool;
										break;
									case "Investment":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableTotalFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowTotalFinalCost = tempBool;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowPageSize = tempBool;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowDimensions = tempBool;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowPercentOfPage = tempBool;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableColumnInches = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowColumnInches = tempBool;
										break;
									case "Total Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableTotalSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowTotalSquare = tempBool;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableAvgAdCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowAvgAdCost = tempBool;
										break;
									case "Final Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableAvgFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowAvgFinalCost = tempBool;
										break;
									case "Avg PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableAvgPCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowAvgPCI = tempBool;
										break;
									case "Total Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableTotalColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowTotalColor = tempBool;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableDiscounts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowDiscounts = tempBool;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowDelivery = tempBool;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowReadership = tempBool;
										break;
									case "Show Signature":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.EnableSignature = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideBulletsState.ShowSignature = tempBool;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Multi Grid Slide Header State
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Logo Grid$]", connection);
				dataTable = new DataTable();

				allowToLoad = false;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();

							if (header.Contains("*"))
							{
								if (header.Equals("*Info Headers"))
									allowToLoad = true;
								else
									allowToLoad = false;
								continue;
							}

							if (allowToLoad)
							{
								switch (header)
								{
									case "Slide Header Options":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableSlideInfo = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowSlideInfo = tempBool;
										break;
									case "Slide Title":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableSlideHeader = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowSlideHeader = tempBool;
										break;
									case "Advertiser":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableAdvertiser = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowAdvertiser = tempBool;
										break;
									case "Decision Maker":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableDecisionMaker = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowDecisionMaker = tempBool;
										break;
									case "Presentation Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnablePresentationDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowPresentationDate = tempBool;
										break;
									case "Schedule Window":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableFlightDates = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowFlightDates = tempBool;
										break;
									case "Publication Name":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableName = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowName = tempBool;
										break;
									case "Publication Logo":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.EnableLogo1 = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												defaultMultiGridSlideHeaderState.ShowLogo1 = tempBool;
										break;
								}
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				#region Load Default Calendar Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Calendar$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string header = string.Empty;
							if (row[0] != null)
								header = row[0].ToString().Trim();
							switch (header)
							{
								case "Ad Section":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableSection = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowSection = tempBool;
									break;
								case "Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableAvgCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowAvgCost = tempBool;
									break;
								case "Color-BW":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowColor = tempBool;
									break;
								case "Codes Only":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableAbbreviationOnly = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowAbbreviationOnly = tempBool;
									break;
								case "Col x In":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableAdSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowAdSize = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowPageSize = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowPercentOfPage = tempBool;
									break;
								case "Big Dates":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableBigDate = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowBigDate = tempBool;
									break;
								case "Slide Title":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableTitle = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowTitle = tempBool;
									break;
								case "Logo":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableLogo = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowLogo = tempBool;
									break;
								case "Advertiser":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableBusinessName = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowBusinessName = tempBool;
									break;
								case "Contact":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableDecisionMaker = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowDecisionMaker = tempBool;
									break;
								case "Monthly $":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowCost = tempBool;
									break;
								case "Legend":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableLegend = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowLegend = tempBool;
									break;
								case "Avg Rate":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableAvgCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowAvgCost = tempBool;
									break;
								case "Comment":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableComments = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowComments = tempBool;
									break;
								case "# Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableTotalAds = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowTotalAds = tempBool;
									break;
								case "# Days":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableActiveDays = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.ShowActiveDays = tempBool;
									break;
								case "Gray":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableGray = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												defaultCalendarViewSettings.SlideColor = "gray";
									break;
								case "Black":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableBlack = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												defaultCalendarViewSettings.SlideColor = "black";
									break;
								case "Blue":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableBlue = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												defaultCalendarViewSettings.SlideColor = "blue";
									break;
								case "Teal":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableTeal = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												defaultCalendarViewSettings.SlideColor = "teal";
									break;
								case "Orange":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableOrange = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												defaultCalendarViewSettings.SlideColor = "orange";
									break;
								case "Green":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											defaultCalendarViewSettings.EnableGreen = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												defaultCalendarViewSettings.SlideColor = "green";
									break;
							}
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				#endregion

				connection.Close();

				#region Save XML
				var xml = new StringBuilder();
				xml.AppendLine("<PrintStrategy>");
				foreach (SlideHeader header in slideHeaders)
				{
					xml.Append(@"<Header ");
					xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (SlideHeader status in statuses)
				{
					xml.Append(@"<Status ");
					xml.Append("Value = \"" + status.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (Publication publication in publications)
				{
					xml.Append(@"<Publication ");
					xml.Append("Name = \"" + publication.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Abbreviation = \"" + publication.Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("BigLogo = \"" + publication.BigLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("LittleLogo = \"" + publication.LittleLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("TinyLogo = \"" + publication.TinyLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("DailyCirculation = \"" + publication.DailyCirculation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("DailyReadership = \"" + publication.DailyReadership.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("SundayCirculation = \"" + publication.SundayCirculation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("SundayReadership = \"" + publication.SundayReadership.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("AllowSundaySelect = \"" + publication.AllowSundaySelect + "\" ");
					xml.Append("AllowMondaySelect = \"" + publication.AllowMondaySelect + "\" ");
					xml.Append("AllowTuesdaySelect = \"" + publication.AllowTuesdaySelect + "\" ");
					xml.Append("AllowWednesdaySelect = \"" + publication.AllowWednesdaySelect + "\" ");
					xml.Append("AllowThursdaySelect = \"" + publication.AllowThursdaySelect + "\" ");
					xml.Append("AllowFridaySelect = \"" + publication.AllowFridaySelect + "\" ");
					xml.Append("AllowSaturdaySelect = \"" + publication.AllowSaturdaySelect + "\" ");
					xml.AppendLine(@"/>");
				}

				foreach (AdSize adSize in adSizes)
				{
					xml.Append(@"<AdSize ");
					xml.Append("Group = \"" + adSize.Group.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Name = \"" + adSize.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (ShareUnit shareUnit in shareUnits)
				{
					xml.Append(@"<ShareUnit ");
					xml.Append("RateCard = \"" + shareUnit.RateCard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("PercentOfPage = \"" + shareUnit.PercentOfPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Width = \"" + shareUnit.Width.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("WidthMeasureUnit = \"" + shareUnit.WidthMeasureUnit.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Height = \"" + shareUnit.Height.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("HeightMeasureUnit = \"" + shareUnit.HeightMeasureUnit.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (NameCodePair note in notes)
				{
					xml.Append(@"<Note ");
					xml.Append("Value = \"" + note.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Code = \"" + note.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"<SelectedNotesBorderValue>" + selectedNotesBorderValue.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedNotesBorderValue>");

				foreach (string clientType in clientTypes)
				{
					xml.Append(@"<ClientType ");
					xml.Append("Value = \"" + clientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (Section section in sections)
				{
					xml.Append(@"<Section ");
					xml.Append("Name = \"" + section.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Abbreviation = \"" + section.Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"<SelectedSectionsBorderValue>" + selectedSectionsBorderValue.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSectionsBorderValue>");

				foreach (MechanicalType mechanical in mechanicals)
				{
					xml.Append(@"<Mechanicals ");
					xml.Append("Name = \"" + mechanical.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@">");
					foreach (MechanicalItem mechanicalItem in mechanical.Items)
					{
						xml.Append(@"<Mechanical ");
						xml.Append("Name = \"" + mechanicalItem.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
						xml.Append("Value = \"" + mechanicalItem.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
						xml.AppendLine(@"/>");
					}
					xml.AppendLine(@"</Mechanicals>");
				}
				foreach (string deadline in deadlines)
				{
					xml.Append(@"<Deadline ");
					xml.Append("Value = \"" + deadline.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}

				xml.AppendLine(@"<DefaultHomeViewSettings>" + defaultHomeViewSettings.Serialize() + @"</DefaultHomeViewSettings>");

				xml.AppendLine(@"<DefaultPrintScheduleViewSettings>" + defaultPrintScheduleViewSettings.Serialize() + @"</DefaultPrintScheduleViewSettings>");

				xml.AppendLine(@"<DefaultPublicationBasicOverviewSettings>" + defaultPublicationBasicOverviewSettings.Serialize() + @"</DefaultPublicationBasicOverviewSettings>");
				xml.AppendLine(@"<DefaultPublicationMultiSummarySettings>" + defaultPublicationMultiSummarySettings.Serialize() + @"</DefaultPublicationMultiSummarySettings>");
				xml.AppendLine(@"<DefaultSnapshotViewSettings>" + defaultSnapshotViewSettings.Serialize() + @"</DefaultSnapshotViewSettings>");

				xml.AppendLine(@"<DefaultDetailedGridColumnState>" + defaultDetailedGridColumnState.Serialize() + @"</DefaultDetailedGridColumnState>");
				xml.AppendLine(@"<DefaultDetailedGridAdNotesState>" + defaultDetailedGridAdNotesState.Serialize() + @"</DefaultDetailedGridAdNotesState>");
				xml.AppendLine(@"<DefaultDetailedGridSlideBulletsState>" + defaultDetailedGridSlideBulletsState.Serialize() + @"</DefaultDetailedGridSlideBulletsState>");
				xml.AppendLine(@"<DefaultDetailedGridSlideHeaderState>" + defaultDetailedGridSlideHeaderState.Serialize() + @"</DefaultDetailedGridSlideHeaderState>");

				xml.AppendLine(@"<DefaultMultiGridColumnState>" + defaultMultiGridColumnState.Serialize() + @"</DefaultMultiGridColumnState>");
				xml.AppendLine(@"<DefaultMultiGridAdNotesState>" + defaultMultiGridAdNotesState.Serialize() + @"</DefaultMultiGridAdNotesState>");
				xml.AppendLine(@"<DefaultMultiGridSlideBulletsState>" + defaultMultiGridSlideBulletsState.Serialize() + @"</DefaultMultiGridSlideBulletsState>");
				xml.AppendLine(@"<DefaultMultiGridSlideHeaderState>" + defaultMultiGridSlideHeaderState.Serialize() + @"</DefaultMultiGridSlideHeaderState>");

				xml.AppendLine(@"<DefaultCalendarViewSettings>" + defaultCalendarViewSettings.Serialize() + @"</DefaultCalendarViewSettings>");

				xml.AppendLine(@"</PrintStrategy>");

				string xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
				using (var sw = new StreamWriter(xmlPath, false))
				{
					sw.Write(xml.ToString());
					sw.Flush();
				}
				#endregion

				AppManager.Instance.ShowInformation("Data was updated.");
			}
		}
	}
}