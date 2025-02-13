using Facebook.Application.Dtos.MessageReadBy;
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

namespace Facebook.Application.Services.MessageReadBy
{
    public class MessageReadByService : BaseService<MessageReadByEntity, MessageReadByDto, MessageReadByCreateDto, MessageReadByUpdateDto>, IMessageReadByService
    {
        private readonly IMessageReadByRepository _conservationRepository;

        public MessageReadByService(IMessageReadByRepository conservationRepository) : base(conservationRepository)
        {
            _conservationRepository = conservationRepository;
        }

        public override MessageReadByDto MapEntityToEntityDto(MessageReadByEntity entity)
        {
            throw new NotImplementedException();
        }

        public override MessageReadByEntity MapInsertDtoToEntity(MessageReadByCreateDto insertDto)
        {
            throw new NotImplementedException();
        }

        public override MessageReadByEntity MapUpdateDtoToEntity(MessageReadByUpdateDto updateDto, MessageReadByEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
