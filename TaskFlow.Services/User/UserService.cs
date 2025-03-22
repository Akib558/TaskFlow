using TaskFlow.Core.DTOs;
using TaskFlow.Core.Records;
using TaskFlow.Repositories.User;

namespace TaskFlow.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //
        // public async Task<UserInfoResponseDto> GetUserByUsername(string username)
        // {
        //     var res = await _userRepository.GetUserByUsername(username);
        //     return new UserInfoResponseDto
        //     {
        //         Id = res.Id,
        //         GuidId = res.UserGuidId,
        //         Username = res.UserName,
        //         Email = res.UserEmail,
        //         Role = res.UserRole,
        //         CreatedDate = res.CreatedDate,
        //     };
        // }

        public async Task<UserInfoResponseDto> GetUserById(int UserId)
        {
            var res = await _userRepository.GetUserByUserId(UserId);
            return new UserInfoResponseDto
            {
                Id = res.Id,
                Username = res.UserName,
                Email = res.UserEmail,
                CreatedDate = res.CreatedDate
            };
        }


        public async Task<bool> UpdateUser(UserUpdateRequestDto user)
        {
            var obj = new UserRecord(
                0,
                user.Username,
                user.Email,
                "",
                false,
                user.CreatedDate
            );
            var res = await _userRepository.UpdateUser(obj);

            return res;

            // return new UserInfoResponseDto
            // {
            //     Id = res.Id,
            //     Username = res.UserName,
            //     Email = res.UserEmail,
            //     CreatedDate = res.CreatedDate
            // };
        }

        // public async Task DeleteUser(int id)
        // {
        //     await _userRepository.DeleteUser(id);
        // }
    }
}