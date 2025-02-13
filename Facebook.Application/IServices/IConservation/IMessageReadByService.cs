using Facebook.Application.Dtos.MessageReadBy;
using Facebook.Application.IServices.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IConservation
{
    public interface IMessageReadByService : IBaseService<MessageReadByDto, MessageReadByCreateDto, MessageReadByUpdateDto>
    {
    }
}
