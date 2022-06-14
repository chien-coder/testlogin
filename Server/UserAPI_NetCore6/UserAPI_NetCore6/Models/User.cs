using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace UserAPI_NetCore6.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Range(minimum:5,maximum:22)]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public Roles roles { get; set; }

    }
    public enum Roles
    {
        Admin,
        staff
    }
}
