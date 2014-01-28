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
using CommandCentral.InteropClasses;

namespace CommandCentral.TabMainDashboard
{
	internal class OnlineStrategyManager
	{
		private const string SourceFileName = @"Data\!Main_Dashboard\Online Source\Online Strategy.xls";
		private const string DestinationFileName = @"Data\!Main_Dashboard\Online XML\Online Strategy.xml";
		private const string ImageSourceFolder = @"Data\!Main_Dashboard\Online Source\Category Images";

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

					if (!category.Name.Trim().Equals("Headers") && !category.Name.Trim().Equals("Sites") && !category.Name.Trim().Equals("Strengths") && !category.Name.Trim().Equals("Categories") && !category.Name.Trim().Equals("Slides") && !category.Name.Trim().Equals("File-Status") && !category.Name.Trim().Equals("WebFormula"))
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
			string defaultFormula = string.Empty;

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

					sites.Sort((x, y) =>
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

				//Load Default Formula
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [WebFormula$]", connection);
				dataTable = new DataTable();

				bool loadDefaultFormula = true;

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 2)
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Trim().Equals("*Web Formula Default"))
							{
								loadDefaultFormula = true;
								continue;
							}

							if (loadDefaultFormula)
							{
								if (row[0] != null && row[1] != null && row[1].ToString().Trim().ToLower().Equals("d"))
									defaultFormula = row[0].ToString();
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
								var filePath = Path.Combine(Application.StartupPath, ImageSourceFolder, row[3].ToString().Trim());
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
						if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 7)
							foreach (DataRow row in dataTable.Rows)
							{
								var product = new Product();
								product.SubCategory = row[0].ToString().Trim();
								product.Name = row[1].ToString().Trim();
								product.RateType = row[2].ToString().Trim();
								product.Rate = row[3].ToString().Trim();
								product.Width = row[4].ToString().Trim();
								product.Height = row[5].ToString().Trim();
								product.Overview = row[6].ToString().Trim();
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
				connection.Close();
			}

			//Save XML
			var xml = new StringBuilder();
			xml.AppendLine("<OnlineStrategy>");
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

			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
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
				xml.Append("Overview = \"" + product.Overview.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			xml.AppendLine(@"</OnlineStrategy>");

			string xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
			using (var sw = new StreamWriter(xmlPath, false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}

			AppManager.Instance.ShowInformation("Data was updated.");
		}
	}
}