using CustomerApi.Controllers;
using CustomerApi.Dtos;
using CustomerApi.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Requirements.Bindings
{
	[Binding]
	public class CustomerBindings
	{
		private readonly ScenarioContext _context;
		private readonly CustomerMapper _mapper;
		private readonly SharedCustomer _sharedCustomer;

		public CustomerBindings(
			ScenarioContext context,
			CustomerMapper mapper,
			SharedCustomer sharedCustomer)
		{
			_context = context;
			_mapper = mapper;
			_sharedCustomer = sharedCustomer;
		}

		[Given(@"en kund med följande uppgifter")]
		public void GivetEnKundMedFoljandeUppgifter(Table table)
		{
			var request = _mapper.CreateCustomerRequest(table);
			CreateCustomer(request);
		}

		[Given(@"följande kunder")]
		public void GivetFoljandeKunder(Table table)
		{
			foreach (var request in _mapper.CreateCustomerRequests(table))
			{
				CreateCustomer(request);
			}
		}

		[When(@"kunden anonymiseras")]
		public void NarKundenAnonymiseras()
		{
			Api().Anonymize(_mapper.AnonymizeCustomerRequest());
		}

		[When(@"kunden '(.*)' anonymiseras")]
		public void NarKundenAnonymiseras(string email)
		{
			Api().Anonymize(_mapper.AnonymizeCustomerRequest(email));
		}

		[Then(@"ska kundens uppgifter vara")]
		public void SaSkaKundensUppgifterVara(Table table)
		{
			var expected = _mapper.Customer(table);

			var actual = Api().Get(_mapper.TheOnlyCustomerId);

			AssertCustomer(expected, actual);
		}

		[Then(@"ska följande kunder finnas")]
		public void SaSkaFoljandeKunderFinnas(Table table)
		{
			var actual = Api().Get();

			Assert.AreEqual(table.RowCount, actual.Count, "Wrong number of customers");

			// Assume that the order is important
			var i = 0;
			foreach (var expected in _mapper.Customers(table))
			{
				AssertCustomer(expected, actual[i]);
				i++;
			}
		}

		private CustomerController Api()
		{
			return new CustomerController(_context.Repository());
		}

		private void CreateCustomer(CreateCustomerRequest request)
		{
			var customerId = Api().Create(request);

			_sharedCustomer.CustomerIds[request.Email] = customerId;
		}

		private void AssertCustomer(CustomerDto expected, CustomerDto actual)
		{
			Assert.AreEqual(expected.FirstName, actual.FirstName);
			Assert.AreEqual(expected.LastName, actual.LastName);
			Assert.AreEqual(expected.Email, actual.Email);
		}
	}
}
