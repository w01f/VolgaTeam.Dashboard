﻿using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.OfficeInterops;
using Asa.Solutions.Common.Common;

namespace Asa.Solutions.Common.PresentationClasses
{
	public abstract class BaseSolutionEditor : UserControl, ISolutionEditor
	{
		public BaseSolutionInfo SolutionInfo { get; }

		public string SolutionId => SolutionInfo.Id;
		public abstract SlideType SelectedSlideType { get; }
		public abstract string HelpKey { get; }

		public abstract PowerPointProcessor PowerPointProcessor { get; }

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<SelectedSlideTypeChanged> SlideTypeChanged;
		public event EventHandler<OutputStatusChangedEventArgs> OutputStatusChanged;

		protected BaseSolutionEditor(BaseSolutionInfo solutionInfo)
		{
			Dock = DockStyle.Fill;
			SolutionInfo = solutionInfo;
		}

		#region GUI Methods
		public abstract void InitControl();

		public virtual void ShowEditor()
		{
			BringToFront();
		}
		public abstract void ShowHomeSlide();
		#endregion

		#region Data Management
		public abstract void LoadData();
		public abstract void SaveData();
		public abstract void ApplyChanges();
		public void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
			RaiseOutputStatuesChanged();
		}
		#endregion

		#region Output
		public abstract bool ReadyForOutput { get; }

		public void RaiseSlideTypeChanged()
		{
			SlideTypeChanged?.Invoke(this, new SelectedSlideTypeChanged { SlideType = SelectedSlideType });
		}

		public void RaiseOutputStatuesChanged()
		{
			OutputStatusChanged?.Invoke(this, new OutputStatusChangedEventArgs { IsOutputEnabled = ReadyForOutput });
		}

		public abstract void OutputPowerPoint();
		public abstract void OutputPdf();
		public abstract void Preview();
		public abstract void Email();
		#endregion
	}
}
