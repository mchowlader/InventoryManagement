using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.DBModel
{
    [Table("UserAuditLog")]
    public class UserAuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime ActionMoment { get; set; }
        public string? IpAddress { get; set; }
        public long ByUserId { get; set; }
        public Guid AffectedUserId { get; set; }
        public long ActionId { get; set; }
        public virtual Action? Stores { get; set; }
    }
}
