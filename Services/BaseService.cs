//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjMngWasm.Services;
public class BaseService {

  protected readonly HttpClient _httpClient;
  protected readonly ISessionStorageService _sess;
  public BaseService(HttpClient httpClient , ISessionStorageService sess ) {
			_httpClient = httpClient;
		_sess = sess;
    }
		public async Task<IEnumerable<TResult>> GetMethodList<TResult>(string url) {
			try {
				HttpResponseMessage response = await _httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();
				if (response.StatusCode == System.Net.HttpStatusCode.OK) {
					using var responseStream = await response.Content.ReadAsStreamAsync();
					return await JsonSerializer.DeserializeAsync<IEnumerable<TResult>>(responseStream);
				}
				return null;
			}
			catch (HttpRequestException e) {
				return null;
			}
		}

		public async Task<TResult> GetMethod<TResult>(string url) {
			try {
				HttpResponseMessage response = await _httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();
				if (response.StatusCode == System.Net.HttpStatusCode.OK) {
					using var responseStream = await response.Content.ReadAsStreamAsync();
					return await JsonSerializer.DeserializeAsync<TResult>(responseStream);
				}

				return default(TResult);

			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				return default(TResult);
			}
		}

		public async Task PostMethodList<T>(string url, List<T> content) {
			await _httpClient.PostAsJsonAsync(url, content);
		}
		public async Task PostMethod<T>(string url, T content) {
			await _httpClient.PostAsJsonAsync(url, content);
		}
	public async Task PostMethodRes<T>(string url, T content) {

		// 토큰이 만료 되었으면 새로 가져 오는 로직을 넣자.
		string tk = await _sess.GetItemAsync<string>("ums_token");

		if (!string.IsNullOrEmpty(tk) && _httpClient.DefaultRequestHeaders.Authorization == null ) {
			_httpClient.DefaultRequestHeaders.Authorization =  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tk);
		}

		HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content, JsonSerializerOptions.Default);
		response.EnsureSuccessStatusCode();
		//JToken jtoken =null;

		if (response.StatusCode == System.Net.HttpStatusCode.OK) {
			var responseString = await response.Content.ReadAsStringAsync();//.ReadAsStreamAsync();

			Newtonsoft.Json.Linq.JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(responseString);


			//JToken jt1 = jobj.Descendants().Where(t => t.Type == JTokenType.Property && ((JProperty)t).Name == "dt0")
			//    .Select(p => ((JProperty)p).Value).FirstOrDefault();

			//JToken jt = jobj.Descendants().Where(x => x.Type == JTokenType.Property && ((JProperty)x).Name == "data")
			//		.Select(p => ((JProperty)p).Value).FirstOrDefault();

			//Newtonsoft.Json.Linq.JToken jtoken = jobj.SelectToken("$....dt0");
			//string token = jtoken.Value<string>();


			//var token =  jobj.SelectToken("$.data");
			//var token1 = jobj.SelectToken("$..data");
			//var token = jobj.SelectToken("$..data[0].TOKEN");

			//string tokenStr = token?.ToString();

			//if( !string.IsNullOrEmpty(tokenStr)) {
			//	_sess.SetItemAsync("ums_token", tokenStr);
			//}

			//string bbb = token.ToString();

			//string aaa = "";

			// dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(bbb);
		}

	}


  public async Task<string> PostMethodObj<T>(string url, T content) {

		string result = string.Empty;
    // 토큰이 만료 되었으면 새로 가져 오는 로직을 넣자.
    string tk = await _sess.GetItemAsync<string>("ums_token");

    if (!string.IsNullOrEmpty(tk) && _httpClient.DefaultRequestHeaders.Authorization == null) {
      _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tk);
    }

    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content, JsonSerializerOptions.Default);
    response.EnsureSuccessStatusCode();
    //JToken jtoken =null;

    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
      var responseString = await response.Content.ReadAsStringAsync();//.ReadAsStreamAsync();

      Newtonsoft.Json.Linq.JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(responseString);

      JToken jt = jobj.Descendants().Where(x => x.Type == JTokenType.Property && ((JProperty)x).Name == "data")
      .Select(p => ((JProperty)p).Value).FirstOrDefault();


			result = jt?.ToString();


    }
		return result;

  }


  public async Task<string> PostMethodStr<T>(string url, T content) {
		string tokenStr = string.Empty;
    // 토큰이 만료 되었으면 새로 가져 오는 로직을 넣자.
    string tk = await _sess.GetItemAsync<string>("ums_token");

    if (!string.IsNullOrEmpty(tk) && _httpClient.DefaultRequestHeaders.Authorization == null) {
      _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tk);
    }

    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content, JsonSerializerOptions.Default);
    response.EnsureSuccessStatusCode();
		Newtonsoft.Json.Linq.JObject jobj = null;
    if (response.StatusCode == System.Net.HttpStatusCode.OK) {
      var responseString = await response.Content.ReadAsStringAsync();//.ReadAsStreamAsync();

      jobj = Newtonsoft.Json.Linq.JObject.Parse(responseString);


      var token = jobj.SelectToken("$..data[0].TOKEN");

      tokenStr = token.ToString();



    }
		return tokenStr;
  }


  public async Task<TResult> PostMethodGetObject<TResult, TValue>(string url, TValue content) {
			try {
				HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content);
				response.EnsureSuccessStatusCode();
				if (response.StatusCode == System.Net.HttpStatusCode.OK) {
					using var responseStream = await response.Content.ReadAsStreamAsync();
					return await JsonSerializer.DeserializeAsync<TResult>(responseStream);
				}
				return default(TResult);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				return default(TResult);
			}
		}
	}