using Facebook.Application.Dtos.Message;
using Facebook.Application.IServices.IConservation;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities.Conservation;
using Facebook.Domain.IRepositories.IBase;
using Facebook.Domain.IRepositories.IConservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Message
{
    public class MessageService : BaseService<MessageEntity, MessageDto, MessageCreateDto, MessageUpdateDto>, IMessageService
    {
        private readonly IMessageRepository _conservationRepository;

        public MessageService(IMessageRepository conservationRepository) : base(conservationRepository)
        {
            _conservationRepository = conservationRepository;
        }

        public override MessageDto MapEntityToEntityDto(MessageEntity entity)
        {
            throw new NotImplementedException();
        }

        public override MessageEntity MapInsertDtoToEntity(MessageCreateDto insertDto)
        {
            throw new NotImplementedException();
        }

        public override MessageEntity MapUpdateDtoToEntity(MessageUpdateDto updateDto, MessageEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
