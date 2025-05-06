using ProjetoRabbitMQ.Models.Base;

namespace ProjetoRabbitMQ.Services.Interfaces
{
    public interface IPasswordHasher
    {
        public Result<string> Hash(string password);
        public Result<bool> Compare(string password, string hashedPassword);
    }
}
