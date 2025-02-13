using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Message;
using Facebook.Application.IServices.IConservation;

namespace Facebook.API.Controllers
{
    public class MessageController : BaseController<MessageDto, MessageCreateDto, MessageUpdateDto>
    {
        private readonly IMessageService _MessageService;
        public MessageController(IMessageService MessageService) : base(MessageService)
        {
            _MessageService = MessageService;
        }
    }
}
