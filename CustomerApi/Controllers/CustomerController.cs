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
		[HttpPost]
		public Guid Create(CreateCustomerRequest request)
		{
			return Guid.NewGuid();
		}

		[HttpPost]
		public void Anonymize(AnonymizeCustomerRequest request)
		{
		}

		[HttpGet]
		public CustomerDto Get(Guid id)
		{
			return new CustomerDto();
		}
	}
}
