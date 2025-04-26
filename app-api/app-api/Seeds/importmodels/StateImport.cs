
using Newtonsoft.Json;

public class BaseImport
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

}
public class StateImport : BaseImport
{

    [JsonProperty("country_id")]
    public string CountryImportId { get; set; } = string.Empty;

}
