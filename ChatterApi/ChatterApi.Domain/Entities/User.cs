using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatterApi.Domain.Entities
{
    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Column("username")]
        public string Username { get; set; }
    }
}