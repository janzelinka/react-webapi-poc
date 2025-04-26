using Newtonsoft.Json;

public class CountryImport : BaseImport
{

    [JsonProperty("iso3")]
    public string Code { get; set; } = string.Empty;


}