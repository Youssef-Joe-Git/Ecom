using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Entities.Product
{
    public class Image : BaseEntity<int>
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}
