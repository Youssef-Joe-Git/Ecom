using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Shared
{
    public class ProductPrams
    {
       public string sort { get; set; } = "";
       public int? categoryId { get; set; }
       public string search { get; set; } = "";
       public int pageSize { get; set; } = 10;
       public int pageNumber { get; set; } = 1;
       




    }
}
