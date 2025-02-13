using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.ConservationMember;
using Facebook.Application.IServices.IConservation;

namespace Facebook.API.Controllers
{
    public class ConservationMemberController : BaseController<ConservationMemberDto, ConservationMemberCreateDto, ConservationMemberUpdateDto>
    {
        private readonly IConservationMemberService _conservationMemberService;
        public ConservationMemberController(IConservationMemberService conservationMemberService) : base(conservationMemberService)
        {
            _conservationMemberService = conservationMemberService;
        }
    }
}
