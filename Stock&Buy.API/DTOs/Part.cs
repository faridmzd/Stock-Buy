using Stock_Buy.API.Domain;
using Stock_Buy.API.DTOs.Bundles;

namespace Stock_Buy.API.DTOs.Parts
{
    #region Requests

    public record GetPartsRequest(int? PageNumber, int? PageSize);
    public record AddPartRequest(string Name, int StockQuantity)
    {
        public static explicit operator Part(AddPartRequest request)
        {
            return new Part { Name = request.Name, StockQuantity = request.StockQuantity };
        }
    }
    public record UpdatePartRequest(string? Name, int? StockQuantity);

    #endregion

    #region Responses

    public record GetPartResponse(Guid Id, string Name, int StockQuantity)
    {
        public static explicit operator GetPartResponse(Part part)
        {
            return new GetPartResponse(part.Id, part.Name, part.StockQuantity);
        }
    }
    public record AddPartResponse(Guid Id, string Name, int StockQuantity)
    {
        public static explicit operator AddPartResponse(Part part)
        {
            return new AddPartResponse(part.Id, part.Name, part.StockQuantity);
        }
    }
    public record GetPartsResponse(Guid Id, string Name, int StockQuantity);

    #endregion

}
