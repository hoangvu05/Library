using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string? Username { get; set; }
        [Required]
        [StringLength(50)]
        //[Column("FirstName")]
        [Display(Name = "Password")]

        [JsonIgnore]
        public string? PasswordHash { get; set; }

        [JsonIgnore]

        public string? Role { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }

        [JsonIgnore]
        public List<Book> books { get; set; }
        //public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        //public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        ////[JsonIgnore]
        ////public byte[] Salt { get; set; }
        ///


        //[JsonIgnore]
        //public byte[] Salt { get; set; }
    }
}