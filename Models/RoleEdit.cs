using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AniWatch.Models
{
    public class RoleEdit
    {
        public RoleEdit() {
            Users = new List<string>(); 
        }
        public IdentityRole Role { get; set; }

        public string Id { get; set; }

        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }

        public string RoleName { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
        public List<string>Users { get; set; }

    }

}
