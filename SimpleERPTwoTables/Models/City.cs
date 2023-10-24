namespace SimpleERPTwoTables.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<CityLanguage> CityLanguages { get; set; } = new();

    }
}
