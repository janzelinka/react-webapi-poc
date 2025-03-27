using Newtonsoft.Json;

public class DistrictImport {

    [JsonProperty("name")]
    public string Name {get;set;} = string.Empty;

    [JsonProperty("region_id")]
    public string RegionId {get;set;} = string.Empty;

    [JsonProperty("id")]
    public string DistrictId {get;set;} = string.Empty;

}