using System.ComponentModel.DataAnnotations;

namespace SproomInbox.Shared
{
    public class UserModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// User FirstName
        /// </summary>
        [Required]
        public string FirstName { get; set; }


        /// <summary>
        /// User Lastname
        /// </summary>
        [Required]
        public string LastName { get; set; }
    }
}
