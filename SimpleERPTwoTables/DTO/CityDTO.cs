namespace SimpleERPTwoTables.DTO
{
    public class CityForSaveDTO
    {
        public string CityName { get; set; }

        public string CityDescription { get; set; }

        public int CountryId { get; set; }
    }

    public class CityForUpdateDTO : CityForSaveDTO
    {
        public int CityId { get; set; }
    }

    public class CityDTO : CityForUpdateDTO
    {
        public CountryDTOMinusRelations? Country { get; set; }
    }

    public class CityDTOMinusRelations : CityForUpdateDTO
    {

    }
}
