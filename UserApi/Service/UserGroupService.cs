using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service
{
    public class UserGroupService
    {
        private readonly UserApiDbContext _context;

        public UserGroupService(UserApiDbContext context)
        {
            _context = context;
        }

        public async Task<UserGroup> Create(UserGroup userGroup)
        {
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();
            return userGroup;
        }

        public UserGroup GetById(int id)
        {
            return _context.UserGroups.Find(id) ?? throw new Exception("UserGroup not found");
        }

        public IEnumerable<UserGroup> GetAll()
        {
            return _context.UserGroups.ToList();
        }
    }
}