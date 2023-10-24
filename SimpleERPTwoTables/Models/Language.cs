namespace SimpleERPTwoTables.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public List<CityLanguage> CityLanguages { get; set; } = new();

    }
}
