using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommandCentral.CommonClasses;
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

			var _defaultHomeViewSettings = new HomeViewSettings();

			var _defaultPrintScheduleViewSettings = new PrintScheduleViewSettings();

			var _defaultPublicationBasicOverviewSettings = new PublicationBasicOverviewSettings();
			var _defaultPublicationMultiSummarySettings = new PublicationMultiSummarySettings();
			var _defaultSnapshotViewSettings = new SnapshotViewSettings();

			var _defaultDetailedGridColumnState = new GridColumnsState();
			var _defaultDetailedGridAdNotesState = new AdNotesState();
			var _defaultDetailedGridSlideBulletsState = new SlideBulletsState();
			var _defaultDetailedGridSlideHeaderState = new SlideHeaderState();

			var _defaultMultiGridColumnState = new GridColumnsState();
			var _defaultMultiGridAdNotesState = new AdNotesState();
			var _defaultMultiGridSlideBulletsState = new SlideBulletsState();
			var _defaultMultiGridSlideHeaderState = new SlideHeaderState();

			var _defaultCalendarViewSettings = new CalendarViewSettings();

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
											_defaultHomeViewSettings.EnableAccountNumber = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowAccountNumber = tempBool;
									break;
								case "Sales Call Type Person":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableSalesStrategyPerson = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowSalesStrategyPerson = tempBool;
									break;
								case "Sales Call Type Email":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableSalesStrategyEmail = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowSalesStrategyEmail = tempBool;
									break;
								case "Sales Call Type Fax":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableSalesStrategyFax = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowSalesStrategyFax = tempBool;
									break;
								case "Logo":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableLogo = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowLogo = tempBool;
									break;
								case "Code":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableCode = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowCode = tempBool;
									break;
								case "Delivery":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableDelivery = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowDelivery = tempBool;
									break;
								case "Readership":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.EnableReadership = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultHomeViewSettings.ShowReadership = tempBool;
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
											_defaultPrintScheduleViewSettings.EnablePCI = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultPCI = tempBool;
									break;
								case "Flat":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableFlat = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultFlat = tempBool;
									break;
								case "Share":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableShare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultShare = tempBool;
									break;
								case "BW":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableBlackWhite = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultBlackWhite = tempBool;
									break;
								case "Spot Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableSpotColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultSpotColor = tempBool;
									break;
								case "Full Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableFullColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultFullColor = tempBool;
									break;
								case "Per Ad":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableCostPerAd = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultCostPerAd = tempBool;
									break;
								case "% of Ad":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnablePercentOfAd = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultPercentOfAd = tempBool;
									break;
								case "Included":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableColorIncluded = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultColorIncluded = tempBool;
									break;
								case "PCI":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.EnableCostPerInch = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPrintScheduleViewSettings.DefaultCostPerInch = tempBool;
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
											_defaultPublicationBasicOverviewSettings.EnableSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowSquare = tempBool;
									break;
								case "Columns X Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableDimensions = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowDimensions = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowPercentOfPage = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowPageSize = tempBool;
									break;
								case "Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowColor = tempBool;
									break;
								case "Total Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableTotalInserts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowTotalInserts = tempBool;
									break;
								case "Total Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableTotalSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowTotalSquare = tempBool;
									break;
								case "Avg Ad Rate":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableAvgAdCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowAvgAdCost = tempBool;
									break;
								case "Avg PCI":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableAvgPCI = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowAvgPCI = tempBool;
									break;
								case "Total Discounts":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableDiscounts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowDiscounts = tempBool;
									break;
								case "Total Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableInvestment = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowInvestment = tempBool;
									break;
								case "Flight Dates":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableFlightDates2 = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowFlightDates2 = tempBool;
									break;
								case "Specific Days":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableDates = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowDates = tempBool;
									break;
								case "Custom Field":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.EnableComments = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationBasicOverviewSettings.ShowComments = tempBool;
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
											_defaultPublicationMultiSummarySettings.EnableTotalInserts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowTotalInserts = tempBool;
									break;
								case "Dimensions":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableDimensions = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowDimensions = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowPageSize = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowPercentOfPage = tempBool;
									break;
								case "Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableTotalColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowTotalColor = tempBool;
									break;
								case "BW Avg Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableAvgAdCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowAvgAdCost = tempBool;
									break;
								case "FINAL Avg Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableAvgFinalCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowAvgFinalCost = tempBool;
									break;
								case "Discounts":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableDiscounts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowDiscounts = tempBool;
									break;
								case "Sections":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableSection = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowSection = tempBool;
									break;
								case "Flight Dates":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableFlightDates = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowFlightDates = tempBool;
									break;
								case "Specific Days":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableDates = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowDates = tempBool;
									break;
								case "Custom Field":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.EnableComments = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultPublicationMultiSummarySettings.ShowComments = tempBool;
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
											_defaultSnapshotViewSettings.EnableLogo = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowLogo = tempBool;
									break;
								case "Total Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableTotalInserts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowTotalInserts = tempBool;
									break;
								case "Investment":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableTotalFinalCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowTotalFinalCost = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowPageSize = tempBool;
									break;
								case "Columns X Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableDimensions = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowDimensions = tempBool;
									break;
								case "Total Column Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowSquare = tempBool;
									break;
								case "Total Inches":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableTotalSquare = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowTotalSquare = tempBool;
									break;
								case "Final Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableAvgFinalCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowAvgFinalCost = tempBool;
									break;
								case "Total Color":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableTotalColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowTotalColor = tempBool;
									break;
								case "Discounts":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableTotalDiscounts = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowTotalDiscounts = tempBool;
									break;
								case "Readership":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableReadership = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowReadership = tempBool;
									break;
								case "Delivery":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnableDelivery = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowDelivery = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultSnapshotViewSettings.ShowPercentOfPage = tempBool;
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
												_defaultDetailedGridColumnState.EnableID = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowID = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.IDPosition = tempInt;
										break;
									case "INS#":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableIndex = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowIndex = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.IndexPosition = tempInt;
										break;
									case "Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowDate = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.DatePosition = tempInt;
										break;
									case "Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowColor = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.ColorPosition = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.SectionPosition = tempInt;
										break;
									case "PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnablePCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowPCI = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.PCIPosition = tempInt;
										break;
									case "Total Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowFinalCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.FinalCostPosition = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.PublicationPosition = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.PercentOfPagePosition = tempInt;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.CostPosition = tempInt;
										break;
									case "Col x In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.DimensionsPosition = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.MechanicalsPosition = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.DeliveryPosition = tempInt;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableDiscount = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowDiscount = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.DiscountPosition = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.PageSizePosition = tempInt;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.SquarePosition = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.DeadlinePosition = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridColumnState.ReadershipPosition = tempInt;
										break;
									case "Select up to 4 Ad-Notes":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.EnableAdNotes = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridColumnState.ShowAdNotes = tempBool;
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
												_defaultDetailedGridAdNotesState.EnableComments = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowComments = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionComments = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionSection = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionMechanicals = tempInt;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionDimensions = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionDelivery = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionPublication = tempInt;
										break;
									case "Total Col In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionSquare = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionPageSize = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionPercentOfPage = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionReadership = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridAdNotesState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultDetailedGridAdNotesState.PositionDeadline = tempInt;
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
												_defaultDetailedGridSlideBulletsState.EnableSlideBullets = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowSlideBullets = tempBool;
										break;
									case "Last Slide":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableOnlyOnLastSlide = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowOnlyOnLastSlide = tempBool;
										break;
									case "Total Ads":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableTotalInserts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowTotalInserts = tempBool;
										break;
									case "Investment":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableTotalFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowTotalFinalCost = tempBool;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowPageSize = tempBool;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowDimensions = tempBool;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowPercentOfPage = tempBool;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableColumnInches = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowColumnInches = tempBool;
										break;
									case "Total Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableTotalSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowTotalSquare = tempBool;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableAvgAdCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowAvgAdCost = tempBool;
										break;
									case "Final Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableAvgFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowAvgFinalCost = tempBool;
										break;
									case "Avg PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableAvgPCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowAvgPCI = tempBool;
										break;
									case "Total Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableTotalColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowTotalColor = tempBool;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableDiscounts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowDiscounts = tempBool;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowDelivery = tempBool;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowReadership = tempBool;
										break;
									case "Show Signature":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.EnableSignature = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideBulletsState.ShowSignature = tempBool;
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
												_defaultDetailedGridSlideHeaderState.EnableSlideInfo = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowSlideInfo = tempBool;
										break;
									case "Slide Title":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnableSlideHeader = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowSlideHeader = tempBool;
										break;
									case "Advertiser":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnableAdvertiser = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowAdvertiser = tempBool;
										break;
									case "Decision Maker":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnableDecisionMaker = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowDecisionMaker = tempBool;
										break;
									case "Presentation Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnablePresentationDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowPresentationDate = tempBool;
										break;
									case "Schedule Window":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnableFlightDates = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowFlightDates = tempBool;
										break;
									case "Publication Name":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnableName = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowName = tempBool;
										break;
									case "Publication Logo":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.EnableLogo1 = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultDetailedGridSlideHeaderState.ShowLogo1 = tempBool;
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
												_defaultMultiGridColumnState.EnableID = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowID = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.IDPosition = tempInt;
										break;
									case "INS#":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableIndex = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowIndex = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.IndexPosition = tempInt;
										break;
									case "Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowDate = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.DatePosition = tempInt;
										break;
									case "Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowColor = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.ColorPosition = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.SectionPosition = tempInt;
										break;
									case "PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnablePCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowPCI = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.PCIPosition = tempInt;
										break;
									case "Total Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowFinalCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.FinalCostPosition = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.PublicationPosition = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.PercentOfPagePosition = tempInt;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowCost = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.CostPosition = tempInt;
										break;
									case "Col x In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.DimensionsPosition = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.MechanicalsPosition = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.DeliveryPosition = tempInt;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableDiscount = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowDiscount = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.DiscountPosition = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.PageSizePosition = tempInt;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.SquarePosition = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.DeadlinePosition = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridColumnState.ReadershipPosition = tempInt;
										break;
									case "Select up to 4 Ad-Notes":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.EnableAdNotes = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridColumnState.ShowAdNotes = tempBool;
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
												_defaultMultiGridAdNotesState.EnableComments = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowComments = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionComments = tempInt;
										break;
									case "Section":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableSection = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowSection = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionSection = tempInt;
										break;
									case "Mechanicals":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableMechanicals = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowMechanicals = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionMechanicals = tempInt;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowDimensions = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionDimensions = tempInt;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowDelivery = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionDelivery = tempInt;
										break;
									case "Publication":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnablePublication = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowPublication = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionPublication = tempInt;
										break;
									case "Total Col In":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowSquare = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionSquare = tempInt;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowPageSize = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionPageSize = tempInt;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowPercentOfPage = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionPercentOfPage = tempInt;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowReadership = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionReadership = tempInt;
										break;
									case "Deadline":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.EnableDeadline = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridAdNotesState.ShowDeadline = tempBool;
										if (row[3] != null)
											if (int.TryParse(row[3].ToString().Trim(), out tempInt))
												_defaultMultiGridAdNotesState.PositionDeadline = tempInt;
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
												_defaultMultiGridSlideBulletsState.EnableSlideBullets = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowSlideBullets = tempBool;
										break;
									case "Last Slide":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableOnlyOnLastSlide = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowOnlyOnLastSlide = tempBool;
										break;
									case "Total Ads":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableTotalInserts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowTotalInserts = tempBool;
										break;
									case "Investment":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableTotalFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowTotalFinalCost = tempBool;
										break;
									case "Page Size":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnablePageSize = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowPageSize = tempBool;
										break;
									case "Col. X Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableDimensions = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowDimensions = tempBool;
										break;
									case "% of Page":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnablePercentOfPage = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowPercentOfPage = tempBool;
										break;
									case "Total Col. In.":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableColumnInches = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowColumnInches = tempBool;
										break;
									case "Total Inches":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableTotalSquare = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowTotalSquare = tempBool;
										break;
									case "BW Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableAvgAdCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowAvgAdCost = tempBool;
										break;
									case "Final Ad Cost":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableAvgFinalCost = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowAvgFinalCost = tempBool;
										break;
									case "Avg PCI":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableAvgPCI = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowAvgPCI = tempBool;
										break;
									case "Total Color":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableTotalColor = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowTotalColor = tempBool;
										break;
									case "Discounts":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableDiscounts = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowDiscounts = tempBool;
										break;
									case "Delivery":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableDelivery = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowDelivery = tempBool;
										break;
									case "Readership":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableReadership = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowReadership = tempBool;
										break;
									case "Show Signature":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.EnableSignature = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideBulletsState.ShowSignature = tempBool;
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
												_defaultMultiGridSlideHeaderState.EnableSlideInfo = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowSlideInfo = tempBool;
										break;
									case "Slide Title":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnableSlideHeader = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowSlideHeader = tempBool;
										break;
									case "Advertiser":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnableAdvertiser = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowAdvertiser = tempBool;
										break;
									case "Decision Maker":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnableDecisionMaker = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowDecisionMaker = tempBool;
										break;
									case "Presentation Date":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnablePresentationDate = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowPresentationDate = tempBool;
										break;
									case "Schedule Window":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnableFlightDates = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowFlightDates = tempBool;
										break;
									case "Publication Name":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnableName = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowName = tempBool;
										break;
									case "Publication Logo":
										if (row[1] != null)
											if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.EnableLogo1 = tempBool;
										if (row[2] != null)
											if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
												_defaultMultiGridSlideHeaderState.ShowLogo1 = tempBool;
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
											_defaultCalendarViewSettings.EnableSection = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowSection = tempBool;
									break;
								case "Ad Cost":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableAvgCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowAvgCost = tempBool;
									break;
								case "Color-BW":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableColor = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowColor = tempBool;
									break;
								case "Codes Only":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableAbbreviationOnly = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowAbbreviationOnly = tempBool;
									break;
								case "Col x In":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableAdSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowAdSize = tempBool;
									break;
								case "Page Size":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnablePageSize = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowPageSize = tempBool;
									break;
								case "% of Page":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnablePercentOfPage = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowPercentOfPage = tempBool;
									break;
								case "Big Dates":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableBigDate = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowBigDate = tempBool;
									break;
								case "Slide Title":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableTitle = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowTitle = tempBool;
									break;
								case "Logo":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableLogo = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowLogo = tempBool;
									break;
								case "Advertiser":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableBusinessName = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowBusinessName = tempBool;
									break;
								case "Contact":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableDecisionMaker = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowDecisionMaker = tempBool;
									break;
								case "Monthly $":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowCost = tempBool;
									break;
								case "Legend":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableLegend = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowLegend = tempBool;
									break;
								case "Avg Rate":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableAvgCost = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowAvgCost = tempBool;
									break;
								case "Comment":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableComments = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowComments = tempBool;
									break;
								case "# Ads":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableTotalAds = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowTotalAds = tempBool;
									break;
								case "# Days":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableActiveDays = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.ShowActiveDays = tempBool;
									break;
								case "Gray":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableGray = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												_defaultCalendarViewSettings.SlideColor = "gray";
									break;
								case "Black":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableBlack = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												_defaultCalendarViewSettings.SlideColor = "black";
									break;
								case "Blue":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableBlue = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												_defaultCalendarViewSettings.SlideColor = "blue";
									break;
								case "Teal":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableTeal = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												_defaultCalendarViewSettings.SlideColor = "teal";
									break;
								case "Orange":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableOrange = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												_defaultCalendarViewSettings.SlideColor = "orange";
									break;
								case "Green":
									if (row[1] != null)
										if (bool.TryParse(row[1].ToString().Trim(), out tempBool))
											_defaultCalendarViewSettings.EnableGreen = tempBool;
									if (row[2] != null)
										if (bool.TryParse(row[2].ToString().Trim(), out tempBool))
											if (tempBool)
												_defaultCalendarViewSettings.SlideColor = "green";
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

				xml.AppendLine(@"<DefaultHomeViewSettings>" + _defaultHomeViewSettings.Serialize() + @"</DefaultHomeViewSettings>");

				xml.AppendLine(@"<DefaultPrintScheduleViewSettings>" + _defaultPrintScheduleViewSettings.Serialize() + @"</DefaultPrintScheduleViewSettings>");

				xml.AppendLine(@"<DefaultPublicationBasicOverviewSettings>" + _defaultPublicationBasicOverviewSettings.Serialize() + @"</DefaultPublicationBasicOverviewSettings>");
				xml.AppendLine(@"<DefaultPublicationMultiSummarySettings>" + _defaultPublicationMultiSummarySettings.Serialize() + @"</DefaultPublicationMultiSummarySettings>");
				xml.AppendLine(@"<DefaultSnapshotViewSettings>" + _defaultSnapshotViewSettings.Serialize() + @"</DefaultSnapshotViewSettings>");

				xml.AppendLine(@"<DefaultDetailedGridColumnState>" + _defaultDetailedGridColumnState.Serialize() + @"</DefaultDetailedGridColumnState>");
				xml.AppendLine(@"<DefaultDetailedGridAdNotesState>" + _defaultDetailedGridAdNotesState.Serialize() + @"</DefaultDetailedGridAdNotesState>");
				xml.AppendLine(@"<DefaultDetailedGridSlideBulletsState>" + _defaultDetailedGridSlideBulletsState.Serialize() + @"</DefaultDetailedGridSlideBulletsState>");
				xml.AppendLine(@"<DefaultDetailedGridSlideHeaderState>" + _defaultDetailedGridSlideHeaderState.Serialize() + @"</DefaultDetailedGridSlideHeaderState>");

				xml.AppendLine(@"<DefaultMultiGridColumnState>" + _defaultMultiGridColumnState.Serialize() + @"</DefaultMultiGridColumnState>");
				xml.AppendLine(@"<DefaultMultiGridAdNotesState>" + _defaultMultiGridAdNotesState.Serialize() + @"</DefaultMultiGridAdNotesState>");
				xml.AppendLine(@"<DefaultMultiGridSlideBulletsState>" + _defaultMultiGridSlideBulletsState.Serialize() + @"</DefaultMultiGridSlideBulletsState>");
				xml.AppendLine(@"<DefaultMultiGridSlideHeaderState>" + _defaultMultiGridSlideHeaderState.Serialize() + @"</DefaultMultiGridSlideHeaderState>");

				xml.AppendLine(@"<DefaultCalendarViewSettings>" + _defaultCalendarViewSettings.Serialize() + @"</DefaultCalendarViewSettings>");

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