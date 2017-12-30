using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.SalesDepot;

namespace CommandCentral.BusinessClasses.DataConvertors
{
	class SalesLibrariesDataConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "SDSearch.xml";
		private readonly string _sourceFilePath;

		private readonly List<SearchGroup> _categories = new List<SearchGroup>();

		public SalesLibrariesDataConvertor(string sourceFilePath)
		{
			_sourceFilePath = sourceFilePath;
		}

		public void Convert(IList<string> destinationFolderPaths)
		{
			var searchGroups = new List<SearchGroup>();
			var searchTopGroupIcons = new Dictionary<string, string>();
			var maxTags = 0;
			var tagCount = false;

			var connnectionString =
				String.Format(
					@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";",
					_sourceFilePath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch (Exception)
			{
				throw new ConversionException { SourceFilePath = _sourceFilePath };
			}
			if (connection.State == ConnectionState.Open)
			{
				//Load Settings
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Settings$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						foreach (DataRow row in dataTable.Rows)
						{
							if (row[0].ToString().Equals("Max Tags") && int.TryParse(row[1].ToString(), out int tempInt))
								maxTags = tempInt;
							else
							{
								if (row[0].ToString().Equals("Tag Count") && bool.TryParse(row[1].ToString(), out bool tempBool))
									tagCount = tempBool;
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

				//Load Top Group Icons
				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Groups$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						foreach (DataRow row in dataTable.Rows)
						{
							var topGroupName = row[0].ToString().Trim();
							var topGroupIcon = row[1].ToString().Trim();
							searchTopGroupIcons.Add(topGroupName, topGroupIcon);
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

				//Load Groups
				{
					GetCategories(connection);
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Categories$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Rows.Count > 1 && dataTable.Columns.Count > 1)
							foreach (DataRow row in dataTable.Rows)
							{
								string groupName = row[0].ToString().Trim();
								SearchGroup searchGroup = _categories.FirstOrDefault(x => x.Name.Equals(groupName));
								if (searchGroup != null)
								{
									searchGroup.Description = row[1].ToString().Trim();

									var topGroupName = row[2].ToString().Trim();
									searchGroup.TopGroupName = topGroupName;
									if (searchTopGroupIcons.ContainsKey(topGroupName))
										searchGroup.TopGroupIcon = searchTopGroupIcons[topGroupName];

									searchGroups.Add(searchGroup);
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

				//Load Tags
				foreach (SearchGroup searchGroup in searchGroups)
				{
					searchGroup.Tags.Clear();
					var dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", searchGroup.Name.Replace(".", "#")),
						connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								string value = row[0].ToString().Trim();
								if (!string.IsNullOrEmpty(value))
									searchGroup.Tags.Add(value);
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
				throw new ConversionException { SourceFilePath = _sourceFilePath };

			var xml = new StringBuilder();
			xml.AppendLine("<SDSearch>");
			xml.AppendLine("<MaxTags>" + maxTags + "</MaxTags>");
			xml.AppendLine("<TagCount>" + tagCount + "</TagCount>");
			foreach (SearchGroup group in searchGroups)
			{
				xml.Append(@"<Category ");
				xml.Append("Name = \"" + group.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Description = \"" + group.Description.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Group = \"" + group.TopGroupName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
				xml.Append("GroupIcon = \"" + group.TopGroupIcon.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@">");
				foreach (string tag in group.Tags)
				{
					xml.Append(@"<Tag ");
					xml.Append("Value = \"" + tag.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</Category>");
			}
			xml.AppendLine(@"</SDSearch>");

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
					var searchGroup = new SearchGroup();
					searchGroup.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "").Replace("#", ".");

					if (!searchGroup.Name.Trim().Equals("Categories") &&
						!searchGroup.Name.Trim().Equals("Settings") &&
						!searchGroup.Name.Trim().Equals("Groups"))
						_categories.Add(searchGroup);
				}
			}
			catch { }
		}
	}
}
