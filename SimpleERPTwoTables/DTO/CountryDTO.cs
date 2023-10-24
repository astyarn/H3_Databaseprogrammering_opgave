namespace SimpleERPTwoTables.DTO
{
    public class CountryForSaveDTO
    {
        public string CountryName { get; set; }
    }

    public class CountryForUpdateDTO : CountryForSaveDTO
    {
        public int CountryId { get; set; }
    }

    public class CountryDTO : CountryForUpdateDTO
    {
        public ICollection<CityDTOMinusRelations> Cities { get; set; }
    }

    public class CountryDTOMinusRelations : CountryForUpdateDTO
    {

    }
}
