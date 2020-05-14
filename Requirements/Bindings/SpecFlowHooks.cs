using CustomerApi.Controllers;
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
			_context.SetRepository(new CustomerRepository());
		}
	}
}