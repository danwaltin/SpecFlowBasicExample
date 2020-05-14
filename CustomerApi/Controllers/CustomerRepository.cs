using System;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Dtos;

namespace CustomerApi.Controllers
{
	public class CustomerRepository
	{
		private Dictionary<Guid, CustomerDto> _customerRepository = new Dictionary<Guid, CustomerDto>();

		public void Add(Guid id, CustomerDto customer)
		{
			_customerRepository[id] = Clone(customer);
		}

		public CustomerDto Get(Guid id)
		{
			return Clone(_customerRepository[id]);
		}

		public List<CustomerDto> GetAll()
		{
			return _customerRepository.Values.Select(Clone).ToList();
		}

		public void Save(Guid id, CustomerDto customer)
		{
			_customerRepository[id] = Clone(customer);
		}

		private CustomerDto Clone(CustomerDto customer)
		{
			return new CustomerDto
			{
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email =  customer.Email
			};
		}
	}
}