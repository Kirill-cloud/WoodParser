using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WoodParser.DataLayer;
using WoodParser.Models;

namespace WoodParser
{
	internal class Program
	{
		static int pageSize = 20;
		static void Main(string[] args)
		{
			Console.WriteLine("started");
			DealProvider dealProvider = new DealProvider(null); // надо из конфига бд сделать, но мне лень пока
			OrganizationProvider organizationProvider = new OrganizationProvider(null);

			int j = 1;
			while (true)
			{
				Console.WriteLine($"Итерация: {j}");
				RestClient client = new RestClient("https://www.lesegais.ru");
				RestRequest request = new RestRequest("open-area/graphql", Method.Post);
				request.AddJsonBody(new DefaultWoodQueryCount());

				var response = client.Execute(request);
				var count = JsonConvert.DeserializeObject<ApiWoodResponse>(response.Content).Data.SearchReportWoodDeal.total;

				for (int i = 0; i <= count / pageSize/10000; i++)
				{
					request = new RestRequest("open-area/graphql", Method.Post);
					request.AddJsonBody(new DefaultWoodQuery(i));
					response = client.Execute(request);
					var deals = JsonConvert.DeserializeObject<ApiWoodResponse>(response.Content).Data.SearchReportWoodDeal.Content;

					foreach (var item in deals)
					{
						try
						{
							var deal = dealProvider.GetDeal(item.dealNumber);
							if (deal != null)
							{
								Console.WriteLine($"Сделка {item.dealNumber} уже добавлена");
								continue;
							}
						}
						catch (Exception) // если нет записи бросается юсключение на пустую последовательность
						{
							int sellerId;
							try
							{
								sellerId = organizationProvider.GetOrganization(item.sellerInn).Id;
								Console.WriteLine($"Используем старого субъекта с ИНН: {item.sellerInn}");
							}
							catch (Exception)
							{
								sellerId = organizationProvider.Insert(new Dictionary<string, object>()
								{
									{ "Name", item.sellerName },
									{ "Inn", item.sellerInn },
								});
								Console.WriteLine($"Новый субъект с ИНН: {item.sellerInn}");
							}

							int buyerId;
							try
							{
								buyerId = organizationProvider.GetOrganization(item.buyerInn).Id;
								Console.WriteLine($"Используем старого субъекта с ИНН: {item.buyerInn}");
							}
							catch (Exception)
							{
								buyerId = organizationProvider.Insert(new Dictionary<string, object>()
								{
									{ "Name", item.buyerName },
									{ "Inn", item.buyerInn },
								});
								Console.WriteLine($"Новый субъект с ИНН: {item.buyerInn}");
							}

							var deal = dealProvider.Insert(new Dictionary<string, object>()
							{
								{"dealNumber",item.dealNumber},
								{"date",item.dealDate.ToString("yyyy.MM.dd")},
								{"seller", sellerId },
								{"buyer", buyerId},
								{"sellerVolume", item.woodVolumeSeller},
								{"buyerVolume", item.woodVolumeBuyer},
							}); ;
							Console.WriteLine($"Сделка {item.dealNumber} добавлена");
						}
						Console.WriteLine("".PadLeft(20,'-'));
					}
					Console.WriteLine("".PadLeft(20, '='));
					Console.WriteLine(String.Format("{0:0.00}%",(double)i* pageSize / count * 100));
				}
				j++;
				var tenMinutes = 600;
				for (int i = tenMinutes; i > 0 ; i--)
				{
					Console.Write(".");
					if (i%5 == 0)
					{
						Console.WriteLine($"До перезапуска {i} секунд...");
					}
					Thread.Sleep(1000);
				}
			}
		}
	}
}
