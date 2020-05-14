using CustomerApi.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using CustomerApi.Dtos;

namespace CustomerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController
	{
		private static CustomerDto _customer;

		[HttpPost]
		public Guid Create(CreateCustomerRequest request)
		{
			return Guid.NewGuid();
		}

		[HttpPost]
		public void Anonymize(AnonymizeCustomerRequest request)
		{
			_customer = new CustomerDto
			{
				FirstName = "Anonymiserad",
				LastName = "Anonymiserad",
				Email = string.Empty
			};
		}

		[HttpGet]
		public CustomerDto Get(Guid id)
		{
			return _customer;
		}
	}
}
