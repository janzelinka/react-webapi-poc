using app.Models;

namespace api.Models.Events
{
    public class Address : BaseEntity
    {
        public string CityId
        {
            get; set;
        } = string.Empty;

        public string CountryId
        {
            get; set;
        } = string.Empty;

        public string StateId
        {
            get; set;
        } = string.Empty;
    }
}