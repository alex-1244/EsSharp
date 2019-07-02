using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.Tests.TestEntities.Events;

namespace EsSharp.Tests.TestEntities
{
	[Serializable]
	public class TyreShop : Aggregate
	{
		private List<Tyre> TyreReserve { get; set; }
		private int Money { get; set; }

		public TyreShop()
		{
			this.TyreReserve = new List<Tyre>()
			{
				new Tyre{ Price = 10 }
			};
		}

		public void ServiceCar(Car car)
		{
			if (this.TyreReserve.Any())
			{
				var tyre = TyreReserve.First();
				car.ChangeTyre(tyre, TyrePlacement.LeftFront);
				this.TyreReserve.RemoveAt(0);
				this.PublishEvent(new TyreChangedEvent(this.Id, this.Version, tyre, TyrePlacement.LeftFront));
			}
		}

		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic)@event);
		}

		private void HandleEvent(TyreChangedEvent ev)
		{
			this.Money += ev.Tyre.Price;
			this.TyreReserve.Remove(ev.Tyre);
		}
	}

	[Serializable]
	public class Car : Aggregate
	{
		public Car()
		{
			this.Id = Guid.NewGuid();
		}

		public Tyre LeftFrontTyre { get; private set; }
		public Tyre RightFrontTyre { get; private set; }
		public Tyre LeftBackTyre { get; private set; }
		public Tyre RightBackTyre { get; private set; }

		public void ChangeTyre(Tyre newTyre, TyrePlacement tyrePlacement)
		{
			SetTyre(newTyre, tyrePlacement);
			this.PublishEvent(new TyreChangedEvent(this.Id, this.Version, newTyre, tyrePlacement));
		}

		private void SetTyre(Tyre newTyre, TyrePlacement tyrePlacement)
		{
			switch (tyrePlacement)
			{
				case TyrePlacement.LeftFront:
					this.LeftFrontTyre = newTyre;
					break;
				case TyrePlacement.RightFront:
					this.RightFrontTyre = newTyre;
					break;
				case TyrePlacement.LeftBack:
					this.LeftBackTyre = newTyre;
					break;
				case TyrePlacement.RightBack:
					this.RightBackTyre = newTyre;
					break;
				default:
					throw new ArgumentException("Tyre placement is unknown");
			}
		}

		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic)@event);
		}

		private void HandleEvent(TyreChangedEvent ev)
		{
			this.SetTyre(ev.Tyre, ev.Placement);
		}
	}
}
