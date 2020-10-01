namespace Wings.Saas.Shared.Areas.Common.Dtos
{
  public class Res<T>
  {
    public T Result { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static Res<K> Success<K>(K data, string message = null)
    {
      return new Res<K> { IsSuccess = true, Result = data, Message = message };
    }

    public static Res<K> Error<K>(K data, string message = null)
    {
      return new Res<K> { IsSuccess = false, Result = data, Message = message };
    }
  }
}