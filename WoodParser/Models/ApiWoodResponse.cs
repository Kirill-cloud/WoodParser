using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodParser.Models
{
	public class ApiWoodResponse
	{
		public Data Data { get; set; }
	}

	public class Data
	{
		public SearchReport SearchReportWoodDeal { get; set; }
	}
	public class SearchReport
	{
		public List<DealExternal> Content { get; set; }
		public long total { get; set; }
	}
	
	public class DealExternal
	{
		public string sellerName { get; set; }
		public string sellerInn { get; set; }
		public string buyerName { get; set; }
		public string buyerInn { get; set; }
		public decimal woodVolumeBuyer { get; set; }
		public decimal woodVolumeSeller { get; set; }
		public DateTime dealDate { get; set; }
		public string dealNumber { get; set; } 
	}
}
