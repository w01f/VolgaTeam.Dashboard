using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.Calendar
{
	public class CalendarTemplatesManager
	{
		public List<CalendarTemplate> CalendarTemplates { get; set; }

		public CalendarTemplatesManager()
		{
			CalendarTemplates = new List<CalendarTemplate>();
		}

		public void LoadCalendarTemplates()
		{
			CalendarTemplates.Clear();

			var legendFile = new StorageFile(ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts.Merge("FileLegend.xls"));
			var connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", legendFile.LocalPath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch
			{ }

			if (connection.State != ConnectionState.Open) return;
			
			//Load Headers
			var dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connection);
			var dataTable = new DataTable();

			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						for (int i = 0; i < 2; i++)
						{
							for (int j = 0; j < 6; j++)
							{
								for (int k = 0; k < 2; k++)
								{
									var template = new CalendarTemplate();
									var tempDate = DateTime.MinValue;
									DateTime.TryParse(row[0].ToString().Trim(), out tempDate);
									template.Month = tempDate.ToString("MMM-yy");
									switch (i)
									{
										case 0:
											template.HasLogo = true;
											break;
										case 1:
											template.HasLogo = false;
											break;
									}
									switch (j)
									{
										case 0:
											template.Color = "Gray";
											break;
										case 1:
											template.Color = "Black";
											break;
										case 2:
											template.Color = "Blue";
											break;
										case 3:
											template.Color = "Green";
											break;
										case 4:
											template.Color = "Orange";
											break;
										case 5:
											template.Color = "Teal";
											break;
									}
									switch (k)
									{
										case 0:
											template.IsLarge = true;
											break;
										case 1:
											template.IsLarge = false;
											break;
									}
									int logoCellNumber = i + 1;
									int colorCellNumber = (j * 2) + 3;
									int sizeCellNumber = colorCellNumber + k;

									if (dataTable.Columns.Count > logoCellNumber)
										if (row[logoCellNumber] != null)
											template.TemplateName = row[logoCellNumber].ToString().Trim();
									if (dataTable.Columns.Count > sizeCellNumber)
										if (row[sizeCellNumber] != null)
											template.SlideMaster = row[sizeCellNumber].ToString().Trim();
									if (!string.IsNullOrEmpty(template.Month) && !string.IsNullOrEmpty(template.TemplateName) && !string.IsNullOrEmpty(template.SlideMaster))
										CalendarTemplates.Add(template);
								}
							}
						}
					}
			}
			catch { }
			finally
			{
				dataAdapter.Dispose();
				dataTable.Dispose();
			}
			connection.Close();
		}

		public string GetSlideName(CalendarOutputData outputData)
		{
			string result = string.Empty;
			var template = CalendarTemplates.FirstOrDefault(t =>
				t.IsLarge == outputData.ShowBigDate &&
				t.HasLogo == outputData.ShowLogo &&
				t.Color.ToLower().Equals(outputData.SlideColor.ToLower()) &&
				t.Month.ToLower().Equals(outputData.Parent.Date.ToString("MMM-yy").ToLower()));
			if (template != null)
				result = template.TemplateName;
			return result;
		}

		public string GetSlideMasterName(CalendarOutputData outputData)
		{
			string result = string.Empty;
			var template = CalendarTemplates.FirstOrDefault(t =>
				t.IsLarge == outputData.ShowBigDate &&
				t.HasLogo == outputData.ShowLogo &&
				t.Color.ToLower().Equals(outputData.SlideColor.ToLower()) &&
				t.Month.ToLower().Equals(outputData.Parent.Date.ToString("MMM-yy").ToLower()));
			if (template != null)
				result = template.SlideMaster;
			return result;
		}
	}

	public class CalendarTemplate
	{
		public CalendarTemplate()
		{
			Month = string.Empty;
			TemplateName = string.Empty;
			SlideMaster = string.Empty;
		}

		public string Month { get; set; }
		public bool HasLogo { get; set; }
		public bool IsLarge { get; set; }
		public string Color { get; set; }
		public string TemplateName { get; set; }
		public string SlideMaster { get; set; }
	}
}
