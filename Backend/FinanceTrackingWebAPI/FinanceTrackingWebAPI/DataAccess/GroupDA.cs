using FinanceTrackingWebAPI.Data;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.DataAccessLayer
{
    public interface IGroupDA
    {
        IEnumerable<UsersGroupDTO> GetGroups(string userId);
        Task<int> AddGroup(Group groups);
        Task<int> UsersGroups(UsersGroup usersGroup);
        Task<int> UpdateGroup(Group groups);
        bool DeleteGroup(int id);
        bool DeleteGroupUser(string userId);
    }
    public class GroupDA : IGroupDA
    {
        private readonly ApplicationDbContext _context;
        public GroupDA(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool DeleteGroup(int id)
        {
            var result = _context.Groups.FirstOrDefault(a => a.Id == id);
            if (result != null)
            {
                _context.Groups.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteGroupUser(string userId)
        {
            var result = _context.UsersGroup.FirstOrDefault(x => x.ApplicationUser.UserName == userId);
            if (result != null)
            {
                _context.UsersGroup.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public IEnumerable<UsersGroupDTO> GetGroups(string userId)
        {
            return _context.Groups.Select(o => new UsersGroupDTO
            {
                groupId = o.Id,
                groupName = o.GroupName,
                Count = o.usersGroup.Where(x => x.Groups.GroupName == o.GroupName).Count(),
                userIds = o.usersGroup.Select(un => un.ApplicationUser.UserName).ToList()

            }).Where(a => a.userIds.Contains(userId)).ToList();
        }

        public async Task<int> AddGroup(Group groups)
        {
            await _context.Groups.AddAsync(groups);
            await _context.SaveChangesAsync();
            return groups.Id;
        }
        public async Task<int> UsersGroups(UsersGroup usersgroup)
        {
            await _context.UsersGroup.AddAsync(usersgroup);
            await _context.SaveChangesAsync();
            return usersgroup.Id;
        }

        public async Task<int> UpdateGroup(Group groups)
        {
            var updateGroup = _context.Groups.FirstOrDefault(a => a.Id == groups.Id);
            if (updateGroup != null)
            {
                updateGroup.GroupName = groups.GroupName;
                await _context.SaveChangesAsync();
                return updateGroup.Id;
            }
            throw new System.Exception();
        }
    }
}
