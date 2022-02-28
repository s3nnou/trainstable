using Microsoft.AspNetCore.Identity;

namespace TrainTable.DAL.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
