using Newtonsoft.Json;

public class CityImport {

        [JsonProperty("name")]
        public string Name {get;set;} = string.Empty;

        [JsonProperty("state_id")]
        public string StateImportId {get;set;} = string.Empty;

        [JsonProperty("zip")]
        public string PostalCode {get;set;} = string.Empty;

    }