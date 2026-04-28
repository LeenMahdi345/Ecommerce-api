using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Data.DTO.Response
{
    public class RegisterResponse
    {
        public string Message { get; set; }
        public bool Succes { get; set; }
        public List<string> Error { get; set; }

    }
}
