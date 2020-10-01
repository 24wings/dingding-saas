using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wings.Saas.Shared.Areas.Worker.Dtos
{
    public class OcrTaskOutput
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage = "任务名必填")]
        public string TaskName { get; set; }
        /// <summary>
        /// 创建世界
        /// </summary>
        /// <value></value>
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 起步
        /// </summary>
        /// <value></value>
        public int Start { get; set; }
        /// <summary>
        /// 截止
        /// </summary>
        /// <value></value>
        public int End { get; set; }
        /// <summary>
        /// 密钥key
        /// </summary>
        /// <value></value>
        public string Key { get; set; }
        /// <summary>
        /// 密钥Secret
        /// </summary>
        /// <value></value>
        public string Secret { get; set; }
        public OcrTaskStatus Status { get; set; } = OcrTaskStatus.Ready;

        [NotMapped]

        public int Process { get; set; } = 0;



    }

    public enum OcrTaskStatus
    {
        Ready,
        Process,
        Finished
    }

}
