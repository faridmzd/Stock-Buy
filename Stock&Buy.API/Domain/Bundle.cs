using Stock_Buy.API.DTOs.Bundles;
using System.ComponentModel.DataAnnotations;

namespace Stock_Buy.API.Domain
{
    public class Bundle : Product
    {
        public List<Bundle> AssociatedBundles { get; set; } = new();
        public List<Part> AssociatedParts { get; set; } = new();

        public static explicit operator Bundle(AssociatedBundle associatedBundle)
        {
            return new Bundle 
            {
                Id = associatedBundle.ChildBundleId,
                Name = associatedBundle.ChildBundle.Name,
                QuantityNeeded = associatedBundle.QuantityNeeded
            };
        }
    }
}
