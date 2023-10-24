using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SimpleERPTwoTables.Models
{
    public class CityLanguage
    {
        public int LanguageId { get; set; }
        public int CityId { get; set; }

        public City City { get; set; } = null!;
        public Language Language { get; set; } = null!;

    }
}
