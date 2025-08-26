using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Restaurang_luna.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    public class Admin
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
