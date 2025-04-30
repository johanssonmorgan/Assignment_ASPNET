using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class MembersPageViewModel
    {
        public IEnumerable<UserModel> Members { get; set; } = [];
        public IEnumerable<RoleModel> Roles { get; set; } = [];
        public AddMemberModalViewModel AddMember { get; set; } = new();
        public EditMemberModalViewModel EditMember { get; set; } = new();
    }
}
