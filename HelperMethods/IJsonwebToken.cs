using TodoWebAPI.Models;

namespace TodoWebAPI.HelperMethods
{
    public interface IJsonwebToken
    {
        string GenerateToken(User user);
    }
}
