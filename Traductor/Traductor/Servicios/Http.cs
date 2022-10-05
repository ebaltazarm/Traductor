using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Traductor.Models;
using Xamarin.Forms;


namespace Traductor.Servicios
{
	public class Http
	{
		//GPI-19
		//  private static string url_base_prod = "http://192.168.0.22:3000/";
		private static string     url_base_prod = "http://192.168.156.30:3000/";
		private static string     url_base_test = "http://192.168.156.30:3000/";
		private static string     url_contenido = "http://192.168.156.30:3000";
		private static string     url_base      = UrlProduccion();
		private static string     UriBase       = url_base + "Login";
		private static HttpClient client        = new HttpClient();

		public Http()
		{
			client.Timeout = TimeSpan.FromSeconds(100);
		}

		public static string UrlProduccion()
		{
			try
			{
				return url_base_prod;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static string UrlContenido()
		{
			try
			{
				return url_contenido;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private static MultipartFormDataContent ConvertDataForm(string json, string index)
		{
			try
			{
				var validacion = GetHttpData(index).Replace("\"", "").Replace("{data:", "").Replace(",code:200}", "");

				var     multiFormConvert = new MultipartFormDataContent();
				dynamic dynamicObject    = JValue.Parse(json);

				if (json != "null")
				{
					foreach (JProperty property in dynamicObject)
					{
						try
						{
							multiFormConvert.Add(new StringContent(property.Value.ToString()), property.Name);
						}
						catch (Exception ex)
						{
						}
					}
				}

				multiFormConvert.Add(new StringContent(validacion), "__RequestVerificationToken");

				return multiFormConvert;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private static string ConvertDataQuerry(object objetJson)
		{
			string           Query       = "?";
			List<QueryModel> jsonReplace = objetJson as List<QueryModel>;

			var Error = "";

			foreach (var item in jsonReplace)
			{
				try
				{
					Query += $"{item.Param}={item.Value}&";
				}
				catch (Exception ex)
				{
					Error += "";
				}
			}


			return Query.Substring(0, Query.Length - 1);
		}

		public static async Task<String> PostForm(string _uri, dynamic json = null, object objetJsonQuery = null, string index = "App/Base/Anty")
		{
			try
			{
				client.DefaultRequestHeaders.Clear();

				var        multiForm = new MultipartFormDataContent();
				UriBuilder builder   = new UriBuilder(url_base + _uri);
				builder.Query = objetJsonQuery != null ? ConvertDataQuerry(objetJsonQuery) : ""; //"?desde=&hasta=&tipoAhorroSearch=&asociadoId=26";

				multiForm = ConvertDataForm(JsonConvert.SerializeObject(json), index);


				//    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
				CookieRequestVerificationToken();

				var response = await client.PostAsync(builder.Uri, multiForm);

				return response.Content.ReadAsStringAsync().Result;
			}
			catch (Exception ex)
			{
				throw new Exception("Ocurrio un error en la petición, verifique");
			}
		}

		public static void CookieRequestVerificationToken()
		{
			if (Application.Current.Resources.ContainsKey("__RequestVerificationToken"))
			{
				var Request = Application.Current.Resources["__RequestVerificationToken"] as CookieContainer;
				client.DefaultRequestHeaders.Add("Cookie", Request.GetCookieHeader(new Uri(UriBase)));
			}
		}

		public static void CookieAutorizacion()
		{
			if (Application.Current.Resources.ContainsKey("Autorizacion")) //&& client.DefaultRequestHeaders.ToString() == "")
			{
				var cookie = Application.Current.Resources["Autorizacion"] as CookieContainer;
				client.DefaultRequestHeaders.Add("Cookie",           cookie.GetCookieHeader(new Uri(UriBase)));
				client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			}
		}

		public static CookieContainer ObtenerCookies()
		{
			return ReadCookies(client.GetAsync(url_base + "Login").Result);
		}

		public static string GetHttpData(string _uri, object objetJsonQuery = null)
		{
			try
			{
				var builder = new UriBuilder(url_base + _uri);
				builder.Query = objetJsonQuery != null ? ConvertDataQuerry(objetJsonQuery) : ""; //"?desde=&hasta=&tipoAhorroSearch=&asociadoId=26";
				//client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

				var response = client.GetAsync(builder.Uri).Result;
				
				return response.Content.ReadAsStringAsync().Result;
			}
			catch (Exception ex)
			{
				throw new Exception("Ocurrio un error en la petición, intentar de nuevo");
			}
		}


		public static async Task<String> Post(string _uri, string json)
		{
			client.DefaultRequestHeaders.Clear();

			StringContent data = (json != null) ? new StringContent(json, Encoding.UTF8, "application/json") : null;

			client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			var response = await client.PostAsync(url_base + _uri, data);
			//Application.Current.Resources["Autorizacion"] = ReadCookies(response);

			return response.Content.ReadAsStringAsync().Result;
		}


		public static CookieContainer ReadCookies(HttpResponseMessage response)
		{
			var pageUri = response.RequestMessage.RequestUri;

			var                 cookieContainer = new CookieContainer();
			IEnumerable<string> cookies;
			if (response.Headers.TryGetValues("set-cookie", out cookies))
			{
				foreach (var c in cookies)
				{
					cookieContainer.SetCookies(pageUri, c);
				}
			}

			return cookieContainer;
		}

		public static async Task<string> Loguearse(string json, double timeout = 25)
		{
			client         = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(timeout);
			//httpClient.DefaultRequestHeaders.CacheControl
			var _req = ObtenerCookies();
			Application.Current.Resources["__RequestVerificationToken"] = _req;
			return await Post("Login/App", json);
		}
	}
}