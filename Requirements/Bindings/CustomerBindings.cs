using CustomerApi.Controllers;
using CustomerApi.Requests;
using System.Linq;
using CustomerApi.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	[Binding]
	public class CustomerBindings
	{
		private readonly SharedCustomer _sharedCustomer;

		public CustomerBindings(SharedCustomer sharedCustomer)
		{
			_sharedCustomer = sharedCustomer;
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

			var api = new CustomerController();

			var customerId = api.Create(request);

			_sharedCustomer.IdOfLastCreatedCustomer = customerId;
		}

		[When(@"kunden anonymiseras")]
		public void NarKundenAnonymiseras()
		{
			var request = new AnonymizeCustomerRequest
			{
				CustomerId = _sharedCustomer.IdOfLastCreatedCustomer
			};

			var api = new CustomerController();

			api.Anonymize(request);
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

			var api = new CustomerController();

			var actual = api.Get(_sharedCustomer.IdOfLastCreatedCustomer);

			Assert.AreEqual(expected.FirstName, actual.FirstName);
			Assert.AreEqual(expected.LastName, actual.LastName);
			Assert.AreEqual(expected.Email, actual.Email);
		}

		private string GetValue(Table table, string uppgift)
		{
			var row = table.Rows.First(row => row["Uppgift"] == uppgift);
			return row["Värde"];
		}
	}
}
