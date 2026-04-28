using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Models
{
  
        public class Brand : AuditableEntity
        {
            public int Id { get; set; }
            public string logo { get; set; }

            // Navigation Property
            public List<Product> Products { get; set; }
            public List<BrandTranslation> Translations { get; set; }

        }

    }


