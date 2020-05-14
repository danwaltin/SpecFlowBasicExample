using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	[Binding]
	public class CustomerBindings
	{
		[Given(@"en kund med följande uppgifter")]
		public void GivetEnKundMedFoljandeUppgifter(Table table)
		{
			ScenarioContext.Current.Pending();
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
	}
}
