using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoodParser.Models;

namespace WoodParser.DataLayer
{
	public class OrganizationProvider
	{
        private readonly SqlDataAccess db;
        public OrganizationProvider(IConfiguration config)
        {
            db = new SqlDataAccess();
        }

		public List<Organization> GetOrganizations()
		{
			string sql = $"SELECT * FROM Subject";
		
			return db.LoadData<Organization>(sql, null);
		}

		public Organization GetOrganization(string inn)
		{
			string sql = @"SELECT * FROM Subject
WHERE Inn = @inn";

			return db.LoadSingle<Organization>(sql, SqlDataAccess.BuildSingleParam("inn",inn));
		}

		public int Insert(Dictionary<string, object> org)
		{
			return db.Insert<int>("Subject", org);
		}
	}
}
