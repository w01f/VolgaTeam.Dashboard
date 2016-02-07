using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalProductsContainer
	{
		IDigitalProductsContent DigitalProductsContent { get; }
		bool SettingsNotSaved { get; set; }
		Theme SelectedTheme { get; }
	}
}
