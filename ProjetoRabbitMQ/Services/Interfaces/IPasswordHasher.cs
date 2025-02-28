namespace ProjetoRabbitMQ.Services.Interfaces
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool Compare(string password, string hashedPassword);
    }
}
