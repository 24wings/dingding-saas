using Wings.Saas.Shared.Areas.Worker.Dtos;

namespace Wings.Saas.Server.Models
{
  /// <summary>
  /// scan dingding account  with dingding worker
  /// </summary>
  public class ScanTask
  {

    public int Id { get; set; }
    public int Start { get; set; }

    public int End { get; set; }
    /// <summary>
    /// redis task name
    /// </summary>
    public string TaskName { get; set; }

    public ScanTaskStatus Status { get; set; } = ScanTaskStatus.Ready;

  }




}