using CustomerApi.Controllers;
using CustomerApi.Requests;
using System.Linq;
using CustomerApi.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	[Binding]
	public class SpecFlowHooks
	{
		private readonly ScenarioContext _context;

		public SpecFlowHooks(ScenarioContext context)
		{
			_context = context;
		}

		[BeforeScenario]
		public void BeforeScenario()
		{
			_context["Repository"] = new CustomerRepository();
		}
	}

	[Binding]
	public class CustomerBindings
	{
		private readonly ScenarioContext _context;
		private readonly SharedCustomer _sharedCustomer;

		public CustomerBindings(
			ScenarioContext context, 
			SharedCustomer sharedCustomer)
		{
			_context = context;
			_sharedCustomer = sharedCustomer;
		}

		private CustomerController Api()
		{
			var repository = (CustomerRepository) _context["Repository"];
			return new CustomerController(repository);
		}

		[Given(@"en kund med följande uppgifter")]
		public void GivetEnKundMedFoljandeUppgifter(Table table)
		{
			var request = new CreateCustomerRequest
			{
				FirstName = GetValue(table, "Förnamn"),
				LastName = GetValue(table, "Efternamn"),
				Email = GetValue(table, "Epost")
			};

			var customerId = Api().Create(request);

			_sharedCustomer.CustomerIds[request.Email] = customerId;
		}

		[Given(@"följande kunder")]
		public void GivetFoljandeKunder(Table table)
		{
			foreach (var row in table.Rows)
			{
				var request = new CreateCustomerRequest
				{
					FirstName = row["Förnamn"],
					LastName = row["Efternamn"],
					Email = row["Epost"]
				};

				var customerId = Api().Create(request);

				_sharedCustomer.CustomerIds[request.Email] = customerId;
			}
		}

		[When(@"kunden anonymiseras")]
		public void NarKundenAnonymiseras()
		{
			var request = new AnonymizeCustomerRequest
			{
				CustomerId = _sharedCustomer.CustomerIds.First().Value
			};

			Api().Anonymize(request);
		}

		[When(@"kunden '(.*)' anonymiseras")]
		public void NarKundenAnonymiseras(string email)
		{
			var request = new AnonymizeCustomerRequest
			{
				CustomerId = _sharedCustomer.CustomerIds[email]
			};

			Api().Anonymize(request);
		}

		[Then(@"ska kundens uppgifter vara")]
		public void SaSkaKundensUppgifterVara(Table table)
		{
			var expected = new CustomerDto
			{
				FirstName = GetValue(table, "Förnamn"),
				LastName = GetValue(table, "Efternamn"),
				Email = GetValue(table, "Epost")
			};

			var actual = Api().Get(_sharedCustomer.CustomerIds.First().Value);

			Assert.AreEqual(expected.FirstName, actual.FirstName);
			Assert.AreEqual(expected.LastName, actual.LastName);
			Assert.AreEqual(expected.Email, actual.Email);
		}

		[Then(@"ska följande kunder finnas")]
		public void SaSkaFoljandeKunderFinnas(Table table)
		{
			var actual = Api().Get();

			Assert.AreEqual(table.RowCount, actual.Count, "Wrong number of customers");

			// Assume that the order is important
			for (var i = 0; i < table.RowCount; i++)
			{
				var row = table.Rows[i];
				var expected = new CustomerDto
				{
					FirstName = row["Förnamn"],
					LastName = row["Efternamn"],
					Email = row["Epost"]
				};

				Assert.AreEqual(expected.FirstName, actual[i].FirstName);
				Assert.AreEqual(expected.LastName, actual[i].LastName);
				Assert.AreEqual(expected.Email, actual[i].Email);
			}

		}

		private string GetValue(Table table, string uppgift)
		{
			var row = table.Rows.First(row => row["Uppgift"] == uppgift);
			return row["Värde"];
		}
	}
}
