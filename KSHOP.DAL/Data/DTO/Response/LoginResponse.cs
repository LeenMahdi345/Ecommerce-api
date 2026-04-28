using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Data.DTO.Response
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public bool Succes { get; set; }
        public string AccessToken { get; set; }
    }
}
