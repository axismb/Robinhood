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

    public class Dividend
    {
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("instrument")]
        public string Instrument { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
        [JsonProperty("withholding")]
        public decimal Withholding { get; set; }
        [JsonProperty("record_date")]
        public DateTime RecordDate { get; set; }
        [JsonProperty("payable_date")]
        public DateTime PayableDate { get; set; }
        [JsonProperty("paid_at")]
        public DateTime PaidAt { get; set; }
    }

    public class Quote
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("ask_price")]
        public decimal AskPrice { get; set; }
        [JsonProperty("ask_size")]
        public uint AskSize { get; set; }
        [JsonProperty("bid_price")]
        public decimal BidPrice { get; set; }
        [JsonProperty("bid_size")]
        public uint BidSize { get; set; }
        [JsonProperty("last_trade_price")]
        public decimal LastTradePrice { get; set; }
        [JsonProperty("previous_close")]
        public decimal PreviousClosePrice { get; set; }
        [JsonProperty("adjusted_previous_close")]
        public decimal AdjustedClosePrice { get; set; }
        [JsonProperty("previous_close_date")]
        public DateTime PreviousCloseDate { get; set; }
        [JsonProperty("trading_halted")]
        public bool Halted { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated_At { get; set; }

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

    class ObjectCollection<T>
    {
        [JsonProperty("results")]
        public List<T> Results { get; set; }
        [JsonProperty("detail")]
        public string Error { get; set; }
    }
}
