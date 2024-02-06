namespace Stock_Buy.API.Domain
{
    public class AssociatedPart
    {
        public Guid BundleId { get; set; }
        public Bundle Bundle { get; set; }
        public Part Part { get; set; }
        public Guid PartId { get; set; }
        public int QuantityNeeded { get; set; }
    }
}
