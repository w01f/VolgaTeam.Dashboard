namespace Asa.Bar.App.ExternalProcesses
{
	enum ExternalProcessBehaviour
	{
		HideIfIsActive,
		HideIfIsActiveAndMaximized,
		SetNotOnTopIfIsActive,
		HideIfProcessIsRunning,
		HideIfTitlebarMatches
	}

	enum BarVsProcessStatus
	{
		OnTop,
		Hidden,
		NotOnTop
	}
}
