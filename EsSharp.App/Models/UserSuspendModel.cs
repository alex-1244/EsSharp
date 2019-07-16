using System;

namespace EsSharp.App.Models
{
	public class UserSuspendModel
	{
		public Guid UserId { get; set; }
		public string Reason { get; set; }
	}
}