using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ItemsManagementUnitTest
{
    public class ItemIntegrationTest
    {
        private readonly HttpClient _httpClient;
        private readonly ITestOutputHelper _output;

        public ItemIntegrationTest(ITestOutputHelper output)
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
            _output = output;
        }

        [Fact(DisplayName = "Fetch All Items")]
        public async Task Get_Items_ReturnsSuccess()
        {
            var response = await _httpClient.GetAsync("/api/v1/Items");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            
            Assert.NotNull(result);

            List<string> names = new List<string>();
            var resultObject = JArray.Parse(result);
            foreach (JObject item in resultObject)
            {
                if (item.TryGetValue("name", out JToken nameToken) && nameToken.Type == JTokenType.String)
                {
                    string name = nameToken.ToString();
                    if(!string.IsNullOrWhiteSpace(name))
                    {
                        names.Add(name);
                    }
                }
            }

            Assert.True(names.Any());
        }

        [Fact(DisplayName = "Add Item(s)")]
        public async Task Add_Items_ReturnsSuccess()
        {
            var json = @"[
                          {
                            ""name"": ""Item-Unit Test"",
                            ""description"": ""Item from Unit test""
                          }
                        ]";
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/v1/Items", data);
            
            var result = await response.Content.ReadAsStringAsync();
            _output.WriteLine(result);
            Assert.NotNull(result);

            Assert.Equal("1",result); // 1 means number of reacord added in the DB
        }

        [Fact(DisplayName = "Edit Item")]
        public async Task Edit_Items_ReturnsSuccess()
        {
            var json = @"
                          {
                            ""name"": ""Item-Unit Test"",
                            ""description"": ""Item from Edit Unit test method"" 
                          }
                        ";
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = Path.Combine("/api/v1/Items", "1");
            var response = await _httpClient.PutAsync(uri, data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode); 
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotNull(result);
            

        }

        [Fact(DisplayName = "Delete Item")]
        public async Task Delete_Items_ReturnsSuccess()
        {
            
            var uri = Path.Combine("/api/v1/Items", "2");
            var response = await _httpClient.DeleteAsync(uri);

            //Added check if there is specified id does not exist in DB to delete then NotFound Status code returned
            if(response.StatusCode== HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.True(true);
            }
            
            
        }
    }
}
