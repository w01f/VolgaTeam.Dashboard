using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Asa.Business.Online.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Json;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.Common;
using CommandCentral.BusinessClasses.Entities.Online;
using Newtonsoft.Json;

namespace CommandCentral.BusinessClasses.DataConvertors
{
	class OnlineDataConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "Online Strategy.xml";
		private readonly string _mainDataSourceFilePath;
		private readonly string _buttonsImagesFolderPath;
		private readonly string _categoryImagesFolderPath;

		private readonly List<Category> _categories = new List<Category>();

		public OnlineDataConvertor(string mainDataSourceFilePath, string imagesFolderPath)
		{
			_mainDataSourceFilePath = mainDataSourceFilePath;
			_buttonsImagesFolderPath = Path.Combine(imagesFolderPath, "digital_icons");
			_categoryImagesFolderPath = Path.Combine(imagesFolderPath, "digital_categories");
		}

		public void Convert(IList<string> destinationFolderPaths)
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

			var defaultHomeViewSettings = new HomeViewSettings();
			var defaultDigitalProductSettings = new DigitalProductSettings();
			var defaultDigitalProductPackageSettings = new DigitalPackageSettings();
			var defaultDigitalStandalonePackageSettings = new DigitalPackageSettings();

			var controlsConfiguration = new DigitalControlsConfiguration();

			var connnectionString =
				String.Format(
					@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";",
					_mainDataSourceFilePath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch (Exception)
			{
				throw new ConversionException { SourceFilePath = _mainDataSourceFilePath };
			}
			if (connection.State == ConnectionState.Open)
			{
				//Load Headers
				{
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
							var result = y.IsDefault.CompareTo(x.IsDefault);
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
				}

				//Load Statuses
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
					var dataTable = new DataTable();
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
				}

				//Load Sites
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sites$]", connection);
					var dataTable = new DataTable();
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
										site.IsDefault = row[1].ToString().Trim().Equals("d", StringComparison.OrdinalIgnoreCase);
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
				}

				//Load Strengths
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Strengths$]", connection);
					var dataTable = new DataTable();
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
				}

				//Load Default Formula
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [WebFormula$]", connection);
					var dataTable = new DataTable();

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
				}

				//Load Locked Mode
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [LockedMode$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString();
								if (!String.IsNullOrEmpty(rowValue) &&
									!rowValue.StartsWith("*") &&
									Boolean.TryParse(rowValue, out var temp))
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
				}

				//Load Pricing Strategy
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [PricingStrategy$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString();
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
				}

				//Load Position Column
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [PositionColumn$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString();
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
				}

				//Load Special Links Group
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A1:A2]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								if (row[0] == null) continue;
								var rowValue = row[0].ToString();
								if (!String.IsNullOrEmpty(rowValue) && Boolean.TryParse(rowValue, out var temp))
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
				}

				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A4:A5]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString();
								if (String.IsNullOrEmpty(rowValue)) continue;
								specialLinksGroupName = rowValue;
								var imageFilePath = Path.Combine(_buttonsImagesFolderPath, "!RibbonGroup.png");
								if (File.Exists(imageFilePath))
									specialLinksGroupLogo = new Bitmap(imageFilePath);
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
				}

				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A7:E8]", connection);
					var dataTable = new DataTable();
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
				}

				//Load Special Link Buttons
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [SpecialRBNLinks$A10:J30]", connection);
					var dataTable = new DataTable();
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
										var imageFilePath = Path.Combine(_buttonsImagesFolderPath, rowValue);
										if (File.Exists(imageFilePath))
											specialButton.Image = new Bitmap(imageFilePath);
										break;
									default:
										specialButton.Paths.Add(rowValue);
										break;
								}
							}
							if (!String.IsNullOrEmpty(specialButton.Name) && !String.IsNullOrEmpty(specialButton.Type) &&
								specialButton.Paths.Any())
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
				}

				//Load Targeting
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [TGT_Popup$]", connection);
					var dataTable = new DataTable();
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
											if (Boolean.TryParse(rowValue, out var temp))
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
				}

				//Load Rich Media
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [RM_Popup$]", connection);
					var dataTable = new DataTable();
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
											if (Boolean.TryParse(rowValue, out var temp))
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
				}

				//Load Categories
				{
					GetCategories(connection);

					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Categories$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						var i = 0;
						if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 4)
							foreach (DataRow row in dataTable.Rows)
							{
								var originalName = row[0].ToString().Trim();
								var schemaName = originalName
									.Replace(".", "#")
									.Replace("!", "_")
									.Replace("`", "_")
									.Replace("[", "(")
									.Replace("]", ")");
								var category = _categories.FirstOrDefault(x => x.Name.Equals(schemaName));
								if (category != null)
								{
									category.Name = originalName;
									category.Order = i;
									category.TooltipTitle = row[1].ToString().Trim();
									category.TooltipValue = row[2].ToString().Trim();
									var filePath = Path.Combine(_categoryImagesFolderPath, row[3].ToString().Trim());
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
				}

				//Load Products
				{
					products.Clear();
					foreach (var category in _categories)
					{
						var dataAdapter = new OleDbDataAdapter(
							String.Format("SELECT * FROM [{0}$]",
								category.Name
									.Replace("!", "")
							)
							, connection);
						var dataTable = new DataTable();
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
									product.DefaultWebsite = row[10]?.ToString().Trim();
									product.EnableRate = row[11].ToString().Trim().ToLower() == "e";
									product.Category = category;
									if (!string.IsNullOrEmpty(product.Name))
										products.Add(product);
								}
						}
						catch (Exception)
						{
						}
						finally
						{
							dataAdapter.Dispose();
							dataTable.Dispose();
						}
					}
				}

				//Load Home Settings
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Home$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count >= 3)
							foreach (DataRow row in dataTable.Rows)
							{
								var optionName = row[0]?.ToString().Trim();
								if (String.IsNullOrEmpty(optionName)) continue;
								switch (optionName)
								{
									case "Ad Dimensions":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultHomeViewSettings.EnableDigitalDimensions = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultHomeViewSettings.ShowDigitalDimensions = temp;
										}
										break;
									case "Location":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultHomeViewSettings.EnableDigitalLocation = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultHomeViewSettings.ShowDigitalLocation = temp;
										}
										break;
									case "Pricing Strategy":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultHomeViewSettings.EnableDigitalStrategy = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultHomeViewSettings.ShowDigitalStrategy = temp;
										}
										break;
									case "Rich Media":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultHomeViewSettings.EnableDigitalRichMedia = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultHomeViewSettings.ShowDigitalRichMedia = temp;
										}
										break;
									case "Targeting":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
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
				}

				//Load Digital Product Settings
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [WebSlide$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count >= 3)
							foreach (DataRow row in dataTable.Rows)
							{
								var optionName = row[0]?.ToString().Trim();
								if (String.IsNullOrEmpty(optionName)) continue;
								switch (optionName)
								{
									case "Strategy":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductSettings.EnableCategory = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductSettings.ShowCategory = temp;
										}
										break;
									case "Campaign Dates":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductSettings.EnableFlightDates = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductSettings.ShowFlightDates = temp;
										}
										break;
									case "Duration":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductSettings.EnableDuration = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductSettings.ShowDuration = temp;
										}
										break;
									case "Pricing Default":
										{
											if (row[2] != null && Int32.TryParse(row[2].ToString(), out var temp))
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
				}

				//Load Digital Package Settings
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [DigPkgA$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count >= 3)
							foreach (DataRow row in dataTable.Rows)
							{
								var optionName = row[0]?.ToString().Trim();
								if (String.IsNullOrEmpty(optionName)) continue;
								switch (optionName)
								{
									case "Category":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableCategory = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowCategory = temp;
										}
										break;
									case "Group":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableGroup = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowGroup = temp;
										}
										break;
									case "Product":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableProduct = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowProduct = temp;
										}
										break;
									case "Impressions":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableImpressions = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowImpressions = temp;
										}
										break;
									case "CPM":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableCPM = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowCPM = temp;
										}
										break;
									case "Rate":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableRate = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowRate = temp;
										}
										break;
									case "Investment":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableInvestment = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowInvestment = temp;
										}
										break;
									case "Schedule Info":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableInfo = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowInfo = temp;
										}
										break;
									case "Campaign":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableLocation = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowLocation = temp;
										}
										break;
									case "Screenshot":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalProductPackageSettings.EnableScreenshot = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalProductPackageSettings.ShowScreenshot = temp;
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
				}

				//Load Digital Standalone Package Settings
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [DigPkgB$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Columns.Count >= 3)
							foreach (DataRow row in dataTable.Rows)
							{
								var optionName = row[0]?.ToString().Trim();
								if (String.IsNullOrEmpty(optionName)) continue;
								switch (optionName)
								{
									case "Category":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableCategory = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowCategory = temp;
										}
										break;
									case "Group":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableGroup = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowGroup = temp;
										}
										break;
									case "Product":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableProduct = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowProduct = temp;
										}
										break;
									case "Impressions":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableImpressions = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowImpressions = temp;
										}
										break;
									case "CPM":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableCPM = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowCPM = temp;
										}
										break;
									case "Rate":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableRate = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowRate = temp;
										}
										break;
									case "Investment":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableInvestment = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowInvestment = temp;
										}
										break;
									case "Schedule Info":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableInfo = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowInfo = temp;
										}
										break;
									case "Campaign":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableLocation = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowLocation = temp;
										}
										break;
									case "Screenshot":
										{
											if (row[1] != null && Boolean.TryParse(row[1].ToString(), out var temp))
												defaultDigitalStandalonePackageSettings.EnableScreenshot = temp;
											if (row[2] != null && Boolean.TryParse(row[2].ToString(), out temp))
												defaultDigitalStandalonePackageSettings.ShowScreenshot = temp;
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
				}

				connection.Close();
			}
			else
				throw new ConversionException { SourceFilePath = _mainDataSourceFilePath };

			connnectionString =
				String.Format(
					@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=No;IMEX=1"";",
					_mainDataSourceFilePath);
			connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch (Exception)
			{
				throw new ConversionException { SourceFilePath = _mainDataSourceFilePath };
			}
			if (connection.State == ConnectionState.Open)
			{
				//Load Digital Tab Controls Configuration
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [DigitalRBNLabels$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						var groupName = String.Empty;
						var groupValues = new List<string>();
						foreach (DataRow row in dataTable.Rows)
						{
							var rowValue = row[0]?.ToString().Trim();
							if (String.IsNullOrEmpty(rowValue)) continue;
							if (rowValue.Contains("*"))
							{
								if (!String.IsNullOrEmpty(groupName) && groupValues.Any())
									controlsConfiguration.ApplyValues(groupName, groupValues);
								groupName = rowValue.Replace("*", "");
								groupValues.Clear();
							}
							else
								groupValues.Add(rowValue);
						}
						if (!String.IsNullOrEmpty(groupName) && groupValues.Any())
							controlsConfiguration.ApplyValues(groupName, groupValues);
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

				//Load Digital Section Controls Configuration
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [DigitalSubScheduleLabels$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						var groupName = String.Empty;
						var groupValues = new List<string>();
						foreach (DataRow row in dataTable.Rows)
						{
							var rowValue = row[0]?.ToString().Trim();
							if (String.IsNullOrEmpty(rowValue)) continue;
							if (rowValue.Contains("*"))
							{
								if (!String.IsNullOrEmpty(groupName) && groupValues.Any())
									controlsConfiguration.ApplyValues(groupName, groupValues);
								groupName = rowValue.Replace("*", "");
								groupValues.Clear();
							}
							else
								groupValues.Add(rowValue);
						}
						if (!String.IsNullOrEmpty(groupName) && groupValues.Any())
							controlsConfiguration.ApplyValues(groupName, groupValues);
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
			}
			else
				throw new ConversionException { SourceFilePath = _mainDataSourceFilePath };

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
				xml.AppendLine("<SpecialButtonsGroupLogo>" + System.Convert.ToBase64String((byte[])converter.ConvertTo(specialLinksGroupLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SpecialButtonsGroupLogo>");
			foreach (var browser in specialLinksBrowsers)
				xml.AppendLine("<Browser>" + browser + "</Browser>");

			foreach (var specialLinkButton in specialLinkButtons)
			{
				xml.Append(@"<SpecialButton ");
				xml.Append("Name = \"" + specialLinkButton.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Type = \"" + specialLinkButton.Type.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Tooltip = \"" + specialLinkButton.Tooltip.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Image = \"" + System.Convert.ToBase64String((byte[])converter.ConvertTo(specialLinkButton.Image, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@">");
				foreach (var path in specialLinkButton.Paths)
					xml.AppendLine(String.Format(@"<Path>{0}</Path>", path.Replace(@"&", "&#38;").Replace("\"", "&quot;")));
				xml.AppendLine(@"</SpecialButton>");
			}

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
				xml.Append("Logo = \"" + System.Convert.ToBase64String((byte[])converter.ConvertTo(category.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
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
			xml.AppendLine(String.Format(@"<DefaultDigitalProductPackageSettings>{0}</DefaultDigitalProductPackageSettings>", defaultDigitalProductPackageSettings.Serialize()));
			xml.AppendLine(String.Format(@"<DefaultDigitalStandalonePackageSettings>{0}</DefaultDigitalStandalonePackageSettings>", defaultDigitalStandalonePackageSettings.Serialize()));
			xml.AppendLine(String.Format(@"<DigitalControlsConfiguration>{0}</DigitalControlsConfiguration>", System.Convert.ToBase64String(Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(controlsConfiguration, Formatting.None, new JsonImageConverter())))));

			xml.AppendLine(@"</OnlineStrategy>");

			foreach (var folderPath in destinationFolderPaths)
			{
				var xmlPath = Path.Combine(folderPath, DestinationFileName);
				using (var sw = new StreamWriter(xmlPath, false))
				{
					sw.Write(xml.ToString());
					sw.Flush();
				}
			}
		}

		private void GetCategories(OleDbConnection connection)
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
						"SpecialRBNLinks",
						"TGT_Popup",
						"RM_Popup",
						"Home",
						"WebSlide",
						"DigPkgA",
						"DigPkgB",
						"DigitalRBNLabels",
						"DigitalSubScheduleLabels",
					}.Contains(category.Name.Trim()))
						_categories.Add(category);
				}
			}
			catch
			{
			}
		}
	}
}
