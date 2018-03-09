using System;
using System.Drawing;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class CoverState
	{
		public ListDataItem SlideHeader { get; set; }
		public bool AddAsPageOne { get; set; }
		public Image Clipart1 { get; set; }
		public string Subheader1 { get; set; }
		public DateTime? Calendar1 { get; set; }
		public User Combo1 { get; set; }
		
		public CoverState()
		{
			AddAsPageOne = true;
		}
	}
}
