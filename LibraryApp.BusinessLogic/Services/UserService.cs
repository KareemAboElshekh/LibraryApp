using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.BusinessLogic.Interfaces;
using LibraryApp.BusinessLogic.Models;
using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using static LibraryApp.BusinessLogic.Models.DTOs;

namespace LibraryApp.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<LibraryDbContext> _factory;
        public UserService(IDbContextFactory<LibraryDbContext> factory)
            => _factory = factory;

        public async Task<UserDto?> AuthenticateAsync(string u, string p)
        {
            await using var ctx = _factory.CreateDbContext();
            var user = await ctx.Users.Include(x => x.Type)
                .FirstOrDefaultAsync(x => x.UserName == u && x.Password == p);
            if (user == null) return null;
            return new UserDto { UserId = user.UserId, Username = user.UserName, TypeName = user.Type.TypeName };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            await using var ctx = _factory.CreateDbContext();
            return await ctx.Users.Include(u => u.Type)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.UserName,
                    TypeName = u.Type.TypeName
                })
                .ToListAsync();
        }

        public async Task<OperationResult> CreateUserAsync(string username, string password, string phone, int typeId)
        {
            await using var ctx = _factory.CreateDbContext();
            if (await ctx.Users.AnyAsync(u => u.UserName == username))
                return new OperationResult { Success = false, Message = "المستخدم موجود بالفعل." };

            ctx.Users.Add(new User
            {
                UserName = username,
                Password = password,
                Phone = phone,
                TypeId = typeId
            });
            await ctx.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<OperationResult> DeleteUserAsync(int userId)
        {
            await using var ctx = _factory.CreateDbContext();
            var user = await ctx.Users.FindAsync(userId);
            if (user == null)
                return new OperationResult { Success = false, Message = "المستخدم غير موجود." };

            ctx.Users.Remove(user);
            await ctx.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<OperationResult> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            await using var ctx = _factory.CreateDbContext();
            var user = await ctx.Users.FindAsync(userId);
            if (user == null)
                return new OperationResult { Success = false, Message = "المستخدم غير موجود." };

            if (user.Password != oldPassword)
                return new OperationResult { Success = false, Message = "كلمة المرور الحالية غير صحيحة." };

            user.Password = newPassword;
            await ctx.SaveChangesAsync();
            return new OperationResult { Success = true };
        }
    }
}
