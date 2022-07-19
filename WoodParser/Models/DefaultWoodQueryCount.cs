using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodParser.Models
{
	public class DefaultWoodQueryCount : ApiQuery
	{
		public DefaultWoodQueryCount()
		{
			Query = "query SearchReportWoodDealCount($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!]) {\n  searchReportWoodDeal(filter: $filter, pageable: {number: $number, size: $size}, orders: $orders) {\n    total\n    number\n    size\n    overallBuyerVolume\n    overallSellerVolume\n    __typename\n  }\n}\n";
			Variables = new Variables()
			{
				Size = 20, // 20 как стандартные запросы при просмотре страниц
				Number = 0,
			};
			OperationName = "SearchReportWoodDealCount";
		}
	}
}
