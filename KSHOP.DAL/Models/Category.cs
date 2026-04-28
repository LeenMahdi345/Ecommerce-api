using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Models
{
    public class Category: AuditableEntity
    {
        public int Id { get; set; }
        public ICollection<CategoryTranslation> Translations { get; set; }

      

    }
}
