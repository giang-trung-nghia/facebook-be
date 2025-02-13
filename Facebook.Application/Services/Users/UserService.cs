using AutoMapper;
using Facebook.Application.Dtos.Base;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IBase;
using Facebook.Application.IServices.IUsers;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Users
{
    public class UserService : BaseService<UserEntity, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task ValidateInsertBusiness(UserEntity user)
        {
            var existedUser = await GetAsync(user.Id);

            if (existedUser != null)
            {
                throw new ConflictException($"User: {user.Id} existed in the database");
            }

            // should validate email dupplicated

        }

        public override UserDto MapEntityToEntityDto(UserEntity userEntity)
        {
            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public override UserEntity MapInsertDtoToEntity(UserCreateDto insertDto)
        {
            var userEntity = _mapper.Map<UserEntity>(insertDto);

            return userEntity;
        }

        public override UserEntity MapUpdateDtoToEntity(UserUpdateDto updateDto, UserEntity entity)
        {
            // map từ source -> destination => gộp source vào destination
            // các trường source nếu có giá trị thì sẽ gán lại vào entity
            // nếu source = default | null => lấy giá trị của entity
            var userEntity = _mapper.Map(updateDto, entity);

            return userEntity;
        }

        public async Task<PagingResponse<UserRelationshipDto>> GetFriends(Guid id, int pageNumber, int pageSize)
        {
            var friends = await _userRepository.GetFriends(id, pageNumber, pageSize);
            var totalFriends = await _userRepository.GetTotalFriends(id);
            var result = new PagingResponse<UserRelationshipDto>()
            {
                data = _mapper.Map<List<UserRelationshipDto>>(friends),
                page = pageNumber,
                pageSize = pageSize,
                total = totalFriends,
                totalPage = totalFriends / pageSize
            };

            return result;
        }

        public async Task<PagingResponse<UserDto>> GetPeopleMaybeYouKnow(Guid id, int pageNumber, int pageSize)
        {
            var people = await _userRepository.GetPeopleMaybeYouKnow(id, pageNumber, pageSize);
            var result = new PagingResponse<UserDto>()
            {
                data = _mapper.Map<List<UserDto>>(people),
                page = pageNumber,
                pageSize = pageSize,
                total = people.Count, // @todo: not done, need to create other reposity to get correct data
                totalPage = people.Count // @todo: not done, need to create other reposity to get correct data
            };

            return result;
        }

        public async Task<PagingResponse<UserRelationshipDto>> GetAddFriendOffers(Guid id, int pageNumber, int pageSize)
        {
            var people = await _userRepository.GetAddFriendOffers(id, pageNumber, pageSize);
            var result = new PagingResponse<UserRelationshipDto>()
            {
                data = _mapper.Map<List<UserRelationshipDto>>(people),
                page = pageNumber,
                pageSize = pageSize,
                total = people.Count, // @todo: not done, need to create other reposity to get correct data
                totalPage = people.Count // @todo: not done, need to create other reposity to get correct data
            };

            return result;
        }
    }
}
