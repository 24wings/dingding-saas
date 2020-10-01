using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Saas.Server.Models
{
    public class KeySecret
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }

        public string Secret { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public DateTime LastUpdateDateTime { get; set; } = DateTime.Now;
        [Required]
        public string AccountName { get; set; }

        public string AccountPassword { get; set; }
    }

}
