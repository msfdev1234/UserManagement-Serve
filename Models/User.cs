using System.ComponentModel.DataAnnotations;

namespace UserManagement_Serv.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public String Token { get; set; }

    }
}
