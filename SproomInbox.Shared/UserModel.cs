using System.ComponentModel.DataAnnotations;

namespace SproomInbox.Shared
{
    public class UserModel
    {
        //username
        [Required]
        public string Username { get; set; }

        //firstname of the user
        [Required]
        public string FirstName { get; set; }

        //lastname of the user
        [Required]
        public string LastName { get; set; }
    }
}
