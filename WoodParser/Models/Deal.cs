using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodParser.Models
{
	public class Deal
	{
		public string DealNumber { get; set; }
		public int Buyer { get; set; }
		public int Seller { get; set; }
		public decimal BuyerVolume { get; set; }
		public decimal SellerVolume { get; set; }
		public DateTime Date { get; set; }
	}
}
