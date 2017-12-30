using System.Collections.Generic;

namespace CommandCentral.BusinessClasses.DataConvertors
{
	public interface IExcel2XmlConvertor
	{
		void Convert(IList<string> destinationFolderPaths);
	}
}
