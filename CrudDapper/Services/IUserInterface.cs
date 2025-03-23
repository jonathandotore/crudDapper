using CrudDapper.Dto;
using CrudDapper.Models;

namespace CrudDapper.Services
{
    public interface IUserInterface
    {
        Task<ResponseModel<List<ListUserDto>>> GetAllUsers();
        Task<ResponseModel<ListUserDto>> GetUser(Guid id);
        Task<ResponseModel<List<ListUserDto>>> CreateUser (CreateUserDto newUser);
        Task<ResponseModel<ListUserDto>> UpdateUser(Guid id, UpdateUserDto user);
        Task<ResponseModel<List<ListUserDto>>> DeleteUser(Guid id);
    }
}
