using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.Entities.Dashboard;

namespace CommandCentral.BusinessClasses.DataConvertors.MainData
{
	class UsersConvertor : IExcel2XmlConvertor
	{
		private const string DestinationFileName = "Users.xml";
		private readonly string _sourceFilePath;

		public UsersConvertor(string sourceFilePath)
		{
			_sourceFilePath = sourceFilePath;
		}

		public void Convert(IList<string> destinationFolderPaths)
		{
			var users = new List<User>();
			var connnectionString =
				String.Format(
					@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";",
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
					var dataAdapter = new OleDbDataAdapter("SELECT * FROM [users$]", connection);
					var dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						var columnsCount = dataTable.Columns.Count;
						if (columnsCount > 0)
							foreach (DataRow row in dataTable.Rows)
							{

								var rowValue = row[0]?.ToString().Trim() ?? String.Empty;
								if (String.IsNullOrEmpty(rowValue))
									break;

								var user = new User();
								user.FirstName = rowValue;
								user.LastName = row[1]?.ToString().Trim() ?? String.Empty;
								user.Email = row[2]?.ToString().Trim() ?? String.Empty;
								user.Phone = row[3]?.ToString().Trim() ?? String.Empty;
								user.Login = row[4]?.ToString().Trim() ?? String.Empty;
								user.IsAdmin = String.Equals(row[5]?.ToString().Trim(), "yes", StringComparison.OrdinalIgnoreCase);
								user.Title = row[6]?.ToString().Trim() ?? String.Empty;

								var groupColumnCount = 7;
								while (columnsCount > groupColumnCount)
								{
									var station = row[groupColumnCount]?.ToString().Trim() ?? String.Empty;
									if (String.IsNullOrWhiteSpace(station))
										break;

									user.Groups.Add(station);
									groupColumnCount++;
								}

								if (!String.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.LastName))
									users.Add(user);
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
				else
					throw new ConversionException { SourceFilePath = _sourceFilePath };
				connection.Close();
			}

			var xml = new StringBuilder();
			xml.AppendLine("<Users>");
			xml.AppendLine("<TotalUsers>" + users.Count + "</TotalUsers>");
			foreach (User user in users)
			{
				xml.Append(@"<User ");
				xml.Append("FirstName = \"" + user.FirstName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("LastName = \"" + user.LastName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Phone = \"" + user.Phone.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Email = \"" + user.Email.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsAdmin = \"" + user.IsAdmin + "\" ");
				xml.Append("Title = \"" + user.Title.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@">");
				foreach (var userGroup in user.Groups)
					xml.AppendLine(String.Format("<Group>{0}</Group>", userGroup));
				xml.AppendLine(@"</User>");
			}
			xml.AppendLine(@"</Users>");

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
