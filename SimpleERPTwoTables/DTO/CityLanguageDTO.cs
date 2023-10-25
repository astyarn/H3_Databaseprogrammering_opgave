namespace SimpleERPTwoTables.DTO
{
    public class CityLanguageForSaveAndUpdateDTO
    {
        public int CityId { get; set; }

        public int LanguageId { get; set; }
    }

    public class CityLanguageDTO : CityLanguageForSaveAndUpdateDTO
    {
        public CityDTOMinusRelations City { get; set; }

        public LanguageDTOMinusRelations Language { get; set; }
    }

    public class CityLanguageMinusCityDTO : CityLanguageForSaveAndUpdateDTO
    {
        public LanguageDTOMinusRelations Language { get; set; }
    }

    public class CityLanguageDTOMinusLanguage : CityLanguageForSaveAndUpdateDTO
    {
        public CityDTOMinusRelations City { get; set; }
    }
}
