using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Data.DTO.Response
{
    public class CategoryResponse
    {
        public int cat_id { get; set; }
        public string userCreated { get; set; }
        //public List<CategoryTranslationResponse> Translations { get; set; }

        public string Name { get; set; }

    }
}
