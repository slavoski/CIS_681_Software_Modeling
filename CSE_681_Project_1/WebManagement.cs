using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace CSE_681_Project_1
{
	public class WebManagement
	{
		#region member variables

		private static readonly Lazy<WebManagement> _instance = new Lazy<WebManagement>(() => new WebManagement());

		#endregion member variables

		#region properties

		public static WebManagement Instance
		{
			get => _instance.Value;
		}

		#endregion properties

		#region methods

		public Tuple<string, T> GetJsonFromURL<T>(string url)
		{
			var httpClient = new HttpClient() { BaseAddress = new Uri(url) };
			return new Tuple<string, T>(httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result, httpClient.GetFromJsonAsync<T>(httpClient.BaseAddress).Result);
		}

		#endregion methods
	}
}