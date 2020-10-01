using System;
using System.ComponentModel.DataAnnotations;

namespace Ocr
{
    public class Group
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public int no { get; set; }
        public DateTime createAt { get; set; } = DateTime.Now;
    }
}
