using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.Common;
using CommandCentral.BusinessClasses.Entities.StarApp;

namespace CommandCentral.BusinessClasses.DataConvertors.StarAppData
{
	abstract class BaseStarAppConvertor : IExcel2XmlConvertor
	{
		private readonly string _sourceFilePath;
		protected abstract string SourceSheetName { get; }
		protected abstract string OutputFileName { get; }
		protected abstract string OutputRootNodeName { get; }

		protected BaseStarAppConvertor(string sourceFilePath)
		{
			_sourceFilePath = sourceFilePath;
		}

		protected abstract IList<StartAppDictionaryConfiguration> GetDictionaryConfigurations();

		public void Convert(IList<string> destinationFolderPaths)
		{
			var dictionaryConfigurations = GetDictionaryConfigurations();

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
						var dataAdapter = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", SourceSheetName), connection);
						var dataTable = new DataTable();
						try
						{
							dataAdapter.Fill(dataTable);
							var columnsCount = dataTable.Columns.Count;
							if (columnsCount > 0)
							{
								var processReading = false;
								var dictionaryName = String.Empty;
								foreach (DataRow row in dataTable.Rows)
								{
									var rowValue = row[0]?.ToString().Trim() ?? String.Empty;
									if (String.IsNullOrEmpty(rowValue))
										break;
									if (rowValue.StartsWith("*", StringComparison.OrdinalIgnoreCase))
									{
										processReading = true;
										dictionaryName = rowValue.Replace("*", "");
										continue;
									}
									if (!processReading)
										continue;

									var dictionaryConfiguration =
										dictionaryConfigurations.FirstOrDefault(configItem => String.Equals(configItem.InputTagName, dictionaryName));
									if (dictionaryConfiguration == null)
										continue;

									var listDataItem = new ListDataItem
									{
										Value = rowValue,
										IsDefault = String.Equals(row[1]?.ToString().Trim(), "D", StringComparison.OrdinalIgnoreCase)
									};
									dictionaryConfiguration.ListItems.Add(listDataItem);
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
					}
				}
				else
					throw new ConversionException { SourceFilePath = _sourceFilePath };
				connection.Close();
			}

			var xml = new StringBuilder();
			xml.AppendLine(String.Format("<{0}>", OutputRootNodeName));
			foreach (var dictionaryConfiguration in dictionaryConfigurations)
			{
				foreach (var slideHeader in dictionaryConfiguration.ListItems)
				{
					xml.Append(String.Format("<{0} ", dictionaryConfiguration.OutputNodeName));
					xml.Append("Value = \"" + slideHeader.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + slideHeader.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
			}

			foreach (var externalLine in ConvertorExtensions.GetExternalOutputLines(
				Path.Combine(destinationFolderPaths.First(), OutputFileName),
				OutputRootNodeName,
				dictionaryConfigurations.Select(configItem => configItem.OutputNodeName).ToArray()))
				xml.AppendLine(externalLine);

			xml.AppendLine(String.Format("</{0}>", OutputRootNodeName));

			foreach (var folderPath in destinationFolderPaths)
			{
				var xmlPath = Path.Combine(folderPath, OutputFileName);
				using (var sw = new StreamWriter(xmlPath, false))
				{
					sw.Write(xml.ToString());
					sw.Flush();
				}
			}
		}
	}
}
