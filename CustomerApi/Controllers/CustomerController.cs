using CustomerApi.Requests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController
	{
		[HttpPost]
		public void Create(CreateCustomerRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
