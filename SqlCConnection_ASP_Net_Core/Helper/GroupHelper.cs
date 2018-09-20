using Auth.Models;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Credentials;
using Chayns.Backend.Api.Repository;
using Microsoft.Extensions.Options;
using SqlCConnection_ASP_Net_Core.Interfaces;
using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chayns.Backend.Api.Models.Result;

namespace SqlCConnection_ASP_Net_Core.Helper
{
    public class GroupHelper : IGroupHelper
    {
        public ChaynsApiInfo _backendApiSettings;

        public GroupHelper(IOptions<ChaynsApiInfo> backendApiSettings)
        {
            _backendApiSettings = backendApiSettings.Value;
        }

        public List<UacGroupResult> GetGroups()
        {
            var secret = new SecretCredentials(_backendApiSettings.Secret, 430010);
            var uacRepo = new UacRepository(secret);
            var uacData = new UacGroupDataGet(158753);
            var groups = uacRepo.GetUacGroups(uacData);
            return groups.Data;
        }
        public List<UacMemberResult> GetMembers(int groupId)
        {
            var secret = new SecretCredentials(_backendApiSettings.Secret, 430010);
            var uacMemberRepo = new UacMemberRepository (secret);
            var uacMemberData = new UacMemberDataGet(158753)
            {
                UacGroupId = groupId
            };
            var members = uacMemberRepo.GetUacGroupMember(uacMemberData);
            return members.Data;
        }
        public List<int> GetGroupsOfMember(int userId)
        {
            var groupList = new List<int>();

            for (int i = 0; i < GetGroups().Count; i++)
            {
                var groups = this.GetGroups();
                var members = this.GetMembers(groups[i].UserGroupId);
                for (int x = 0; x < members.Count; x++)
                {
                    if (members[x].UserId == userId)
                    {
                        groupList.Add(groups[i].UserGroupId);
                    }
                }
                
            }
            Console.WriteLine(groupList[0]);
            Console.WriteLine(groupList[1]);
            return groupList;
        }
    }
}
