using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.MessageReadBy;
using Facebook.Application.IServices.IConservation;

namespace Facebook.API.Controllers
{
    public class MessageReadByController : BaseController<MessageReadByDto, MessageReadByCreateDto, MessageReadByUpdateDto>
    {
        private readonly IMessageReadByService _messageReadByService;
        public MessageReadByController(IMessageReadByService messageReadByService) : base(messageReadByService)
        {
            _messageReadByService = messageReadByService;
        }
    }
}
