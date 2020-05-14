using System;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Dtos;
using CustomerApi.Requests;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	public class CustomerMapper
	{
		private readonly SharedCustomer _sharedCustomer;

		public CustomerMapper(SharedCustomer sharedCustomer)
		{
			_sharedCustomer = sharedCustomer;
		}

		public CreateCustomerRequest CreateCustomerRequest(Table table) => new CreateCustomerRequest
			{
				FirstName = GetValue(table, "Förnamn"),
				LastName = GetValue(table, "Efternamn"),
				Email = GetValue(table, "Epost")
			};

		public IEnumerable<CreateCustomerRequest> CreateCustomerRequests(Table table) =>
			table.Rows.Select(CreateCustomerRequest);

		public CustomerDto Customer(Table table) => new CustomerDto
		{
			FirstName = GetValue(table, "Förnamn"),
			LastName = GetValue(table, "Efternamn"),
			Email = GetValue(table, "Epost")
		};

		public CustomerDto Customer(TableRow row) => new CustomerDto
		{
			FirstName = row["Förnamn"],
			LastName = row["Efternamn"],
			Email = row["Epost"]
		};

		public IEnumerable<CustomerDto> Customers(Table table) =>
			table.Rows.Select(Customer);

		private CreateCustomerRequest CreateCustomerRequest(TableRow row) => new CreateCustomerRequest
		{
			FirstName = row["Förnamn"],
			LastName = row["Efternamn"],
			Email = row["Epost"]
		};

		private string GetValue(Table table, string uppgift)
		{
			var row = table.Rows.First(row => row["Uppgift"] == uppgift);
			return row["Värde"];
		}

		public AnonymizeCustomerRequest AnonymizeCustomerRequest()
		{
			return new AnonymizeCustomerRequest
			{
				CustomerId = TheOnlyCustomerId
			};
		}

		public AnonymizeCustomerRequest AnonymizeCustomerRequest(string email)
		{
			return new AnonymizeCustomerRequest
			{
				CustomerId = CustomerId(email)
			};
		}

		public Guid TheOnlyCustomerId => _sharedCustomer.CustomerIds.First().Value;

		private Guid CustomerId(string email) => _sharedCustomer.CustomerIds[email];
	}
}