using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.ShopBoundedContext;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.App.Models
{
	public class CustomerFundBalanceModel
	{
		public int Amount { get; set; }
	}

	public class OrderModel
	{
		public Guid? ManagerId { get; set; }

		public string ManagerName { get; set; }

		public bool IsPayed { get; set; }

		public IEnumerable<ProductModel> Products { get; set; }

		public Guid Id { get; set; }

		public OrderModel() { }

		public OrderModel(Order order)
		{
			this.Id = order.Id;
			this.Products = order.Products.Select(x => new ProductModel(x));
			this.IsPayed = order.IsPayed;
			this.ManagerName = order.Manager?.Name;
			this.ManagerId = order.Manager?.Id;
		}
	}

	public class ProductModel
	{
		public int Price { get; set; }

		public string Type { get; set; }

		public ProductModel() { }

		public ProductModel(Product product)
		{
			this.Price = product.Price;
			this.Type = product.Type;
		}
	}
}