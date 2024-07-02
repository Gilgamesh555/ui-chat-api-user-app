using UserApi.Data;
using UserApi.Models;

namespace UserApi.Service
{
    public class GroupService
    {
        private readonly UserApiDbContext _context;

        public GroupService(UserApiDbContext context)
        {
            _context = context;
        }

        public async Task<Group> Create(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public Group GetById(int id)
        {
            return _context.Groups.Find(id) ?? throw new Exception("Group not found");
        }

        public IEnumerable<Group> GetAll()
        {
            return _context.Groups.ToList();
        }
    }
}