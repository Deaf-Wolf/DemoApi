using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApi.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("userid")]
        public Guid Guid { get; set; }
        [Required]
        [Column("username")]
        public string UserName { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }


    }
}
