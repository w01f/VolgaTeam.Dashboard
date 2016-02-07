using System;
using System.ComponentModel.DataAnnotations;

namespace Asa.Business.Common.Entities.Persistent
{
	public class DBVersion
	{
		[Key]
		public Int64 Id { get; set; }

		[Required]
		public Int64 Revision { get; set; }
	}
}
