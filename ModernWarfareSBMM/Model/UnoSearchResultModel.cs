using Newtonsoft.Json;
using System.Collections.Generic;

namespace ModernWarfareSBMM.Model
{
    internal partial class UnoSearchResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<UnoUserModel> Data { get; set; }
    }
    internal partial class UnoUserModel
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("avatar")]
        public object Avatar { get; set; }
    }
}