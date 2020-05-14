using CustomerApi.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CustomerApi.Dtos;

namespace CustomerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController
	{
		private readonly CustomerRepository _repository;

		public CustomerController(CustomerRepository repository)
		{
			_repository = repository;
		}

		[HttpPost]
		public Guid Create(CreateCustomerRequest request)
		{
			var id = Guid.NewGuid();
			_repository.Add(id, new CustomerDto
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email
			});
			return id;
		}

		[HttpPost]
		public void Anonymize(AnonymizeCustomerRequest request)
		{
			var customer = Get(request.CustomerId);

			customer.FirstName = "Anonymiserad";
			customer.LastName = "Anonymiserad";
			customer.Email = string.Empty;
			
			_repository.Save(request.CustomerId, customer);
		}

		[HttpGet]
		public CustomerDto Get(Guid id)
		{
			return _repository.Get(id);
		}

		[HttpGet]
		public List<CustomerDto> Get()
		{
			return _repository.GetAll();
		}
	}
}
