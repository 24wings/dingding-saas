using System.ComponentModel.DataAnnotations;

namespace Wings.Saas.Server.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PassWord { get; set; }
  }

}