using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodParser.Models
{
	public class ApiQuery
	{
		public string Query { get; set; }
		public Variables Variables { get; set; }
		public string OperationName { get; set; }
	}
}
