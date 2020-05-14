using CustomerApi.Controllers;
using CustomerApi.Requests;
using System.Linq;
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
			ScenarioContext.Current.Pending();
		}

		private string GetValue(Table table, string uppgift)
		{
			var row = table.Rows.First(row => row["Uppgift"] == uppgift);
			return row["Värde"];
		}
	}
}
