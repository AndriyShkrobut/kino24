using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace kino24_user.core.Entities.User
{
    public class User : IdentityUser
    {
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }
    }
}
