using Microsoft.AspNetCore.Mvc;
using Stock_Buy.API.Contracts.V1;
using Stock_Buy.API.Domain;
using Stock_Buy.API.DTOs.Bundles;
using Stock_Buy.API.DTOs.Pagination;
using Stock_Buy.API.Services.Abstract;

namespace Stock_Buy.API.Controllers.V1
{
    public class BundleController : ControllerBase
    {
        IBundleService _bundleService;
        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }


        [HttpGet(ApiRoutes.Bundles.GetAll)]
        public async Task<IActionResult> GetAllBundlesAsync([FromQuery] GetBundlesRequest request)
        {
            var response = await _bundleService.GetPaginatedAsync(
                new PaginationFilter(request?.PageNumber, request?.PageSize),
                b => new GetBundlesResponse(b.Id, b.Name));

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Bundles.Get)]
        public async Task<IActionResult> GetBundleAsync([FromRoute] Guid id)
        {
            return Ok((GetBundleResponse)await _bundleService.GetByIdAsync(id));
        }

        [HttpDelete(ApiRoutes.Bundles.Delete)]
        public async Task<IActionResult> DeleteBundleAsync([FromRoute] Guid id)
        {
            await _bundleService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost(ApiRoutes.Bundles.Add)]
        public async Task<IActionResult> AddBundleAsync([FromBody] AddBundleRequest request)
        {
            return Ok((AddBundleResponse)await _bundleService.AddAsync((Bundle)request));
        }

        [HttpPut(ApiRoutes.Bundles.Update)]
        public async Task<IActionResult> UpdateBundleAsync([FromRoute] Guid id, [FromBody] UpdateBundleRequest request)
        {
            var bundle = await _bundleService.GetByIdAsync(id);
            bundle.Name = request.Name;
            await _bundleService.UpdateAsync(bundle);

            return NoContent();
        }

        [HttpGet(ApiRoutes.Bundles.GetMaxProductionAmount)]
        public async Task<IActionResult> GetBundleMaxProductionAmountAsync([FromRoute] Guid id)
        {
            return Ok(await _bundleService.GetMaxProductionAmountAsync(id));
        }

        [HttpPost(ApiRoutes.Bundles.AddAssociateParts)]
        public async Task<IActionResult> AddAssociatePartsAsync([FromBody] AssociatePartsRequest request)
        {
            await _bundleService.AddAssociatePartsAsync(request.BundleId, (List<AssociatedPart>)request);
            return Created();
        }

        [HttpPost(ApiRoutes.Bundles.AddAssociateBundles)]
        public async Task<IActionResult> AddAssociateBundlesAsync([FromBody] AssociateBundlesRequest request)
        {
            await _bundleService.AddAssociateBundlesAsync(request.BundleId, (List<AssociatedBundle>)request);
            return Created();
        }

        [HttpPut(ApiRoutes.Bundles.UpdateAssociatedBundle)]
        public async Task<IActionResult> UpdateAssociatedBundleAsync([FromBody] UpdateAssociateBundleRequest request)
        {
            await _bundleService.UpdateAssociatedBundleAsync((AssociatedBundle)request);
            return NoContent();
        }

        [HttpPut(ApiRoutes.Bundles.UpdateAssociatedPart)]
        public async Task<IActionResult> UpdateAssociatedPartAsync([FromBody] UpdateAssociatePartRequest request)
        {
            await _bundleService.UpdateAssociatedPartAsync((AssociatedPart)request);
            return NoContent();
        }


    }
}
