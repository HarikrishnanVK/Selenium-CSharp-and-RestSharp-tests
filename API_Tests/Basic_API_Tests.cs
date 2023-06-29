using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumWithSpecflow
{
    [TestFixture]
    [Parallelizable]
    class Basic_API_Tests
    {
        [TestCase("1", TestName = "Get Cat"), Category("Get_Pet")]
        [TestCase("2", TestName = "Get Parrot"), Category("Get_Pet")]
        [TestCase("3", TestName = "Get Rabbit"), Category("Get_Pet")]
        [TestCase("4", TestName = "Get Golden Fish"), Category("Get_Pet")]
        public void VerifyStatusCode(string id)
        {
            RestClient client = new RestClient("https://petstore.swagger.io/v2");
            RestRequest request = new RestRequest($"/pet/{id}", Method.Get);
            RestResponse restResponse = client.Execute(request);
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(restResponse.ResponseStatus, ResponseStatus.Completed);
            Assert.IsTrue(restResponse.IsSuccessful);
            Console.WriteLine(restResponse.Content);
        }
        
        [Test]
        public void CreateNewPetUsingHardCodedData()
        {
            RestClientOptions options = new RestClientOptions("https://petstore.swagger.io/v2")
            {
                ThrowOnDeserializationError = true,
                FailOnDeserializationError = true,
                MaxTimeout = 30000
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest("/pet", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;
            var body = new
            {
                id = "2",
                name = "pussy_cat2",
                status = "rarely available",
            };
            // request.AddBody(body.ToString(), contentType: "Application/JSON");
            request.AddParameter("Application/JSON", body.ToString(), ParameterType.RequestBody);
            RestResponse restResponse = client.ExecutePost(request);
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(restResponse.Content.ToString());
        }

        [TestCase("Cat", TestName = "Add Cat as new pet"), Category("Create_Pet")]
        [TestCase("Parrot", TestName = "Add Parrot as new pet"), Category("Create_Pet")]
        [TestCase("Rabbit", TestName = "Add Rabbit as new pet"), Category("Create_Pet")]
        [TestCase("Golden Fish", TestName = "Add Golden Fish as new pet"), Category("Create_Pet")]
        public async Task CreateNewPetUsingDataFromJSONFile(string petName)
        {
            /* Initiate rest client */
            RestClient client = new RestClient("https://petstore.swagger.io/v2");
            RestRequest request = new RestRequest("/pet", Method.Post);

            /* Add headers */
            request.AddHeader("Accept", "application/json");

            /* Fetch the json file to post request on new pet creation */
            string dir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            JObject data = JObject.Parse(File.ReadAllText(dir + "../../../pet_data_post.json"));

            /* Desearialize the Json to string and store it in dynamics array */
            dynamic array = JsonConvert.DeserializeObject(data.ToString());

            /* get the details of cat using Json Key by casting it to JSON Object */
            var pet = (JObject)array[petName];

            /* convert cat to string and add it as parameter to request */
            request.AddParameter("Application/JSON", pet.ToString(), ParameterType.RequestBody);

            RestResponse restResponse = await client.PostAsync(request);
            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(restResponse.Content.ToString());
            string content = restResponse.Content;
            string petContent = pet.ToString();
            string content2 = Regex.Replace(content, @"\s", "");
            string content3 = Regex.Replace(petContent, @"\s", "");
            Assert.AreEqual(content2, content3);

            // deserialize json string response to JsonNode object
            var responseData = JsonConvert.DeserializeObject<Root>(restResponse.Content);
            var responseData2 = JsonConvert.DeserializeObject<Category>(restResponse.Content);
            var responseData3 = JsonConvert.DeserializeObject<Tag>(restResponse.Content);
            Assert.AreEqual(responseData.id, 1);

            // responseData.id
        }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public Category category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<Tag> tags { get; set; }
        public string status { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
