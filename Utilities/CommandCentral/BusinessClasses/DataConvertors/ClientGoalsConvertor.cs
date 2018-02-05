using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using Asa.Common.Core.Helpers;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.Common;

namespace CommandCentral.BusinessClasses.DataConvertors
{
	class ClientGoalsConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "Needs Analysis.xml";
		private readonly string _sourceFilePath;

		public ClientGoalsConvertor(string sourceFilePath)
		{
			_sourceFilePath = sourceFilePath;
		}

		public void Convert(IList<string> destinationFolderPaths)
		{
			var connnectionString =
				String.Format(
					@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=No;IMEX=1"";",
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
				var slideHeaders = new List<ListDataItem>();
				var clientGoals = new List<ListDataItem>();

				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [6ms$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						var columnsCount = dataTable.Columns.Count;
						if (columnsCount > 0)
						{
							var processReading = false;
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString().Trim() ?? String.Empty;
								if (rowValue.StartsWith("*NeedsAnalysisHeader", StringComparison.OrdinalIgnoreCase))
								{
									processReading = true;
									continue;
								}
								if (processReading && rowValue.StartsWith("*", StringComparison.OrdinalIgnoreCase))
									break;
								if (String.IsNullOrEmpty(rowValue))
									break;
								if (!processReading)
									continue;

								var listDataItem = new ListDataItem();
								listDataItem.Value = rowValue;
								listDataItem.IsDefault = String.Equals(row[1]?.ToString().Trim(), "D", StringComparison.OrdinalIgnoreCase);
								slideHeaders.Add(listDataItem);
							}
						}
					}
					catch
					{
						throw new ConversionException { SourceFilePath = _sourceFilePath };
					}
					finally
					{
						dataAdapter.Dispose();
						dataTable.Dispose();
					}
					slideHeaders.Sort((x, y) =>
					{
						int result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}

				{
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [goals$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						var columnsCount = dataTable.Columns.Count;
						if (columnsCount > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString().Trim();
								if (String.IsNullOrEmpty(rowValue))
									break;
								if (!rowValue.StartsWith("*", StringComparison.OrdinalIgnoreCase))
								{
									var listDataItem = new ListDataItem();
									listDataItem.Value = rowValue;

									var defaultValue = row[1]?.ToString().Trim();
									listDataItem.IsDefault = defaultValue?.StartsWith("D", StringComparison.OrdinalIgnoreCase) ?? false;
									if (listDataItem.IsDefault && Int32.TryParse(defaultValue?.ToUpper().Replace("D", ""), out var temp))
										listDataItem.DefaultOrder = temp;

									clientGoals.Add(listDataItem);
								}
							}
					}
					catch
					{
						throw new ConversionException { SourceFilePath = _sourceFilePath };
					}
					finally
					{
						dataAdapter.Dispose();
						dataTable.Dispose();
					}
					clientGoals.Sort((x, y) =>
					{
						var result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = x.DefaultOrder.CompareTo(y.DefaultOrder);
						if (result == 0)
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}

				connection.Close();

				var xml = new StringBuilder();
				xml.AppendLine("<ClientGoals>");
				foreach (var header in slideHeaders)
				{
					xml.Append(@"<SlideHeader ");
					xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (var goal in clientGoals)
				{
					xml.Append(@"<Goal ");
					xml.Append("Value = \"" + goal.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + goal.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</ClientGoals>");

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
			else
				throw new ConversionException { SourceFilePath = _sourceFilePath };
		}
	}
}
