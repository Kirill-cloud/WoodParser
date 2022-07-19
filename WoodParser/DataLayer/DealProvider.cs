using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoodParser.Models;

namespace WoodParser.DataLayer
{
	public class DealProvider
	{
        private readonly SqlDataAccess db;
        public DealProvider(IConfiguration config)
        {
            db = new SqlDataAccess();
        }

		public List<Deal> GetDeals()
		{
			string sql = $"SELECT * FROM Subject";
		
			return db.LoadData<Deal>(sql, null);
		}

		public Deal GetDeal(string number)
		{
			string sql = @"SELECT * FROM Deal
WHERE DealNumber = @number";

			return db.LoadSingle<Deal>(sql, SqlDataAccess.BuildSingleParam("number", number));
		}

		public string Insert(Dictionary<string, object> org)
		{
			return db.Insert<string>("Deal", org, "dealNumber");
		}
	}
}
