using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodParser.Models
{
	public class DefaultWoodQuery : ApiQuery
	{
		public DefaultWoodQuery(int pageNumber)
		{
			Query = "query SearchReportWoodDeal($size: Int!, $number: Int!, $filter: Filter, $orders: [Order!]) {\n  searchReportWoodDeal(filter: $filter, pageable: {number: $number, size: $size}, orders: $orders) {\n    content {\n      sellerName\n      sellerInn\n      buyerName\n      buyerInn\n      woodVolumeBuyer\n      woodVolumeSeller\n      dealDate\n      dealNumber\n      __typename\n    }\n    __typename\n  }\n}\n";
			Variables = new Variables()
			{
				Size = 20, // 20 как стандартные запросы при просмотре страниц
				Number = pageNumber,
			};
			OperationName = "SearchReportWoodDeal";
		}
	}
}
