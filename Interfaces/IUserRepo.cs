using StudyMash.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyMash.API.Interfaces
{
    public interface IUserRepo
    {
       Task<User> Authenticate(string Username , string Password);
    }
}
