using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Conservation;
using Facebook.Application.IServices.IConservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.API.Controllers
{
    public class ConservationController : BaseController<ConservationDto, ConservationCreateDto, ConservationUpdateDto>
    {
        private readonly IConservationService _conservationService;
        public ConservationController(IConservationService conservationService) : base(conservationService)
        {
            _conservationService = conservationService;
        }

        [HttpGet]
        [Route("relationship/{id}")]
        [Authorize]
        public async Task<ConservationDto> GetConservationByRelationshipId(Guid id)
        {
            var result = await _conservationService.GetByRelationshipId(id);
            return result;
        }


    }
}
