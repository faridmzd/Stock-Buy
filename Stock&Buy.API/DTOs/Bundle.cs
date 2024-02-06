using Stock_Buy.API.Domain;
using static Stock_Buy.API.DTOs.Bundles.AssociateBundlesRequest;
using static Stock_Buy.API.DTOs.Bundles.AssociatePartsRequest;
using static Stock_Buy.API.DTOs.Bundles.GetBundleResponse;

namespace Stock_Buy.API.DTOs.Bundles
{
    #region Requests

    public record GetBundlesRequest(int? PageNumber, int? PageSize);
    public record AddBundleRequest(string Name)
    {
        public static explicit operator Bundle(AddBundleRequest request)
        {
            return new Bundle { Name = request.Name };
        }
    }
    public record AssociatePartsRequest(Guid BundleId, List<AssociatePartsRequestDTO> Parts)
    {
        public static explicit operator List<AssociatedPart>(AssociatePartsRequest request)
        {
            return request.Parts.Select(x => new AssociatedPart { BundleId = request.BundleId, PartId = x.PartId, QuantityNeeded = x.Quantity }).ToList();
        }
        public record AssociatePartsRequestDTO(Guid PartId, int Quantity);
    }
    public record AssociateBundlesRequest(Guid BundleId, List<AssociateBundlesRequestDTO> Bundles)
    {
        public static explicit operator List<AssociatedBundle>(AssociateBundlesRequest request)
        {
            return request.Bundles.Select(x => new AssociatedBundle { ParentBundleId  = request.BundleId, ChildBundleId = x.bundleId, QuantityNeeded = x.quantity }).ToList();
        }

        public record AssociateBundlesRequestDTO(Guid bundleId, int quantity);
    }
    public record UpdateAssociateBundleRequest(Guid BundleId, Guid AssociatedBundleId, int Quantity)
    {
        public static explicit operator AssociatedBundle(UpdateAssociateBundleRequest request)
        {
            return new AssociatedBundle { ParentBundleId = request.BundleId, ChildBundleId = request.AssociatedBundleId, QuantityNeeded = request.Quantity };
        }
    }
    public record UpdateAssociatePartRequest(Guid BundleId, Guid AssociatedPartId, int Quantity)
    {
        public static explicit operator AssociatedPart(UpdateAssociatePartRequest request)
        {
            return new AssociatedPart { BundleId = request.BundleId, PartId = request.AssociatedPartId, QuantityNeeded = request.Quantity };
        }
    }
    public record UpdateBundleRequest(string Name);

    #endregion

    #region Responses

    public record GetBundleResponse(Guid Id, string Name, List<AssociatedBundleDTO> AssociatedBundles, List<AssociatedPartDTO> AssociatedParts)
    {

        public static explicit operator GetBundleResponse(Bundle bundle)
        {
            return new GetBundleResponse(
                bundle.Id,
                bundle.Name,
                bundle.AssociatedBundles.Select(x => (AssociatedBundleDTO)x).ToList(),
                bundle.AssociatedParts.Select(p => (AssociatedPartDTO)p).ToList()
                );

        }

        public record AssociatedBundleDTO(Guid Id, string Name, int Quantity, List<AssociatedBundleDTO> AssociatedBundles, List<AssociatedPartDTO> AssociatedParts)
        {
            public static explicit operator AssociatedBundleDTO(Bundle bundle)
            {
                var associatedBundles = bundle.AssociatedBundles.Select(x => (AssociatedBundleDTO)x).ToList();
                var associatedParts = bundle.AssociatedParts.Select(p => (AssociatedPartDTO)p).ToList();

                return new AssociatedBundleDTO(bundle.Id, bundle.Name, bundle.QuantityNeeded, associatedBundles, associatedParts);
            }
        }

        public record AssociatedPartDTO(Guid Id, string Name, int Quantity)
        {
            public static explicit operator AssociatedPartDTO(Part part)
            {
                return new AssociatedPartDTO(part.Id, part.Name, part.QuantityNeeded);
            }
        }

    }
    public record GetBundlesResponse(Guid BundleId, string Name);
    public record AddBundleResponse(Guid BundleId, string Name)
    {
        public static explicit operator AddBundleResponse(Bundle bundle)
        {
            return new AddBundleResponse(bundle.Id, bundle.Name);
        }
    }

    #endregion

}
