using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjMngWasm.Services;
public class BaseService {

private readonly HttpClient _httpClient;
		public BaseService(HttpClient httpClient) {
			_httpClient = httpClient;
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
		public async Task PostMethodRes<T>(string url, T content, string token) {

// var hmsg = new HttpRequestMessage();
// hmsg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
// hmsg.Content = new HttpContent();

//HttpResponseMessage response = 	await _httpClient.SendAsync(hmsg);

_httpClient.DefaultRequestHeaders.Authorization =  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

		HttpResponseMessage response = 	await _httpClient.PostAsJsonAsync(url, content, JsonSerializerOptions.Default );
		response.EnsureSuccessStatusCode();
				if (response.StatusCode == System.Net.HttpStatusCode.OK) {
					using var responseStream = await response.Content.ReadAsStreamAsync();

					string aaaa = "";
				}

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