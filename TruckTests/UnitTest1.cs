using System;
using Xunit;
using TruckWebApi.DataAccess;
using TruckWebApi.DTO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using FluentAssertions;

namespace TruckTests
{
    public class UnitTest1: IDisposable
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public UnitTest1()
        {
            SetUpClient();
        }

        public void Dispose()
        {

        }

        public async Task SeedData()
        {
            
            // Create entry with id 1 
            var createForm0 = GenerateCreateForm("Truck1", "FM", 2020, 2021);
            var response0 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));

            // Create entry with id 2 
            var createForm1 = GenerateCreateForm("Truck2", "FH", 2010, 2011);
            var response1 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));

            // Create entry with id 3 
            var createForm2 = GenerateCreateForm("Truck3", "FM", 2000, 2001);
            var response2 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm2), Encoding.UTF8, "application/json"));

            // Create entry with id 4 
            var createForm3 = GenerateCreateForm("Truck4", "FH", 1990, 1991);
            var response3 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm3), Encoding.UTF8, "application/json"));

            // Create entry with id 5
            var createForm4 = GenerateCreateForm("Truck5", "FM", 1980, 1981);
            var response4 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm4), Encoding.UTF8, "application/json"));

        }

        //// TEST NAME - CreateTruck
        //// TEST DESCRIPTION - A new truck should be created
        [Fact]
        public async Task TestCase0()
        {
            await SeedData();

            // Create entry with id 6
            var createForm0 = GenerateCreateForm("Truck6", "FH", 2019, 2020);
            var response0 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm0), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().BeEquivalentTo(201);

            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("{\"id\": 6,\"model\": \"FH\",\"modelYear\": 2019,\"manufacYear\": 2020,\"nickName\": \"Truck6\"}");
            realData0.Should().BeEquivalentTo(expectedData0);

            // Create entry with id 7
            var createForm1 = GenerateCreateForm("Truck7", "FM", 2010, 2011);
            var response1 = await Client.PostAsync("/api/truck", new StringContent(JsonConvert.SerializeObject(createForm1), Encoding.UTF8, "application/json"));
            response1.StatusCode.Should().BeEquivalentTo(201);


            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            var expectedData1 = JsonConvert.DeserializeObject("{\"id\": 7,\"model\": \"FM\",\"modelYear\": 2010,\"manufacYear\": 2011,\"nickName\": \"Truck7\"}");
            realData1.Should().BeEquivalentTo(expectedData1);
        }

        // TEST NAME - GetTruck
        // TEST DESCRIPTION - It finds all the trucks in a truck
        [Fact]
        public async Task TestCase1()
        {
            await SeedData();
            
            // Get All trucks 
            var response0 = await Client.GetAsync("/api/truck");
            response0.StatusCode.Should().BeEquivalentTo(200);

            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\": 1,\"model\": \"FM\",\"modelYear\": 2020,\"manufacYear\": 2021,\"nickName\": \"Truck1\"},{\"id\": 2,\"model\": \"FH\",\"modelYear\": 2010,\"manufacYear\": 2011,\"nickName\": \"Truck2\"},{\"id\": 3,\"model\": \"FM\",\"modelYear\": 2000,\"manufacYear\": 2001,\"nickName\": \"Truck3\"},{\"id\": 4,\"model\": \"FH\",\"modelYear\": 1990,\"manufacYear\": 1991,\"nickName\": \"Truck4\"},{\"id\": 5,\"model\": \"FM\",\"modelYear\": 1980,\"manufacYear\": 1981,\"nickName\": \"Truck5\"}]");
            realData0.Should().BeEquivalentTo(expectedData0);

        }

        //// TEST NAME - getSingleEntryById
        //// TEST DESCRIPTION - It finds single truck by ID
        [Fact]
        public async Task TestCase2()
        {
            await SeedData();

            // Get Single truck By ID 
            var response0 = await Client.GetAsync("api/truck/query?id=5");
            response0.StatusCode.Should().BeEquivalentTo(200);

            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\": 5,\"model\": \"FM\",\"modelYear\": 1980,\"manufacYear\": 1981,\"nickName\": \"Truck5\"}]");
            realData0.Should().Equals(expectedData0);

            // Get Single truck By ID which does not exist should return empty array
            var response1 = await Client.GetAsync("api/truck/query?id=9");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            realData1.Should().Equals("[]");
        }

        //// TEST NAME - getTrucksByNickName
        //// TEST DESCRIPTION - It finds trucks by NickName
        [Fact]
        public async Task TestCase3()
        {
            await SeedData();

            // Get Single truck By ID 
            var response0 = await Client.GetAsync("api/truck/query?NickName=Truck5");
            response0.StatusCode.Should().BeEquivalentTo(200);

            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\": 5,\"model\": \"FM\",\"modelYear\": 1980,\"manufacYear\": 1981,\"nickName\": \"Truck5\"}]");
            realData0.Should().Equals(expectedData0);

            // Get Single truck By ID which does not exist should return empty array
            var response1 = await Client.GetAsync("api/truck/query?NickName=abc");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            realData1.Should().Equals("[]");
        }

        //// TEST NAME - getTrucksByModelYear
        //// TEST DESCRIPTION - It finds trucks by modelyear
        [Fact]
        public async Task TestCase4()
        {
            await SeedData();

            // Get Single truck By ID 
            var response0 = await Client.GetAsync("api/truck/query?modelYear=1981");
            response0.StatusCode.Should().BeEquivalentTo(200);

            var realData0 = JsonConvert.DeserializeObject(response0.Content.ReadAsStringAsync().Result);
            var expectedData0 = JsonConvert.DeserializeObject("[{\"id\": 5,\"model\": \"FM\",\"modelYear\": 1980,\"manufacYear\": 1981,\"nickName\": \"Truck5\"}]");
            realData0.Should().Equals(expectedData0);

            // Get Single truck By ID which does not exist should return empty array
            var response1 = await Client.GetAsync("api/truck/query?modelYear=1910");
            response1.StatusCode.Should().BeEquivalentTo(200);
            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            realData1.Should().Equals("[]");
        }

        //// TEST NAME - checkNonExistentApi
        //// TEST DESCRIPTION - It should check if an API exists
        [Fact]
        public async Task TestCase5()
        {
            await SeedData();

            // Return with 404 if no API path exists 
            var response0 = await Client.GetAsync("/api/truck/id/123");
            response0.StatusCode.Should().BeEquivalentTo(404);

            // Return with 405 if API path exists but called with different method
            var response1 = await Client.GetAsync("/api/truck/123");
            response1.StatusCode.Should().BeEquivalentTo(405);
        }

        // TEST NAME - updateTruck
        // TEST DESCRIPTION - Update trucks details
        [Fact]
        public async Task TestCase6()
        {
            await SeedData();

            // Return with 204 if truck is updated 
            var body0 = JsonConvert.DeserializeObject("{\"Model\":\"FH\",\"ManufacYear\":2005,\"ModelYear\":2005,\"NickName\":\"ChangedTruck\"}");
            var response0 = await Client.PutAsync("/api/truck/3", new StringContent(JsonConvert.SerializeObject(body0), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().Be(204);

            //Check if the truck is updated
            var response1 = await Client.GetAsync("api/truck/query?id=3");
            response1.StatusCode.Should().BeEquivalentTo(200);

            var realData1 = JsonConvert.DeserializeObject(response1.Content.ReadAsStringAsync().Result);
            var expectedData1 = JsonConvert.DeserializeObject("{\"id\": 3,\"model\": \"FH\",\"modelYear\": 2005,\"manufacYear\": 2005,\"nickName\": \"ChangedTruck\"}");
            realData1.Should().Equals(expectedData1);
        }

        // TEST NAME - deleteTruck
        // TEST DESCRIPTION - Delete an truck by id
        [Fact]
        public async Task TestCase7()
        {
            await SeedData();

            // Return with 204 if truck is deleted
            var response0 = await Client.DeleteAsync("/api/truck/3");
            response0.StatusCode.Should().Be(204);

            // Check if the truck does not exist
            var response2 = await Client.GetAsync("api/truck/query?id=3");
            response2.StatusCode.Should().BeEquivalentTo(200);
            var realData2 = JsonConvert.DeserializeObject(response2.Content.ReadAsStringAsync().Result);
            realData2.Should().Equals("[]");
        }

        private CreateForm GenerateCreateForm(string nickName, string model, int manufacYear, int modelYear)
        {
            return new CreateForm()
            {
                Model = model,
                NickName = nickName,
                ManufacYear = manufacYear,
                ModelYear = modelYear
            };
        }

        private void SetUpClient()
        {

            var builder = new WebHostBuilder()
                .UseStartup<TruckWebApi.Startup>()
                .ConfigureServices(services =>
                {
                    var context = new TruckContext(new DbContextOptionsBuilder<TruckContext>()
                        .UseSqlite("DataSource=:memory:")
                        .EnableSensitiveDataLogging()
                        .Options);

                    services.RemoveAll(typeof(TruckContext));
                    services.AddSingleton(context);

                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();

                    // Clear local context cache
                    foreach (var entity in context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                });

            _server = new TestServer(builder);
            
            Client = _server.CreateClient();
        }
    }
}
