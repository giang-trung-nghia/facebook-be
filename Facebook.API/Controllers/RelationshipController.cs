using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Relationship;
using Facebook.Application.IServices.IRelationship;
using Facebook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.API.Controllers
{
    public class RelationshipController : BaseController<RelationshipDto, RelationshipCreateDto, RelationshipUpdateDto>
    {
        private readonly IRelationshipService _relationshipService;

        public RelationshipController(IRelationshipService relationshipService) : base(relationshipService)
        {
            _relationshipService = relationshipService;
        }

        [HttpPut("accept-friend")]
        [Authorize]
        public async Task<RelationshipDto> AcceptFriend([FromQuery] Guid id)
        {
            var result = await _relationshipService.AcceptFriend(id);
            return result;
        }
    }
}
