using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Data.DTO.Response
{
    public class CartResponse
    {
        public string ProductName { get; set; }

        public int  ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public decimal SubTotal => (Price - (Price * Discount / 100)) * Count;
    }
}
