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
        public async Task<ResponseModel<ListUserDto>> GetUser(Guid id)
        {
            ResponseModel<ListUserDto> response = new ResponseModel<ListUserDto>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var user = await connection.QueryFirstOrDefaultAsync<Users>($"SELECT * FROM USERS WHERE Id = @Id", new {Id = id});

                if (user == null)
                {
                    response.Message = "User not found. Please, check if you provided correct information and try again!";
                    response.Status = false;

                    return response;
                }

                var mappedUser = _mapper.Map<ListUserDto>(user);

                response.Data = mappedUser;
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
                Salary = (newUser.Salary == null) ? 0.00 : newUser.Salary,
                Cpf = newUser.Cpf ?? throw new ArgumentNullException(nameof(newUser.Cpf), "CPF is required."),
                Status = newUser.Status,
                Password = newUser.Password ?? throw new ArgumentNullException(nameof(newUser.Password), "Password is required.")
            };

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                int rows = await connection.ExecuteAsync("INSERT INTO Users (FullName, Email, JobTitle, Salary, Cpf, Status, Password) VALUES (@FullName, @Email, @JobTitle, @Salary, @Cpf, @Status, @Password)", insertUser);

                if (rows == 0)
                    throw new Exception("Error on insert new User");

                var usersDb = await connection.QueryAsync<Users>("SELECT * FROM USERS");

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
    }
}
