using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.BusinessLogic.Models;
using LibraryApp.DataAccess.Entities;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> AuthenticateAsync(string username, string password);

        // جلب جميع المستخدمين
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        // إنشاء مستخدم جديد
        Task<OperationResult> CreateUserAsync(string username, string password, string phone, int typeId);

        // حذف مستخدم
        Task<OperationResult> DeleteUserAsync(int userId);

        // تغيير كلمة المرور
        Task<OperationResult> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }

}
