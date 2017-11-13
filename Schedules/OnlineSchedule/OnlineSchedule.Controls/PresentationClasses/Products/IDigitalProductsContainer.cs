using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.Core.OfficeInterops;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalProductsContainer
	{
		IDigitalProductsContent DigitalProductsContent { get; }
		Theme SelectedTheme { get; }
		PowerPointProcessor PowerPointProcessor { get; }
		void RaiseDataChanged();
		void LoadProduct(IDigitalProductControl productControl);
	}
}
