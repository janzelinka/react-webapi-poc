using Newtonsoft.Json;

public class CityImport {

        [JsonProperty("fullname")]
        public string Name {get;set;} = string.Empty;

        [JsonProperty("district_id")]
        public string DistrictId {get;set;} = string.Empty;

        [JsonProperty("zip")]
        public string PostalCode {get;set;} = string.Empty;

    }