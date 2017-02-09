using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using XF_FetchJson.Models;
using XLabs;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Threading;

namespace XF_FetchJson.Pages
{
	public class fetchPage : ContentPage
	{
		private static ListView fetchlv;
		private static Button fetchBtn;
		private static Entry getID;


		private static string fetchurl = string.Format("http://jsonplaceholder.typicode.com/users/");

		

		public fetchPage ()
		{
			fetchlv = new ListView() { RowHeight = 40 };
			fetchBtn = new Button() { Text = "Fetch Json" };
			getID = new Entry() { Placeholder = "User id #" };
			
			double topPadding = Device.OnPlatform<double>(20, 0, 0);

			var stack = new StackLayout() {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, 25, 0, 0),
				Children = { getID, fetchBtn, fetchlv },
			};
			this.Padding = new Thickness(0, topPadding, 0, 0);
			this.Content = stack;
			getData("1");
		}

		
		


		async static Task<string> getData(string id) {

			User user = new User();
			

			try {
				var client = new HttpClient();

				client.DefaultRequestHeaders.Add("Accept", "application/json");
				var address = $"http://jsonplaceholder.typicode.com/{id}";

				var res = await client.GetAsync(address);
				var resjson = res.Content.ReadAsStringAsync().Result;

				return resjson;

			}
			catch (System.Exception ex) {
				Console.WriteLine(ex.Message);
				throw;
				
			}
		}
	}
}
