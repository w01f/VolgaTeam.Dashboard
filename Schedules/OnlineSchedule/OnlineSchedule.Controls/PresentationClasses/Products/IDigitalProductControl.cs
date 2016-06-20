using Asa.Business.Online.Entities.NonPersistent;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalProductControl
	{
		DigitalProduct Product { get; }
		void Release();
	}
}
