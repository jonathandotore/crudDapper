namespace CrudDapper.Dto
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public double Salary { get; set; }
        public string Cpf { get; set; }
        public bool Status { get; set; }
    }
}
