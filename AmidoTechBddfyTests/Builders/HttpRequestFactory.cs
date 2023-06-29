using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using AmidoTechBddfyTests.Models;
//using LBS.Identity.API.FunctionalTests.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using AmidoTechBddfyTests.Configuration;

namespace AmidoTechBddfyTests.Builders
{
    public class HttpRequestFactory
    {
        private static TokenResponse _token;

        public static async Task<HttpResponseMessage> Get(string baseUrl,
            string path,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null)
        {
            var authToken = await GetToken();

            return await new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(baseUrl, path)
                                .AddBearerToken(authToken.Token)
                                .AddCustomHeaders(headers)
                                .AddParameters(parameters)
                                .SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(
            string baseUrl,
            string path,
            object body,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null)
        {
            var authToken = await GetToken();

            return await new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(baseUrl, path)
                                .AddBearerToken(authToken.Token)
                                .AddContent(CreateHttpContent(body))
                                .AddCustomHeaders(headers)
                                .AddParameters(parameters)
                                .SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(
            string baseUrl,
            string path,
            object body,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null)
        {
            var authToken = await GetToken();

            return await new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(baseUrl, path)
                                .AddBearerToken(authToken.Token)
                                .AddContent(CreateHttpContent(body))
                                .AddCustomHeaders(headers)
                                .AddParameters(parameters)
                                .SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(
            string baseUrl,
            string path,
            object body,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null)
        {
            var authToken = await GetToken();

            return await new HttpRequestBuilder()
                .AddMethod(HttpMethod.Patch)
                .AddRequestUri(baseUrl, path)
                .AddBearerToken(authToken.Token)
                .AddContent(CreateHttpContent(body))
                .AddCustomHeaders(headers)
                .AddParameters(parameters)
                .SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(
            string baseUrl,
            string path,
            Dictionary<string, string> headers = null)
        {
            var authToken = await GetToken();

            return await new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(baseUrl, path)
                                .AddBearerToken(authToken.Token)
                                .AddCustomHeaders(headers)
                                .SendAsync();
        }

        //Creates HttpContent from the object provided
        private static HttpContent CreateHttpContent<TContent>(TContent content)
        {
            //If the content is empty then create empty HttpContent
            if (EqualityComparer<TContent>.Default.Equals(content, default(TContent)))
            {
                return new ByteArrayContent(new byte[0]);
            }
            //if the content is not empty, then create HttpContent with the Accept header set to 'application/json'
            else
            {
                var json = JsonConvert.SerializeObject(content);
                var result = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
                result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
        }

        private static async Task<TokenResponse> GetToken()
        {
            var config = ConfigAccessor.GetApplicationConfiguration();

           // if (config.IsLocal) { _token = new Token(); }
           // if (_token != null) return _token;

            var responseMessage = await new HttpRequestBuilder()
                    .AddMethod(HttpMethod.Get)
                    .AddRequestUri("https://amido-tech-test.herokuapp.com","/token")
                    .SendAsync();

            _token = JsonConvert.DeserializeObject<TokenResponse>(await responseMessage.Content.ReadAsStringAsync());

            return _token;
        }


        //Creates HttpContent from the object provided
        private static HttpContent CreateForm(Dictionary<string, string> values)
        {
            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var item in values)
            {
                multipartFormDataContent.Add(new StringContent(item.Value), item.Key);
            }

            return multipartFormDataContent;
        }
    }
}
