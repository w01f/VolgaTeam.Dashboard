using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalProductsContainer
	{
		IDigitalProductsContent DigitalProductsContent { get; }
		Theme SelectedTheme { get; }
		void RaiseDataChanged();
		void LoadProduct(IDigitalProductControl productControl);
	}
}
