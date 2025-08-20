using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Resturang_luna.Models
{
    [Index(nameof(UserName))]
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
