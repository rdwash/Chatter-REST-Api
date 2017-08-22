using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatterApi.Domain.Entities
{
    [Table("Message")]
    public partial class Message
    {
        public int Id { get; set; }

        [Required]
        [Column("message")]
        public string ChatMessage { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
