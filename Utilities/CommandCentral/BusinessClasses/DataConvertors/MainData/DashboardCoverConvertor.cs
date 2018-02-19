using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using Asa.Common.Core.Helpers;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.Common;
using CommandCentral.BusinessClasses.Entities.Dashboard;

namespace CommandCentral.BusinessClasses.DataConvertors.MainData
{
	class DashboardCoverConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "Add Cover.xml";
		private readonly string _sourceFilePath;

		public DashboardCoverConvertor(string sourceFilePath)
		{
			_sourceFilePath = sourceFilePath;
		}

		public void Convert(IList<string> destinationFolderPaths)
		{
			var slideHeaders = new List<ListDataItem>();
			var quotes = new List<Quote>();

			var connnectionString =
				String.Format(
					@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=No;IMEX=1"";",
					_sourceFilePath);
			using (var connection = new OleDbConnection(connnectionString))
			{
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
									if (rowValue.StartsWith("*Presentation Titles", StringComparison.OrdinalIgnoreCase))
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

									var slideHeader = new ListDataItem();
									slideHeader.Value = rowValue;
									slideHeader.IsDefault = String.Equals(row[1]?.ToString().Trim(), "D", StringComparison.OrdinalIgnoreCase);
									slideHeaders.Add(slideHeader);
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
						var dataAdapter = new OleDbDataAdapter("SELECT * FROM [quotes$]", connection);
						var dataTable = new DataTable();
						try
						{
							dataAdapter.Fill(dataTable);
							var columnsCount = dataTable.Columns.Count;
							if (columnsCount > 0)
								foreach (DataRow row in dataTable.Rows)
								{
									var value = row[0]?.ToString().Trim();
									var author = row[1]?.ToString().Trim();
									if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(author) &&
										!value.StartsWith("*", StringComparison.OrdinalIgnoreCase))
										quotes.Add(new Quote(value, author));
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
					}
				}
				else
					throw new ConversionException { SourceFilePath = _sourceFilePath };
				connection.Close();
			}

			var xml = new StringBuilder();
			xml.AppendLine("<CoverSlide>");
			foreach (ListDataItem slideHeader in slideHeaders)
			{
				xml.Append(@"<SlideHeader ");
				xml.Append("Value = \"" + slideHeader.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsDefault = \"" + slideHeader.IsDefault + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (Quote quote in quotes)
			{
				xml.Append(@"<Quote ");
				xml.Append("Value = \"" + quote.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Author = \"" + quote.Author.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			xml.AppendLine(@"</CoverSlide>");

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
	}
}
