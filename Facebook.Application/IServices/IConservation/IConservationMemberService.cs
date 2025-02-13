using Facebook.Application.Dtos.ConservationMember;
using Facebook.Application.IServices.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IConservation
{
    public interface IConservationMemberService : IBaseService<ConservationMemberDto, ConservationMemberCreateDto, ConservationMemberUpdateDto>
    {
    }
}
