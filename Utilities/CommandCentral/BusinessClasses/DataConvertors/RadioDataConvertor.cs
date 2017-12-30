namespace CommandCentral.BusinessClasses.DataConvertors
{
	class RadioDataConvertor : MediaDataConvertor
	{
		protected override string MediaName => "Radio";

		public RadioDataConvertor(string mainDataSourceFilePath, string calendarSourceFilePath, string imagesFolderPath) : base(mainDataSourceFilePath, calendarSourceFilePath, imagesFolderPath)
		{
		}
	}
}
