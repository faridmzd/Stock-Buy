using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Buy.API.Domain
{
    public abstract class Product : Entity
    {
        [NotMapped]
        public int QuantityNeeded { get; set; }
    }
}
