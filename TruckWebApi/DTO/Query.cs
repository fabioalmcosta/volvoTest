using System;
namespace TruckWebApi.DTO
{
    public class Query
    {
        public int id { get; set; }
        public string eventOrganizer { get; set; }
        public string location { get; set; }
        public string name { get; set; }
    }
}
