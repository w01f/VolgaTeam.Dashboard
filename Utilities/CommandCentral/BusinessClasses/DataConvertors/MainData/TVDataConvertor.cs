namespace CommandCentral.BusinessClasses.DataConvertors.MainData
{
	class TVDataConvertor : MediaDataConvertor
	{
		protected override string MediaName => "TV";

		public TVDataConvertor(string mainDataSourceFilePath, string calendarSourceFilePath, string imagesFolderPath) : base(mainDataSourceFilePath, calendarSourceFilePath, imagesFolderPath)
		{
		}
	}
}
