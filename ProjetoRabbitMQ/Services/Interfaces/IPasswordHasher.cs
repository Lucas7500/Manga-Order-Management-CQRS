namespace ProjetoRabbitMQ.Services.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public bool Compare(string password, string hashedPassword);
    }
}
