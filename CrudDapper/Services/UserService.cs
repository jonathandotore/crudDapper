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
        public async Task<ResponseModel<Users>> GetUser(Guid id)
        {
            ResponseModel<Users> response = new ResponseModel<Users>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var user = await connection.QueryFirstOrDefaultAsync<Users>($"SELECT * FROM USERS WHERE Id = @Id", new {Id = id});

                if (user == null)
                {
                    response.Message = "User not found. Please, check if you provided correct information and try again!";
                    response.Status = false;

                    return response;
                }

                response.Data = user;
                response.Message = "Success!";
            }

            return response;
        }
        public async Task<ResponseModel<List<ListUserDto>>> CreateUser(CreateUserDto newUser)
        {
            ResponseModel<List<ListUserDto>> response = new ResponseModel<List<ListUserDto>>();

            if (newUser == null)
            {
                response.Message = "The provided information are incorrectly or missing data. Please, check and try again!";
                response.Status = false;
                
                return response;
            }

            var insertUser = new CreateUserDto()
            {
                FullName = newUser.FullName ?? throw new ArgumentNullException(nameof(newUser.FullName), "Full name is required."),
                Email = newUser.Email ?? throw new ArgumentNullException(nameof(newUser.Email), "Email is required."),
                JobTitle = newUser.JobTitle ?? throw new ArgumentNullException(nameof(newUser.JobTitle), "Job title is required."),
                Salary = newUser.Salary,
                Cpf = newUser.Cpf ?? throw new ArgumentNullException(nameof(newUser.Cpf), "CPF is required."),
                Status = newUser.Status,
                Password = newUser.Password ?? throw new ArgumentNullException(nameof(newUser.Password), "Password is required.")
            };

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                int rows = await connection.ExecuteAsync("INSERT INTO Users (FullName, Email, JobTitle, Salary, Cpf, Status, Password) VALUES (@FullName, @Email, @JobTitle, @Salary, @Cpf, @Status, @Password)", insertUser);

                if (rows == 0)
                    throw new Exception("Error on insert new User");

                var usersDb = await ListUsers(connection);

                if (!usersDb.Any())
                {
                    response.Message = "Users not found!";
                    response.Status = false;
                    return response;
                }

                var mappedUser = _mapper.Map<List<ListUserDto>>(usersDb);

                response.Data = mappedUser;
                response.Message = "Success!";
            }

            return response;
        }
        public async Task<ResponseModel<ListUserDto>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            var response = new ResponseModel<ListUserDto>();

            if (updateUserDto == null)
            {
                response.Message = "The provided data is invalid. Please check it and try again";
                response.Status = false;

                return response;
            }

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var userDb = await connection.ExecuteAsync(@"UPDATE USERS
                                                                SET
                                                                    FullName = @FullName,
                                                                    Email = @Email,
                                                                    JobTitle = @JobTitle,
                                                                    Salary = @Salary,
                                                                    Cpf = @Cpf,
                                                                    Status = @Status
                                                             WHERE Id = @Id", updateUserDto);
   
                if (userDb == 0)
                    throw new Exception("Error on update the specified user");

                var updatedUser = await ListUserById(connection, id);

                if (updatedUser == null)
                {
                    response.Message = "User not found!";
                    response.Status = false;
                    return response;
                }

                var mappedUser = _mapper.Map<ListUserDto>(updatedUser);

                response.Data = mappedUser;
                response.Message = "Success!";
            }

            return response;

        }
        public async Task<ResponseModel<List<ListUserDto>>> DeleteUser(Guid id)
        {
            ResponseModel<List<ListUserDto>> response = new ResponseModel<List<ListUserDto>>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var deleteUser = await connection.ExecuteAsync(@"DELETE FROM USERS WHERE Id = @Id", new { Id = id});

                if (deleteUser == 0)
                {
                    response.Message = "User not found or provided infos are incorrect. Please check it and try again!";
                    response.Status = false;

                    return response;
                }

                var usersList = await ListUsers(connection);

                if (usersList == null)
                {
                    response.Message = "An error occured during your solicitation, please try again.";
                    response.Status = false;
                }

                var mappedUsers = _mapper.Map<List<ListUserDto>>(usersList);

                response.Data = mappedUsers;
                response.Message = "Success!";

                return response;

            }
        }

        private static async Task<IEnumerable<Users>> ListUsers(SqlConnection connection)
        {
            return await connection.QueryAsync<Users>("SELECT * FROM USERS");
        }
        private static async Task<IEnumerable<Users>> ListUserById(SqlConnection connection, Guid id)
        {
            return await connection.QueryAsync<Users>(@"SELECT * FROM USERS WHERE Id = @Id", new { Id = id });
        }
    }
}
