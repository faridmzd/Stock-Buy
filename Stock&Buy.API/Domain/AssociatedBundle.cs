namespace Stock_Buy.API.Domain
{
    public class AssociatedBundle
    {
        public Bundle ParentBundle { get; set; }
        public Bundle ChildBundle { get; set; }
        public Guid ParentBundleId { get; set; }
        public Guid ChildBundleId { get; set; }
        public int QuantityNeeded { get; set; }
    }
}
