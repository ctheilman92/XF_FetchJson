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
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Threading;
using System.Diagnostics.Contracts;

namespace XF_FetchJson.Pages
{
	public class fetchPage : ContentPage
	{

		#region variables
		private static ListView fetchlv;
		private static Button fetchBtn;
		private static Entry getID;
		private static List<User> ulist;
		private static DataTemplate dt;
		private static int userid;
		private static bool doesExist;
		private static string fetchurl = string.Format("http://jsonplaceholder.typicode.com/users/");

		#endregion


		public fetchPage ()
		{
			double topPadding = Device.OnPlatform<double>(20, 0, 0);
			ulist = new List<User>();
			getID = new Entry() {
				Placeholder = "User id #",
				Keyboard = Keyboard.Numeric,
				TextColor = Color.Green
			};
			fetchBtn = new Button() { Text = "Fetch Json" };


				
			fetchlv = new ListView() {
				RowHeight = 40,
			};

			var stack = new StackLayout() {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, 25, 0, 0),
				Children = { getID, fetchBtn, fetchlv },
			};
			this.Padding = new Thickness(0, topPadding, 0, 0);
			this.Content = stack;

			getID.TextChanged += (sender, e) => {
				userid = Int16.Parse(e.NewTextValue);
			};

			//fetchBtn.Clicked += async delegate { await getData(userid); };

			fetchBtn.Clicked += async (sender, e) => { await getData(userid); };

		}


		public async Task getData(int id) {

			foreach (var i in ulist) {
				Console.WriteLine(i.id);
				doesExist = (i.id == id) ? true : false;
				Console.WriteLine(doesExist);
			}

			if (!doesExist) {
				HttpClient client = new HttpClient();

				try {

					client.DefaultRequestHeaders.Add("Accept", "application/json");
					var res = await client.GetAsync(fetchurl + id.ToString());

					res.EnsureSuccessStatusCode();
					var resjson = res.Content.ReadAsStringAsync().Result;
					User user = JsonConvert.DeserializeObject<User>(resjson);

					//check if user exists

					ulist.Add(user);

					dt = new DataTemplate(typeof(TextCell));
					dt.SetBinding(TextCell.TextProperty, "email");
					fetchlv.ItemsSource = ulist;
					fetchlv.ItemTemplate = dt;
					getID.Text = null;


				} catch (Exception ex) {
					Console.WriteLine(ex.Message);
				}
			} 
			else {
				await DisplayAlert(" (>.<) User Exists (>.<)", "Sorry, but it seems this user cannot be added more than once...", "OK");
			}
		}
	}
}
