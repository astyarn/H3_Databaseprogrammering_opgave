namespace SimpleERPTwoTables.DTO
{
    public class LanguageForSaveDTO
    {
        public string LanguageName { get; set; }
    }

    public class LanguageForUpdateDTO : LanguageForSaveDTO
    {
        public int LanguageId { get; set; }
    }

    public class LanguageDTO : LanguageForUpdateDTO
    {
        public CityLanguageDTO CityLanguage { get; set; }
    }

    public class LanguageDTOMinusRelations : LanguageForUpdateDTO
    {

    }
}
