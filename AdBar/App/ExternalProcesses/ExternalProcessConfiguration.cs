namespace Asa.Bar.App.ExternalProcesses
{
	class ExternalProcessConfiguration
	{
		public string Name { get; set; }
		public ExternalProcessBehaviour Behaviour { get; set; }

		public ExternalProcessConfiguration()
		{
			Name = null;
			Behaviour = ExternalProcessBehaviour.HideIfIsActive;
		}
	}
}
