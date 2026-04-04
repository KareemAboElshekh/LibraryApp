using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.BusinessLogic.Interfaces
{
    public interface IUserSessionService
    {
        UserDto? CurrentUser { get; set; }
    }
}
