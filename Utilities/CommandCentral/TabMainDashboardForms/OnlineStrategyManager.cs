using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommandCentral.CommonClasses;
using CommandCentral.CommonClasses.DigitalViewSettings;
using CommandCentral.InteropClasses;

namespace CommandCentral.TabMainDashboard
{
	internal class OnlineStrategyManager
	{
		private const string SourceFileName = @"Data\!Main_Dashboard\Online Source\Online Strategy.xls";
		private const string DestinationFileName = @"Data\!Main_Dashboard\Online XML\Online Strategy.xml";
		private const string CategoryImageSourceFolder = @"Data\!Main_Dashboard\Online Source\Category Images";
		private const string SpecialButtonsImageSourceFolder = @"Data\!Main_Dashboard\Online Source\Special Buttons Images";

		public const string ButtonText = "Online Strategy\nData";

		private static readonly List<Category> _categories = new List<Category>();

		private static void GetCategories(OleDbConnection connection)
		{
			try
			{
				_categories.Clear();
				var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				foreach (DataRow row in dataTable.Rows)
				{
					var category = new Category();
					category.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "").Replace("#", ".");

					if (!new[]
					{
						"Headers",
						"Sites",
						"Strengths",
						"Categories",
						"Slides",
						"File-Status",
						"WebFormula",
						"LockedMode",
						"PricingStrategy",
						"PositionColumn",
						"Placeholder",
						"SpecialRBNLinks",
						"TGT_Popup",
						"RM_Popup",						
						"Home",
						"WebSlide",
						"DigPkg",
					}.Contains(category.Name.Trim()))
						_categories.Add(category);
				}
			}
			catch
			{
			}
		}

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
			var slideHeaders = new List<SlideHeader>();
			var sites = new List<SlideHeader>();
			var strengths = new List<SlideHeader>();
			var products = new List<Product>();
			var statuses = new List<SlideHeader>();
			var defaultFormula = String.Empty;
			var lockedMode = false;
			var pricingStrategies = new List<string>();
			var columnPositions = new List<string>();
			var specialLinksEnable = false;
			var specialLinksGroupName = String.Empty;
			Image specialLinksGroupLogo = null;
			var specialLinksBrowsers = new List<string>();
			var specialLinkButtons = new List<SpecialLinkButton>();
			var targetingRecords = new List<DigitalProductInfo>();
			var richMediaRecords = new List<DigitalProductInfo>();
			var placeholders = new List<string>();

			var defaultHomeViewSettings = new HomeViewSettings();
			var defaultDigitalProductSettings = new DigitalProductSettings();
			var defaultDigitalPackageSettings = new DigitalPackageSettings();

			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
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
				//Load Headers
				var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
				var dataTable = new DataTable();
				slideHeaders.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var title = new SlideHeader();
							title.Value = row[0].ToString().Trim();
							if (dataTable.Columns.Count > 1)
								if (row[1] != null)
									title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
							if (!string.IsNullOrEmpty(title.Value))
								slideHeaders.Add(title);
						}

					slideHeaders.Sort((x, y) =>
					{
						int result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = 1;
						return result;
					});
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Statuses
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
				dataTable = new DataTable();
				statuses.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var status = new SlideHeader();
							status.Value = row[0].ToString().Trim();
							if (dataTable.Columns.Count > 1)
								if (row[1] != null)
									status.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
							if (!string.IsNullOrEmpty(status.Value))
								statuses.Add(status);
						}

					statuses.Sort((x, y) =>
					{
						int result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Sites
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sites$]", connection);
				dataTable = new DataTable();
				sites.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var site = new SlideHeader();
							site.Value = row[0].ToString().Trim();
							if (dataTable.Columns.Count > 1)
								if (row[1] != null)
									site.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
							if (!string.IsNullOrEmpty(site.Value))
								sites.Add(site);
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Strengths
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Strengths$]", connection);
				dataTable = new DataTable();
				strengths.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var strength = new SlideHeader();
							strength.Value = row[0].ToString().Trim();
							if (dataTable.Columns.Count > 1)
								if (row[1] != null)
									strength.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
							if (!string.IsNullOrEmpty(strength.Value))
								strengths.Add(strength);
						}

					strengths.Sort((x, y) =>
					{
						var result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Default Formula
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [WebFormula$]", connection);
				dataTable = new DataTable();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 2)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0] != null && row[1] != null && row[1].ToString().Trim().ToLower().Equals("d"))
							{
								defaultFormula = row[0].ToString();
								break;
							}
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Locked Mode
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [LockedMode$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							bool temp;
							if (row[0] == null) continue;
							var rowValue = row[0].ToString();
							if (!String.IsNullOrEmpty(rowValue) &&
								!rowValue.StartsWith("*") &&
								Boolean.TryParse(rowValue, out temp))
							{
								lockedMode = temp;
								break;
							}
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Pricing Strategy
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [PricingStrategy$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0] == null) continue;
							var rowValue = row[0].ToString();
							if (!String.IsNullOrEmpty(rowValue) && !pricingStrategies.Contains(rowValue))
								pricingStrategies.Add(rowValue);
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Position Column
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [PositionColumn$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0] == null) continue;
							var rowValue = row[0].ToString();
							if (!String.IsNullOrEmpty(rowValue) && !columnPositions.Contains(rowValue))
								columnPositions.Add(rowValue);
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Special Links Group
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A1:A2]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0] == null) continue;
							var rowValue = row[0].ToString();
							bool temp;
							if (!String.IsNullOrEmpty(rowValue) && Boolean.TryParse(rowValue, out temp))
							{
								specialLinksEnable = temp;
								break;
							}
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}


				dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A4:A5]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0] == null) continue;
							var rowValue = row[0].ToString();
							if (!String.IsNullOrEmpty(rowValue))
							{
								specialLinksGroupName = rowValue;
								var imageFilePath = Path.Combine(Application.StartupPath, SpecialButtonsImageSourceFolder, "!RibbonGroup.png");
								if (File.Exists(imageFilePath))
									specialLinksGroupLogo = new Bitmap(imageFilePath);
								break;
							}
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A7:E8]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var colCount = dataTable.Columns.Count;
							for (var i = 0; i < colCount; i++)
							{
								if (row[i] == null) continue;
								var rowValue = row[i].ToString().Trim();
								if (String.IsNullOrEmpty(rowValue)) continue;
								specialLinksBrowsers.Add(rowValue);
							}
							break;
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Special Link Buttons
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A10:J30]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						var specialButton = new SpecialLinkButton();
						var colCount = dataTable.Columns.Count;
						for (var i = 0; i < colCount; i++)
						{
							if (row[i] == null) continue;
							var rowValue = row[i].ToString().Trim();
							if (String.IsNullOrEmpty(rowValue)) continue;
							switch (i)
							{
								case 0:
									specialButton.Name = rowValue;
									break;
								case 1:
									specialButton.Type = rowValue;
									break;
								case 2:
									specialButton.Tooltip = rowValue;
									break;
								case 3:
									var imageFilePath = Path.Combine(Application.StartupPath, SpecialButtonsImageSourceFolder, rowValue);
									if (File.Exists(imageFilePath))
										specialButton.Image = new Bitmap(imageFilePath);
									break;
								default:
									specialButton.Paths.Add(rowValue);
									break;
							}
						}
						if (!String.IsNullOrEmpty(specialButton.Name) && !String.IsNullOrEmpty(specialButton.Type) && specialButton.Paths.Any())
							specialLinkButtons.Add(specialButton);
					}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Placeholders
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Placeholder$]", connection);
				dataTable = new DataTable();
				placeholders.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var placeholder = row[0].ToString().Trim();
							if (!String.IsNullOrEmpty(placeholder))
								placeholders.Add(placeholder);
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Targeting
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [TGT_Popup$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						var digitalProductInfo = new DigitalProductInfo();
						var colCount = dataTable.Columns.Count;
						for (var i = 0; i < colCount; i++)
						{
							if (row[i] == null) continue;
							var rowValue = row[i].ToString().Trim();
							if (String.IsNullOrEmpty(rowValue)) continue;
							switch (i)
							{
								case 0:
									digitalProductInfo.Group = rowValue;
									break;
								case 1:
									{
										bool temp;
										if (Boolean.TryParse(rowValue, out temp))
											digitalProductInfo.Selected = temp;
									}
									break;
								default:
									digitalProductInfo.Phrases.Add(rowValue);
									break;
							}
						}
						if (!String.IsNullOrEmpty(digitalProductInfo.Group) && digitalProductInfo.Phrases.Any())
							targetingRecords.Add(digitalProductInfo);
					}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Rich Media
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [RM_Popup$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						var digitalProductInfo = new DigitalProductInfo();
						var colCount = dataTable.Columns.Count;
						for (var i = 0; i < colCount; i++)
						{
							if (row[i] == null) continue;
							var rowValue = row[i].ToString().Trim();
							if (String.IsNullOrEmpty(rowValue)) continue;
							switch (i)
							{
								case 0:
									digitalProductInfo.Group = rowValue;
									break;
								case 1:
									{
										bool temp;
										if (Boolean.TryParse(rowValue, out temp))
											digitalProductInfo.Selected = temp;
									}
									break;
								default:
									digitalProductInfo.Phrases.Add(rowValue);
									break;
							}
						}
						if (!String.IsNullOrEmpty(digitalProductInfo.Group) && digitalProductInfo.Phrases.Any())
							richMediaRecords.Add(digitalProductInfo);
					}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Categories
				GetCategories(connection);
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Categories$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					int i = 0;
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 4)
						foreach (DataRow row in dataTable.Rows)
						{
							string name = row[0].ToString().Trim();
							var category = _categories.FirstOrDefault(x => x.Name.Equals(name));
							if (category != null)
							{
								category.Order = i;
								category.TooltipTitle = row[1].ToString().Trim();
								category.TooltipValue = row[2].ToString().Trim();
								var filePath = Path.Combine(Application.StartupPath, CategoryImageSourceFolder, row[3].ToString().Trim());
								if (File.Exists(filePath))
									category.Logo = new Bitmap(filePath);
							}
							i++;
						}
					_categories.Sort((x, y) => x.Order.CompareTo(y.Order));
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Products
				products.Clear();
				foreach (var category in _categories)
				{
					dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", category.Name), connection);
					dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 12)
							foreach (DataRow row in dataTable.Rows)
							{
								var product = new Product();
								product.SubCategory = row[0].ToString().Trim();
								product.Name = row[1].ToString().Trim();
								product.RateType = row[2].ToString().Trim();
								product.Rate = row[3].ToString().Trim();
								product.Width = row[4].ToString().Trim();
								product.Height = row[5].ToString().Trim();
								product.EnableTarget = row[6].ToString().Trim().ToLower() == "e";
								product.EnableLocation = row[7].ToString().Trim().ToLower() == "e";
								product.EnableRichMedia = row[8].ToString().Trim().ToLower() == "e";
								product.Overview = row[9].ToString().Trim();
								product.DefaultWebsite = row[10] != null ? row[10].ToString().Trim() : null;
								product.EnableRate = row[11].ToString().Trim().ToLower() == "e";
								product.Category = category;
								if (!string.IsNullOrEmpty(product.Name))
									products.Add(product);
							}
					}
					catch
					{
					}
					finally
					{
						dataAdapter.Dispose();
						dataTable.Dispose();
					}
				}

				//Load Home Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Home$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count >= 3)
						foreach (DataRow row in dataTable.Rows)
						{
							var optionName = row[0] != null ? row[0].ToString().Trim() : null;
							if (String.IsNullOrEmpty(optionName)) continue;
							switch (optionName)
							{
								case "Ad Dimensions":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultHomeViewSettings.EnableDigitalDimensions = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultHomeViewSettings.ShowDigitalDimensions = temp;
									}
									break;
								case "Location":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultHomeViewSettings.EnableDigitalLocation = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultHomeViewSettings.ShowDigitalLocation = temp;
									}
									break;
								case "Pricing Strategy":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultHomeViewSettings.EnableDigitalStrategy = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultHomeViewSettings.ShowDigitalStrategy = temp;
									}
									break;
								case "Rich Media":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultHomeViewSettings.EnableDigitalRichMedia = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultHomeViewSettings.ShowDigitalRichMedia = temp;
									}
									break;
								case "Targeting":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultHomeViewSettings.EnableDigitalTargeting = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultHomeViewSettings.ShowDigitalTargeting = temp;
									}
									break;
							}
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Digital Product Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [WebSlide$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count >= 3)
						foreach (DataRow row in dataTable.Rows)
						{
							var optionName = row[0] != null ? row[0].ToString().Trim() : null;
							if (String.IsNullOrEmpty(optionName)) continue;
							switch (optionName)
							{
								case "Strategy":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalProductSettings.EnableCategory = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalProductSettings.ShowCategory = temp;
									}
									break;
								case "Campaign Dates":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalProductSettings.EnableFlightDates = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalProductSettings.ShowFlightDates = temp;
									}
									break;
								case "Duration":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalProductSettings.EnableDuration = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalProductSettings.ShowDuration = temp;
									}
									break;
								case "Pricing Default":
									{
										int temp;
										if (row[2] != null && Int32.TryParse(row[2].ToString(), out temp))
											defaultDigitalProductSettings.DefaultPricing = temp;
									}
									break;
							}
						}
				}
				catch
				{
				}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Digital Package Settings
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [DigPkg$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Columns.Count >= 3)
						foreach (DataRow row in dataTable.Rows)
						{
							var optionName = row[0] != null ? row[0].ToString().Trim() : null;
							if (String.IsNullOrEmpty(optionName)) continue;
							switch (optionName)
							{
								case "Category":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableCategory = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowCategory = temp;
									}
									break;
								case "Group":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableGroup = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowGroup = temp;
									}
									break;
								case "Product":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableProduct = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowProduct = temp;
									}
									break;
								case "Impressions":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableImpressions = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowImpressions = temp;
									}
									break;
								case "CPM":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableCPM = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowCPM = temp;
									}
									break;
								case "Rate":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableRate = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowRate = temp;
									}
									break;
								case "Investment":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableInvestment = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowInvestment = temp;
									}
									break;
								case "Schedule Info":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableInfo = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowInfo = temp;
									}
									break;
								case "Comments":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableComments = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowComments = temp;
									}
									break;
								case "Screenshot":
									{
										bool temp;
										if (row[1] != null && Boolean.TryParse(row[1].ToString(), out temp))
											defaultDigitalPackageSettings.EnableScreenshot = temp;
										if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
											defaultDigitalPackageSettings.ShowScreenshot = temp;
									}
									break;
							}
						}
				}
				catch
				{ }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				connection.Close();
			}

			//Save XML
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var xml = new StringBuilder();
			xml.AppendLine("<OnlineStrategy>");
			foreach (var header in slideHeaders)
			{
				xml.Append(@"<Header ");
				xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var status in statuses)
			{
				xml.Append(@"<Status ");
				xml.Append("Value = \"" + status.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var site in sites)
			{
				xml.Append(@"<Site ");
				xml.Append("Value = \"" + site.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var strength in strengths)
			{
				xml.Append(@"<Strength ");
				xml.Append("Value = \"" + strength.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}

			xml.AppendLine("<DefaultFormula>" + defaultFormula.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</DefaultFormula>");
			xml.AppendLine("<LockedMode>" + lockedMode + "</LockedMode>");

			foreach (var pricingStrategy in pricingStrategies)
			{
				xml.Append(@"<PricingStrategy ");
				xml.Append("Value = \"" + pricingStrategy.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}

			foreach (var columnPosition in columnPositions)
			{
				xml.Append(@"<PositionColumn ");
				xml.Append("Value = \"" + columnPosition.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			xml.AppendLine("<SpecialLinksEnable>" + specialLinksEnable + "</SpecialLinksEnable>");
			xml.AppendLine("<SpecialButtonsGroupName>" + specialLinksGroupName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SpecialButtonsGroupName>");
			if (specialLinksGroupLogo != null)
				xml.AppendLine("<SpecialButtonsGroupLogo>" + Convert.ToBase64String((byte[])converter.ConvertTo(specialLinksGroupLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SpecialButtonsGroupLogo>");
			foreach (var browser in specialLinksBrowsers)
				xml.AppendLine("<Browser>" + browser + "</Browser>");

			foreach (var specialLinkButton in specialLinkButtons)
			{
				xml.Append(@"<SpecialButton ");
				xml.Append("Name = \"" + specialLinkButton.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Type = \"" + specialLinkButton.Type.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Tooltip = \"" + specialLinkButton.Tooltip.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Image = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(specialLinkButton.Image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@">");
				foreach (var path in specialLinkButton.Paths)
					xml.AppendLine(String.Format(@"<Path>{0}</Path>", path.Replace(@"&", "&#38;").Replace("\"", "&quot;")));
				xml.AppendLine(@"</SpecialButton>");
			}

			foreach (var placeholder in placeholders)
				xml.AppendLine(String.Format("<Placeholder>{0}</Placeholder>", placeholder.Replace(@"&", "&#38;").Replace("\"", "&quot;")));

			foreach (var productInfo in targetingRecords)
				xml.AppendLine(String.Format(@"<Targeting>{0}</Targeting>", productInfo.Serialize()));

			foreach (var productInfo in richMediaRecords)
				xml.AppendLine(String.Format(@"<RichMedia>{0}</RichMedia>", productInfo.Serialize()));

			foreach (var category in _categories.Where(c => !String.IsNullOrEmpty(c.Name) && !String.IsNullOrEmpty(c.TooltipTitle) && !String.IsNullOrEmpty(c.TooltipValue) && c.Logo != null))
			{
				xml.Append(@"<Category ");
				xml.Append("Name = \"" + category.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("TooltipTitle = \"" + category.TooltipTitle.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("TooltipValue = \"" + category.TooltipValue.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Logo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(category.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var product in products)
			{
				xml.Append(@"<Product ");
				xml.Append("Category = \"" + product.Category.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("SubCategory = \"" + product.SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Name = \"" + product.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("RateType = \"" + product.RateType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Rate = \"" + product.Rate.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Width = \"" + product.Width.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Height = \"" + product.Height.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("EnableTarget = \"" + product.EnableTarget + "\" ");
				xml.Append("EnableLocation = \"" + product.EnableLocation + "\" ");
				xml.Append("EnableRichMedia = \"" + product.EnableRichMedia + "\" ");
				xml.Append("EnableRate = \"" + product.EnableRate + "\" ");
				xml.Append("Overview = \"" + product.Overview.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				if (!String.IsNullOrEmpty(product.DefaultWebsite))
					xml.Append("DefaultWebsite = \"" + product.DefaultWebsite.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}

			xml.AppendLine(String.Format(@"<DefaultHomeViewSettings>{0}</DefaultHomeViewSettings>", defaultHomeViewSettings.Serialize()));
			xml.AppendLine(String.Format(@"<DefaultDigitalProductSettings>{0}</DefaultDigitalProductSettings>", defaultDigitalProductSettings.Serialize()));
			xml.AppendLine(String.Format(@"<DefaultDigitalPackageSettings>{0}</DefaultDigitalPackageSettings>", defaultDigitalPackageSettings.Serialize()));

			xml.AppendLine(@"</OnlineStrategy>");

			var xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
			using (var sw = new StreamWriter(xmlPath, false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}

			AppManager.Instance.ShowInformation("Data was updated.");
		}
	}
}