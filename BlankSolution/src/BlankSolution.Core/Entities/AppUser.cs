using Microsoft.AspNetCore.Identity;

namespace BlankSolution.Core.Entities;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}
