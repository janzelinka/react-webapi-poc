using Newtonsoft.Json;

public class CountryImport {
    [JsonProperty("name")]
    public string Name {get;set;}= string.Empty;

    [JsonProperty("iso3")]
    public string Code {get;set;} = string.Empty;

    [JsonProperty("id")]
    public string ImportCountryId {get;set;} = string.Empty;

}