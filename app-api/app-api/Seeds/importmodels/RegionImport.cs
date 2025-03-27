
using Newtonsoft.Json;

public class RegionImport {
    [JsonProperty("name")]
    public string Name {get;set;}= string.Empty;

    [JsonProperty("id")]
    public string RegionId {get;set;} = string.Empty;
}
