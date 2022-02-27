using System.ComponentModel.DataAnnotations;

namespace SproomInbox.API.Data.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
