using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Data.DTO.Request
{
    public class BrandRequest
    {
        //public int Id { get; set; }
        public IFormFile logo { get; set; }
        public List<BrandTranslationRequest> Translations { get; set; }

    }
}
