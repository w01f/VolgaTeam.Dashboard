using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public interface IProductToggleContainerInfo
	{
		IList<IProductSubTabInfo> GetSubTabInfoList();
	}
}
