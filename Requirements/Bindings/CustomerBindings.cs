using CustomerApi.Controllers;
using CustomerApi.Requests;
using System.Linq;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	[Binding]
	public class CustomerBindings
	{
		[Given(@"en kund med följande uppgifter")]
		public void GivetEnKundMedFoljandeUppgifter(Table table)
		{
			var request = new CreateCustomerRequest
			{
				FirstName = GetUppgiftValue(table, "Förnamn"),
				LastName = GetUppgiftValue(table, "Efternamn"),
				Email = GetUppgiftValue(table, "Epost")
			};

			var api = new CustomerController();

			api.Create(request);
		}

		[When(@"kunden anonymiseras")]
		public void NarKundenAnonymiseras()
		{
			ScenarioContext.Current.Pending();
		}

		[Then(@"ska kundens uppgifter vara")]
		public void SaSkaKundensUppgifterVara(Table table)
		{
			ScenarioContext.Current.Pending();
		}

		private string GetUppgiftValue(Table table, string uppgift)
		{
			var row = table.Rows.First(row => row["Uppgift"] == uppgift);
			return row["Värde"];
		}
	}
}
