using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wings.Saas.Shared.Areas.Worker.Dtos
{
   public class KeySecretOutput
    {
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
