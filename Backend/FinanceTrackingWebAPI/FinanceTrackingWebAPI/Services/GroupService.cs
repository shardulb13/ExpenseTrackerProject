using FinanceTrackingWebAPI.DataAccessLayer;
using FinanceTrackingWebAPI.Entities;
using FinanceTrackingWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTrackingWebAPI.Services
{
    public interface IGroupService
    {
        IEnumerable<GroupVM> GetAllGroups(string userId);
        Task<int> CreateGroup(GroupVM groups);
        Task<int> UpdateGroup(GroupVM groups);
        bool DeleteGroup(int id);
        bool DeleteGroupUser(string userId);
    }
    public class GroupService : IGroupService
    {
        private readonly IGroupDA _groupDA;
        public GroupService(IGroupDA groupService)
        {
            _groupDA = groupService;
        }

        public bool DeleteGroup(int id)
        {
            return _groupDA.DeleteGroup(id);
        }

        public bool DeleteGroupUser(string userId)
        {
            return _groupDA.DeleteGroupUser(userId);
        }

        public IEnumerable<GroupVM> GetAllGroups(string userId)
        {
            var result = _groupDA.GetGroups(userId);
            if (result != null)
            {
                return (from grp in result
                        select new GroupVM
                        {
                            Id = grp.groupId,
                            GroupName = grp.groupName,
                            Count = grp.Count,
                            UserId = grp.userIds
                        }).ToList();
            }
            return null;
        }
        public async Task<int> CreateGroup(GroupVM groupModel)
        {
            var group = new Group
            {
                GroupName = groupModel.GroupName,
                CreatedBy = groupModel.Id,
                CreatedOn = DateTime.Now,
                IsActive = groupModel.IsActive,
            };
            var groupId = await _groupDA.AddGroup(group);
            foreach (var id in groupModel.UserId)
            {
                var usersGroup = new UsersGroup
                {
                    GroupId = group.Id,
                    UserId = id,
                };
                await _groupDA.UsersGroups(usersGroup);
            }
            return groupId;
        }
        public async Task<int> UpdateGroup(GroupVM groupModel)
        {
            var group = new Group
            {
                Id = groupModel.Id,
                GroupName = groupModel.GroupName,
            };
            var modifiedGroupId = await _groupDA.UpdateGroup(group);
            foreach (var id in groupModel.UserId)
            {
                var usersGroup = new UsersGroup
                {
                    GroupId = group.Id,
                    UserId = id,
                };
                await _groupDA.UsersGroups(usersGroup);
            }
            return modifiedGroupId;
        }
    }
}
