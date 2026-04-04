using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.BusinessLogic.Interfaces;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.BusinessLogic.Services
{
    // UserSessionService.cs
    public class UserSessionService : IUserSessionService
    {
        public UserDto? CurrentUser { get; set; }
    }

}
