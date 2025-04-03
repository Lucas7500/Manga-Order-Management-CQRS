using ProjetoRabbitMQ.Services.Interfaces;
using Scrypt;

namespace ProjetoRabbitMQ.Services
{
    public class ScryptPasswordHasher : IPasswordHasher
    {
        private readonly ScryptEncoder _encoder = new();

        public string Hash(string password) => _encoder.Encode(password);
        public bool Compare(string password, string hashedPassword) => _encoder.Compare(password, hashedPassword);
    }
}
