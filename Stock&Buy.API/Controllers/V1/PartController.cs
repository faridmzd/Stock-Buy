using Microsoft.AspNetCore.Mvc;
using Stock_Buy.API.Contracts.V1;
using Stock_Buy.API.Domain;
using Stock_Buy.API.DTOs.Bundles;
using Stock_Buy.API.DTOs.Pagination;
using Stock_Buy.API.DTOs.Parts;
using Stock_Buy.API.Services.Abstract;
using Stock_Buy.API.Services.Concrete;

namespace Stock_Buy.API.Controllers.V1
{
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        public PartController(IPartService partService)
        {
            _partService = partService;
        }

        [HttpGet(ApiRoutes.Parts.GetAll)]
        public async Task<IActionResult> GetAllBundlesAsync([FromQuery] GetPartsRequest request)
        {
            var response = await _partService.GetPaginatedAsync(
                new PaginationFilter(request?.PageNumber, request?.PageSize),
                b => new GetPartsResponse(b.Id, b.Name, b.StockQuantity));

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Parts.Get)]
        public async Task<IActionResult> GetPartAsync([FromRoute] Guid id)
        {
            return Ok((GetPartResponse)await _partService.GetByIdAsync(id));
        }

        [HttpDelete(ApiRoutes.Parts.Delete)]
        public async Task<IActionResult> DeletePartAsync([FromRoute] Guid id)
        {
            await _partService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost(ApiRoutes.Parts.Add)]
        public async Task<IActionResult> AddPartAsync([FromBody] AddPartRequest request)
        {
            return Ok((AddPartResponse)await _partService.AddAsync((Part)request));
        }

        [HttpPut(ApiRoutes.Parts.Update)]
        public async Task<IActionResult> UpdatePartAsync([FromRoute] Guid id, [FromBody] UpdatePartRequest request)
        {
            var part = await _partService.GetByIdAsync(id);

            part.Name = request.Name != null && request.Name != part.Name ? request.Name : part.Name;
            part.StockQuantity = request.StockQuantity != null && (int)request.StockQuantity != part.StockQuantity ? (int)request.StockQuantity : part.StockQuantity;

            await _partService.UpdateAsync(part);

            return NoContent();
        }
    }
}
