using Asa.Business.Online.Entities.NonPersistent;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalProductControl : IDigitalSlideControl
	{
		DigitalProduct Product { get; }
	}
}
