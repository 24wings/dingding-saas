using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Wings.Saas.Shared.Areas.Worker.Dtos
{
  public class ScanTaskOutput
  {

    public int Id { get; set; }
    public int Start { get; set; }

    public int End { get; set; }
    /// <summary>
    /// redis task name
    /// </summary>
    [DisplayName("任务名")]
    public string TaskName { get; set; }
    public ScanTaskStatus Status { get; set; } = ScanTaskStatus.Ready;
  }
  public enum ScanTaskStatus
  {
    Ready,
    Process,
    Finished,
    Cancel


  }

}