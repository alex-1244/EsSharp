using System;
using System.Linq;
using EsSharp.App.Models;
using EsSharp.Projections;
using EsSharp.ShopBoundedContext.Customers;
using Microsoft.AspNetCore.Mvc;
using User = EsSharp.UserManagementBoundedContext.Users.User;

namespace EsSharp.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly SynchronizedAggregateStore _aggregateStore;

		public CustomerController(SynchronizedAggregateStore aggregateStore)
		{
			_aggregateStore = aggregateStore;
		}

		[HttpPost("create")]
		public Guid Create([FromBody] CustomerCreationModel customer)
		{
			var domainCustomer = new Customer(customer.UserId, customer.Name);

			this._aggregateStore.SaveAggregateAndEvents(domainCustomer);

			return domainCustomer.Id;
		}

		[HttpPost("{customerId}/fund")]
		public ActionResult FundBalance([FromRoute] Guid customerId, [FromBody] CustomerFundBalanceModel model)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			customer.Fund(model.Amount);

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok();
		}

		[HttpPost("{customerId}/createOrder")]
		public ActionResult CreateOrder()
		{
		}
	}
}
