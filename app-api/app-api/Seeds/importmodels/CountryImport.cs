using Newtonsoft.Json;

public class CountryImport {
    [JsonProperty("name")]
    public string Name {get;set;}= string.Empty;

    [JsonProperty("code")]
    public string Code {get;set;} = string.Empty;

}