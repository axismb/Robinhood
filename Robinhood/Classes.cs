using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Robinhood
{
    class Authentication
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("non_field_errors")]
        public string[] Errors { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("detail")]
        public string Error { get; set; }
    }

    public class Instrument
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("bloomberg_unique")]
        public string BloombergUuid { get; set; }
        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }
    }

    class InstrumentCollection
    {
        [JsonProperty("results")]
        public List<Instrument> Instruments { get; set; }
    }
}
