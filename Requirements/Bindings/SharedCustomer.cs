using System;
using System.Collections.Generic;

namespace Requirements.Bindings
{
	public class SharedCustomer
	{
		public Dictionary<string, Guid> CustomerIds { get; set; } = new Dictionary<string, Guid>();
	}
}
