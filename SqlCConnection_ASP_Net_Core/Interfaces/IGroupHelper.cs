using Auth.Models;
using Chayns.Backend.Api.Models.Result;
using SqlCConnection_ASP_Net_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Interfaces
{
    public interface IGroupHelper
    {
        List<UacGroupResult> GetGroups();
        List<UacMemberResult> GetMembers(int groupId);
        List<int> GetGroupsOfMember(int UserId);
    }
}
