using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using Asa.Common.Core.Helpers;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.Common;

namespace CommandCentral.BusinessClasses.DataConvertors.MainData
{
	class TargetCustomersConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "Target Customer.xml";
		private readonly string _sourceFilePath;

		public TargetCustomersConvertor(string sourceFilePath)
		{
			_sourceFilePath = sourceFilePath;
		}

		public void Convert(IList<string> destinationFolderPaths)
		{
			var slideHeaders = new List<ListDataItem>();
			var demos = new List<ListDataItem>();
			var hhis = new List<ListDataItem>();
			var geographies = new List<ListDataItem>();

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
									if (rowValue.StartsWith("*TargetCustomerHeader", StringComparison.OrdinalIgnoreCase))
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
						var dataAdapter = new OleDbDataAdapter("SELECT * FROM [dma$]", connection);
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
									if (rowValue.StartsWith("*Demos", StringComparison.OrdinalIgnoreCase))
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
									demos.Add(listDataItem);
								}

								processReading = false;
								foreach (DataRow row in dataTable.Rows)
								{
									var rowValue = row[0]?.ToString().Trim() ?? String.Empty;
									if (rowValue.StartsWith("*HHI", StringComparison.OrdinalIgnoreCase))
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
									hhis.Add(listDataItem);
								}

								processReading = false;
								foreach (DataRow row in dataTable.Rows)
								{
									var rowValue = row[0]?.ToString().Trim() ?? String.Empty;
									if (rowValue.StartsWith("*Geography", StringComparison.OrdinalIgnoreCase))
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
									geographies.Add(listDataItem);
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

						demos.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Value, y.Value));
						hhis.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Value, y.Value));
						geographies.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Value, y.Value));
					}
				}
				else
					throw new ConversionException { SourceFilePath = _sourceFilePath };
				connection.Close();
			}

			var xml = new StringBuilder();
			xml.AppendLine("<TargetCustomers>");
			foreach (var listDataItem in slideHeaders)
			{
				xml.Append(@"<SlideHeader ");
				xml.Append("Value = \"" + listDataItem.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsDefault = \"" + listDataItem.IsDefault + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var listDataItem in demos)
			{
				xml.Append(@"<Demo ");
				xml.Append("Value = \"" + listDataItem.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsDefault = \"" + listDataItem.IsDefault + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var listDataItem in hhis)
			{
				xml.Append(@"<HHI ");
				xml.Append("Value = \"" + listDataItem.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsDefault = \"" + listDataItem.IsDefault + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var listDataItem in geographies)
			{
				xml.Append(@"<Geography ");
				xml.Append("Value = \"" + listDataItem.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsDefault = \"" + listDataItem.IsDefault + "\" ");
				xml.AppendLine(@"/>");
			}
			xml.AppendLine(@"</TargetCustomers>");

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
