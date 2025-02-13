using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Conservation;
using Facebook.Application.IServices.IConservation;

namespace Facebook.API.Controllers
{
    public class ConservationController : BaseController<ConservationDto, ConservationCreateDto, ConservationUpdateDto>
    {
        private readonly IConservationService _conservationService;
        public ConservationController(IConservationService conservationService) : base(conservationService)
        {
            _conservationService = conservationService;
        }
    }
}
