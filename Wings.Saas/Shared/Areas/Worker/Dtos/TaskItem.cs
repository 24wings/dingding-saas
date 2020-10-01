using System;
using System.Collections.Generic;
using System.Text;

namespace Wings.Saas.Shared.Areas.Worker.Dtos
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int No { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public string OcrResult { get; set; }
    }

}
