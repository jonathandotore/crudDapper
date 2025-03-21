using AutoMapper;
using CrudDapper.Dto;
using CrudDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace CrudDapper.Services
{
    public class UserService : IUserInterface
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ListUserDto>>> GetAllUsers()
        {
            ResponseModel<List<ListUserDto>> response = new ResponseModel<List<ListUserDto>>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var usersDb = await connection.QueryAsync<Users>("SELECT * FROM USERS");

                if (!usersDb.Any())
                {
                    response.Message = "User not found!";
                    response.Status = false;
                    return response;
                }
                
                var mappedUser = _mapper.Map<List<ListUserDto>>(usersDb);

                response.Data = mappedUser;
                response.Message = "Success!";

                return response;
            }
        }
    }
}
