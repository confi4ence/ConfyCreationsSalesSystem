using Microsoft.AspNetCore.Identity;

namespace ConfyCreationsSalesSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [PersonalData]
        public bool IsActive { get; set; } = true;
    }
}