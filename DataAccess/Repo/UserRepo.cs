using Microsoft.EntityFrameworkCore;
using StudyMash.API.Interfaces;
using StudyMash.API.Models;
using System.Threading.Tasks;

namespace StudyMash.API.DataAccess.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        public UserRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Authenticate(string Username, string Password)
        {
            return await _context.Users.FirstOrDefaultAsync(
                x=> x.Username == Username && x.Password == Password);
        }
    }
}
