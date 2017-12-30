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
				var slideHeaders = new List<SlideHeader>();
				var demos = new List<string>();
				var hhis = new List<string>();
				var geographies = new List<string>();

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

								var slideHeader = new SlideHeader();
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

								demos.Add(rowValue);
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

								hhis.Add(rowValue);
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

								geographies.Add(rowValue);
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

					demos.Sort(WinAPIHelper.StrCmpLogicalW);
					hhis.Sort(WinAPIHelper.StrCmpLogicalW);
					geographies.Sort(WinAPIHelper.StrCmpLogicalW);
				}

				connection.Close();

				var xml = new StringBuilder();
				xml.AppendLine("<TargetCustomers>");
				foreach (SlideHeader header in slideHeaders)
				{
					xml.Append(@"<SlideHeader ");
					xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (string demo in demos)
				{
					xml.Append(@"<Demo ");
					xml.Append("Value = \"" + demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (string hhi in hhis)
				{
					xml.Append(@"<HHI ");
					xml.Append("Value = \"" + hhi.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (string geography in geographies)
				{
					xml.Append(@"<Geography ");
					xml.Append("Value = \"" + geography.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
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
			else
				throw new ConversionException { SourceFilePath = _sourceFilePath };
		}
	}
}
