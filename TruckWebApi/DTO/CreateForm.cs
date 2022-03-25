using System;
using Newtonsoft.Json;
namespace TruckWebApi.DTO
{
    public class CreateForm
    {
        [JsonProperty("model")]
        [JsonRequired]
        public string Model { get; set; }

        [JsonProperty("manufacYear")]
        [JsonRequired]
        public int ManufacYear { get; set; }

        [JsonProperty("ModelYear")]
        [JsonRequired]
        public int ModelYear { get; set; }

        [JsonProperty("nickName")]
        public string NickName { get; set; }
    }
}
