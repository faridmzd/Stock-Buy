using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Buy.API.Domain
{
    public class Part : Product
    {
        public List<Bundle> Bundles { get; set; } = new();
        public int StockQuantity { get; set; }

        public static explicit operator Part(AssociatedPart associatedPart)
        {
            return new Part
            {
                Id = associatedPart.PartId,
                Name = associatedPart.Part.Name,
                StockQuantity = associatedPart.Part.StockQuantity,
                QuantityNeeded = associatedPart.QuantityNeeded
            };
        }
    }
}
