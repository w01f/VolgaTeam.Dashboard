namespace CommandCentral.BusinessClasses.DataConvertors.MainData
{
	class RadioDataConvertor : MediaDataConvertor
	{
		protected override string MediaName => "Radio";

		public RadioDataConvertor(string mainDataSourceFilePath, string calendarSourceFilePath, string imagesFolderPath) : base(mainDataSourceFilePath, calendarSourceFilePath, imagesFolderPath)
		{
		}
	}
}
