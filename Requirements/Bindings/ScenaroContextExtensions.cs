using CustomerApi.Controllers;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	public static class ScenaroContextExtensions
	{
		public static CustomerRepository Repository(this ScenarioContext context)
		{
			return (CustomerRepository) context["CustomerRepository"];
		}

		public static void SetRepository(this ScenarioContext context, CustomerRepository repository)
		{
			context["CustomerRepository"] = repository;
		}
	}
}