using System.Threading.Tasks;

namespace Movies.Contract
{
    public interface ITokenBuilderService
    {
        Task<bool> Authenticate(string email, string Password);
        Task<string> GetToken(string email);
    }
}
