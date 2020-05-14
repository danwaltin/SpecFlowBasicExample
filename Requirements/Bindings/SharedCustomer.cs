using System;
using System.Collections.Generic;

namespace Requirements.Bindings
{
	public class SharedCustomer
	{
		public Guid IdOfLastCreatedCustomer { get; set; }
		public Dictionary<string, Guid> CustomerIds { get; set; } = new Dictionary<string, Guid>();
	}
}
