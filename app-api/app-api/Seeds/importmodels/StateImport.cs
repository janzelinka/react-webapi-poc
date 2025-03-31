
using Newtonsoft.Json;

public class StateImport {
    [JsonProperty("name")]
    public string Name {get;set;}= string.Empty;

    [JsonProperty("country_id")]
    public string CountryId {get;set;} = string.Empty;

    [JsonProperty("id")]
    public string StateImportId {get;set;} = string.Empty;
}
