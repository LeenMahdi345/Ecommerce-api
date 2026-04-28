using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Data.DTO.Request
{
    public class CategoryRequest
    {
        //public int Id { get; set; }
        public List<CategoryTranslationRequest> Translations { get; set; }
    }
}
