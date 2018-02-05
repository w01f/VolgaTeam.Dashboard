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
	class LeadoffStatementsConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "Intro Slide.xml";
		private readonly string _sourceFilePath;

		public LeadoffStatementsConvertor(string sourceFilePath)
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
				var statements = new List<ListDataItem>();

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
								if (rowValue.StartsWith("*Leadoff Slide Headers", StringComparison.OrdinalIgnoreCase))
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

							processReading = false;
							foreach (DataRow row in dataTable.Rows)
							{
								var rowValue = row[0]?.ToString().Trim() ?? String.Empty;
								if (rowValue.StartsWith("*Leadoff Statement", StringComparison.OrdinalIgnoreCase))
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
								statements.Add(listDataItem);
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
						var result = y.IsDefault.CompareTo(x.IsDefault);
						if (result == 0)
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}

				connection.Close();

				var xml = new StringBuilder();
				xml.AppendLine("<LeadOff>");
				foreach (var slideHeader in slideHeaders)
				{
					xml.Append(@"<SlideHeader ");
					xml.Append("Value = \"" + slideHeader.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + slideHeader.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (var statement in statements)
				{
					xml.Append(@"<Statement ");
					xml.Append("Value = \"" + statement.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + statement.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</LeadOff>");

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
