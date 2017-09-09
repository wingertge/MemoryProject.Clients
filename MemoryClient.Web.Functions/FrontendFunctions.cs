using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MemoryCore.JsonModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace MemoryClient.Web.Functions
{
    public static class FrontendFunctions
    {
        [FunctionName("Login")]
        public static async Task<HttpResponseMessage> Login([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "login")] HttpRequestMessage req, TraceWriter log)
        {
            if (!string.IsNullOrEmpty(req.GetCookie("__auth_token")))
            {
                var r1 = req.CreateResponse(HttpStatusCode.Redirect);
                r1.Headers.Location = new Uri(req.RequestUri.GetLeftPart(UriPartial.Authority));
                return r1;
            }

            if (req.Method == HttpMethod.Post)
            {
                var result = await new HttpClient().PostAsync($"{Config.ApiHost}/auth/login", await FormDataToJson(req.Content));

                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = req.CreateResponse(result.StatusCode);
                    errorResponse.Content = result.Content;
                    return errorResponse;
                }

                var response = req.CreateResponse(HttpStatusCode.Redirect);
                response.Headers.Location = new Uri(req.RequestUri.GetLeftPart(UriPartial.Authority));
                response.Headers.AddCookies(new List<CookieHeaderValue> { new CookieHeaderValue("__auth_token", await result.Content.ReadAsStringAsync()) });
                return response;
            }

            var html = await new HttpClient().GetStringAsync($"{Config.CdnHost}/html/login.html");
            var responseMessage = req.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(html, Encoding.UTF8, "text/html");
            return responseMessage;
        }

        [FunctionName("Register")]
        public static async Task<HttpResponseMessage> Register([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "register")] HttpRequestMessage req, TraceWriter log)
        {
            if (!string.IsNullOrEmpty(req.GetCookie("__auth_token")))
            {
                var r1 = req.CreateResponse(HttpStatusCode.Redirect);
                r1.Headers.Location = new Uri(req.RequestUri.GetLeftPart(UriPartial.Authority));
                return r1;
            }

            if (req.Method == HttpMethod.Post)
            {
                var result = await new HttpClient().PostAsync($"{Config.ApiHost}/users/register", await FormDataToJson(req.Content));
                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = req.CreateResponse(result.StatusCode);
                    errorResponse.Content = result.Content;
                    return errorResponse;
                }

                var response = req.CreateResponse(HttpStatusCode.Redirect);
                response.Headers.Location = new Uri(req.RequestUri.GetLeftPart(UriPartial.Authority));
                response.Headers.AddCookies(new List<CookieHeaderValue> { new CookieHeaderValue("__auth_token", await result.Content.ReadAsStringAsync()) });
                return response;
            }

            var html = await new HttpClient().GetStringAsync($"{Config.CdnHost}/html/register.html");
            var responseMessage = req.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(html, Encoding.UTF8, "text/html");
            return responseMessage;
        }

        [FunctionName("Home")]
        public static async Task<HttpResponseMessage> Home([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "dashboard")] HttpRequestMessage req, TraceWriter log)
        {
            if (string.IsNullOrEmpty(req.GetCookie("__auth_token")))
            {
                var r1 = req.CreateResponse(HttpStatusCode.Moved);
                r1.Headers.Location = new Uri(req.RequestUri.GetLeftPart(UriPartial.Authority) + "/login");
                return r1;
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-AuthToken", req.GetCookie("__auth_token"));
            var authResponse = await client.PostAsJsonAsync(Config.ApiHost + "/auth/validate", new { });

            if (!authResponse.IsSuccessStatusCode)
            {
                var r2 = req.CreateResponse(HttpStatusCode.Moved);
                r2.Headers.Location = new Uri(req.RequestUri.GetLeftPart(UriPartial.Authority) + "/login");
                return r2;
            }

            var html = await new HttpClient().GetStringAsync($"{Config.CdnHost}/html/home.html");
            var responseMessage = req.CreateResponse(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(html, Encoding.UTF8, "text/html");
            return responseMessage;
        }

        private static async Task<StringContent> FormDataToJson(HttpContent content)
        {
            var formData = await content.ReadAsFormDataAsync();
            var dict = formData.AllKeys.AsParallel().ToDictionary(a => a, a => formData[a]);
            return new StringContent(JsonConvert.SerializeObject(dict), Encoding.UTF8, "application/json");
        }
    }
}
